using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Repositories
{
    public interface IJobsRepository
    {
        List<Job> GetAllJobs();
        Job GetJob(Guid jobId);
        void AddJob(Job job);
        void AddLog(Log log);
        void UpdateJob(Job job);
        void SaveChanges();
        Task SaveChangesAsync();
        bool DoesJobExist(string name);
    }
}