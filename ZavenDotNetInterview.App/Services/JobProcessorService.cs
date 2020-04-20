using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZavenDotNetInterview.App.Extensions;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobProcessorService : IJobProcessorService
    {
        private ZavenDotNetInterviewContext _ctx;

        public JobProcessorService(ZavenDotNetInterviewContext ctx)
        {
            _ctx = ctx;
        }

        public void ProcessJobs()
        {
            IJobsRepository jobsRepository = new JobsRepository(_ctx);
            var allJobs = jobsRepository.GetAllJobs();
            var jobsToProcess = allJobs.Where(x => x.Status == JobStatus.New).ToList();

            jobsToProcess.ForEach(job => job.ChangeStatus(JobStatus.InProgress));
                        
            _ctx.SaveChanges();

            Parallel.ForEach(jobsToProcess, (currentjob) =>
            {
                new Task(async () =>
                {
                    bool result = await this.ProcessJob(currentjob).ConfigureAwait(false);
                    if (result)
                    {
                        currentjob.ChangeStatus(JobStatus.Done);
                    }
                    else
                    {
                        _ctx.SaveChanges();
                        currentjob.ChangeStatus(JobStatus.Failed);
                    }
                }).Start();
            });

            _ctx.SaveChanges();
        }

        private async Task<bool> ProcessJob(Job job)
        {
            Random rand = new Random();
            if (rand.Next(10) < 5)
            {
                await Task.Delay(2000);
                return false;
            }
            else
            {
                await Task.Delay(1000);
                return true;
            }
        }
    }
}