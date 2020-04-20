using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;

namespace ZavenDotNetInterview.App.Repositories
{
    public class LogsRepository
    {
        public LogsRepository()
        {
        }

        public List<Log> GetJobsLogs(Guid jobId)
        {
            using (var connection = new SqlConnection(@"data source=DESKTOP-MQ0KC6D\SQLEXPRESS;initial catalog=ZavenDotNetInterview;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot providerName = &quotSystem.Data.SqlClient"))
            {
                var logs = connection.Query<Log>($"SELECT * FROM Logs WHERE JobId = {jobId}").ToList();
                return logs;
            }
        }

        public Log InsertLog(Log log)
        {
            using (var connection = new SqlConnection(@"data source=DESKTOP-MQ0KC6D\SQLEXPRESS;initial catalog=ZavenDotNetInterview;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot providerName = &quotSystem.Data.SqlClient"))
            {
                string sql = "INSERT INTO Logs (Id, Description, CreatedAt, JobId) Values (@Id, @Description, @CreatedAt, @JobId);";

                log.CreatedAt = DateTime.UtcNow;
                var newLog = connection.Execute(sql, new { Id = log.Id, Description = log.Description, CreatedAt = log.CreatedAt, JobId = log.JobId });

                return log;
            }
        }
    }
}