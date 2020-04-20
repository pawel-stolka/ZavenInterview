using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid JobId { get; set; }
        public virtual Job Job { get; set; }

        public Log() { }
        public Log(Job job, string operation)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Description = Describe(job, operation);
            JobId = job.Id;
        }

        private string Describe(Job job, string operation)
        {
            return $"[{operation} | {job.Status}] {job.Name}";
        }
    }
}