using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityExample1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdentityExample1.Controllers
{
    public class TaskController : Controller
    {
        IConfiguration ConfigRoot;
        private DAL dal;
        public TaskController(IConfiguration config)
        {
            ConfigRoot = config;
            dal = new DAL(config.GetConnectionString("default"));
        }

        public IActionResult Index()
        {
            var results = dal.GetAllTasks();

            ViewData["Tasks"] = results;

            return View();
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            return View(new UserTasks());
        }

        [HttpPost]
        public IActionResult AddTask(UserTasks t)
        {
            int result = dal.CreateTask(t);


            return RedirectToAction("Index");
        }

    }
}