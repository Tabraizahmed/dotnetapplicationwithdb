using DummyApplication.Database;
using DummyApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            }

            return RedirectToAction("Index");
        }
    }
}