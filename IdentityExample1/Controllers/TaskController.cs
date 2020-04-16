using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityExample1.Models;
using IdentityExample1.Models.AccountViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IdentityExample1.Controllers
{
    public class TaskController : Controller
    {
        private DAL dal;
        public TaskController(IConfiguration config)
        {
            dal = new DAL(config.GetConnectionString("default"));
        }

        public IActionResult Index()
        {
            IEnumerable<UserTasks> results = dal.GetAllTasks();

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

        public IActionResult OwnerTasks(int id)
        {
            LoginViewModel owner = dal.GetUserById(id);

            IEnumerable<UserTasks> tasks = dal.GetTasksByUserId(id);
            ViewData["Tasks"] = tasks;
            ViewData["Id"] = id;

            return View(owner);
        }

        public IActionResult MarkComplete(int id)
        {
            var t = dal.GetTaskById(id);
            dal.MarkComplete(t);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int id)
        {
            var t = dal.GetTaskById(id);
            dal.DeleteTask(t);

            return RedirectToAction("Index");
        }

        public IActionResult SortByDateAsc(UserTasks t)
        {
            IEnumerable<UserTasks> results = dal.SortByDateAsc(t);

            ViewData["Tasks"] = results;

            return View("../Task/Index");
        }
        public IActionResult SortByDateDesc(UserTasks t)
        {
            IEnumerable<UserTasks> results = dal.SortByDateDesc(t);

            ViewData["Tasks"] = results;

            return View("../Task/Index");
        }



    }
}