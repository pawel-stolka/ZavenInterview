using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;

namespace ZavenDotNetInterview.App.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly ZavenDotNetInterviewContext _ctx;
        

        public JobsRepository(ZavenDotNetInterviewContext ctx)
        {
            _ctx = ctx;
           
        }

        public List<Job> GetAllJobs()
        {
            return _ctx.Jobs.ToList();
        }

        public void AddJob(Job job)
        {
            _ctx.Jobs.Add(job);
            //AddLog(job.Id, "Add");
            //SaveChangesAsync();
            //_ctx.SaveChanges();
        }

        public void UpdateJob(Job job)
        {
            _ctx.Entry(job).State = EntityState.Modified; //.Update(job);

            var log = new Log(job, "Update");
            _ctx.Logs.Add(log);
        }

        public void AddLog(Log log)
        {
            _ctx.Logs.Add(log);
        }

        public Job GetJob(Guid jobId)
        {
            return _ctx.Jobs.FirstOrDefault(x => x.Id == jobId);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            catch (CommitFailedException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                var e = ex;
                //throw;
            }

        }

        public bool DoesJobExist(string name)
        {
            return _ctx.Jobs.Any(j => j.Name == name);
        }
    }
}