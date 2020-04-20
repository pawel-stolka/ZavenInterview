using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZavenDotNetInterview.App.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is mandatory")]
        [MinLength(2, ErrorMessage = "Must have at least 2 characters")]
        [MaxLength(15, ErrorMessage = "Not more than 15 characters")]  // for better testing
        [Remote("DoesJobExist", controller: "Jobs")]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public JobStatus Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DoAfter { get; set; }
        public int Fails { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public virtual List<Log> Logs { get; set; }

        public Job() { }
        public Job(string name, DateTime? doAfter)
        {
            Id = Guid.NewGuid();
            Name = name;
            Created = DateTime.UtcNow;
            Status = JobStatus.New;
            DoAfter = doAfter;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }

    public enum JobStatus
    {
        Closed = -2,
        Failed = -1,
        New = 0,
        InProgress = 1,
        Done = 2
    }
}