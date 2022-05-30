using ProjektM151ManageWithRaamel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektM151ManageWithRaamel.Controllers
{
    public class SubjectController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Subject
        public ActionResult Index()
        {
            //Get the user from the database depending on who is logged in

            //ApplicationUser userfromDb = new ApplicationUser();
            //userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            //Returns list with only subjects with same user
            List<Subject> subjects = db.Subject.ToList();
            return View(subjects);
        }

        public ActionResult Edit(int id = 0)
        {
            Subject subject = db.Subject.Find(id);

            return View(subject);
        }

        //Save the Subject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                //If subject only needs editing
                if (subject.Id > 0)
                { // To save the edited subject manual
                    Subject subjectFromDb = db.Subject.Find(subject.Id);
                    subjectFromDb.Name = subject.Name;

                    //subjectFromDb.User.Id = User.Identity.Name;
                }
                else
                //When adding a new subject
                {
                    //Get the user from the database depending on who is logged in

                    //ApplicationUser userfromDb = new ApplicationUser();
                    //userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    //subject.User = userfromDb;
                    //subject.User.Id = userfromDb.Id;
                    db.Subject.Add(subject);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Subject");
            }
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Subject subject)
        {
            Subject subjectToDelete = db.Subject.Find(subject.Id);
            db.Subject.Remove(subjectToDelete);
            db.SaveChanges();
            return RedirectToAction("Index", "Subject");
        }

        protected override void Dispose(bool disposing)
        {
            //Falls die DB Klasse noch instantiert ist -> vernichten 
            if (db != null)
            {
                db.Dispose();
            }
            //Dispose vom Controller soll auch noch ausgeführt werden. 
            base.Dispose(disposing);
        }
    }
}