using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Services
{
    public interface IJobService: IJobProcessorService
    {
        List<Job> GetAllJobs();
        Job GetJob(Guid jobId);
        void AddJob(Job job);
        bool DoesJobExist(string name);
    }
}
