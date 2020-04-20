using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZavenDotNetInterview.App.Extensions;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobService : IJobService
    {
        private IJobsRepository _repo;
        private Random _rand;

        public JobService(IJobsRepository repository)
        {
            _repo = repository;
            _rand = new Random();
        }

        public List<Job> GetAllJobs()
        {
            var jobs = _repo.GetAllJobs()
                .OrderByDescending(x => x.Created)
                .ToList();
            return jobs;
        }

        public void AddJob(Job job)
        {
            var log = new Log(job, "Add");
            _repo.AddJob(job);
            _repo.AddLog(log);
            _repo.SaveChanges();
        }

        public bool DoesJobExist(string name)
        {
            return _repo.DoesJobExist(name);
        }

        public void ProcessJobs()
        {
            var allJobs = _repo.GetAllJobs();
            var statusToProcess = new[]
            {
                JobStatus.New,
                JobStatus.InProgress,
                JobStatus.Failed
            };
            var jobsToProcess = allJobs
                
                .Where(x => statusToProcess.Contains(x.Status))
                .Where(x => (x.DoAfter == null || DateTime.UtcNow >= x.DoAfter))
                .OrderByDescending(x => x.DoAfter)
                .ToList();

            jobsToProcess.ForEach(job => job.ChangeStatus(JobStatus.InProgress));
            _repo.SaveChanges();

            Parallel.ForEach(jobsToProcess, (currentjob) =>
            {
                new Task(async () => await this.ProcessJob(currentjob).ConfigureAwait(false)
                ).Start();
            });
        }

        private async Task ProcessJob(Job job)
        {
            var value = _rand.Next(10);
            if (value < 5)
            {
                await UpdateJob(job, JobStatus.Failed);
            }
            else
            {
                await UpdateJob(job, JobStatus.Done);
            }
        }

        private async Task UpdateJob(Job job, JobStatus status)
        {
            //var previousStatus = job.Status;
            if (status == JobStatus.Failed)
            {
                job.Fails++;
                JobStatus newStatus = job.Fails >= 5
                    ? JobStatus.Closed
                    : status;

                job.ChangeStatus(newStatus);
                // TODO: w ten sposób zawsze true => poprzedni jest InProgress
                //if(status != previousStatus)
                //    job.LastUpdatedAt = DateTime.UtcNow;

                _repo.UpdateJob(job);
                await Task.Delay(2000);
                await _repo.SaveChangesAsync();
            }
            else
            {
                job.ChangeStatus(JobStatus.Done);

                _repo.UpdateJob(job);
                await Task.Delay(1000);
                await _repo.SaveChangesAsync();
            }

        }


        public Job GetJob(Guid jobId)
        {
            var job = _repo.GetJob(jobId);
            return job;
        }



    }
}