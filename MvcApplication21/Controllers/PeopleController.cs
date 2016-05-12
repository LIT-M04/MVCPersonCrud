using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication21.Models;

namespace MvcApplication21.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index()
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            return View(manager.GetPeople());
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string firstName, string lastName, int age)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.Add(new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            });
            return Redirect("/people/index");
        }

        [HttpPost]
        public ActionResult Delete(int personId)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.Delete(personId);
            return Redirect("/people/index");
        }

        public ActionResult Edit(int personId)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            Person person = manager.GetPerson(personId);
            return View(person);
        }

        [HttpPost]
        public ActionResult Update(string firstName, string lastName, int age, int id)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.Edit(new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Id = id
            });
            return Redirect("/people/index");
        }

    }
}
