using DummyApplication.Database;
using DummyApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using DummyApplication.Hubs;

namespace DummyApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var listOfUsers = new List<Users>();
            using (var context = new ApplicationContext())
            {
                listOfUsers.AddRange(context.Users.ToList());
            }

            return View(listOfUsers);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string UserName, string UserEmail, string UserAddress)
        {
            var obj = new Users
            {
                UserName = UserName,
                UserEmail = UserEmail,
                UserAddress = UserAddress
            };
            using (var context = new ApplicationContext())
            {
                context.Users.Add(obj);
                context.SaveChanges();
                NotifyUpdates();
            }

            return RedirectToAction("Add");
        }

        public void NotifyUpdates()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            if (hubContext != null)
            {
                using (var context = new ApplicationContext())
                {
                    var stats = context.Users.ToList();
                    hubContext.Clients.All.updateUsers(stats);
                }
            }
        }
    }
}