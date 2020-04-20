using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Services;

namespace ZavenDotNetInterview.App.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobService _jobService;
        private const string INDEX = "Index";

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            List<Job> jobs = _jobService.GetAllJobs();

            return View(jobs);
        }

        // POST: Tasks/Process
        [HttpGet]
        public ActionResult Process()
        {
            _jobService.ProcessJobs();

            return RedirectToAction(INDEX);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(string name, DateTime? doAfter)
        {
            try
            {
                //TODO: Factory?
                Job newJob = new Job(name, doAfter);
                _jobService.AddJob(newJob);

                return RedirectToAction(INDEX);
            }
            catch(Exception e)
            {
                return View();
            }
        }

        public ActionResult Details(Guid jobId)
        {
            Job job = _jobService.GetJob(jobId);
            return View(job);
        }

        public JsonResult DoesJobExist(string Name)
        {

            bool exist = _jobService.DoesJobExist(Name);//.JobsWithName(Name);

            var response = exist
                ? "This name is already taken - please choose another one."
                : "true";

            return Json(response, JsonRequestBehavior.AllowGet);

        }
    }
}
