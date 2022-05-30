using ProjektM151ManageWithRaamel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektM151ManageWithRaamel.Controllers
{
    public class HomeworkController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Homework
        public ActionResult Index() //When used?
        {
            //Get the user from the database depending on who is logged in

            //ApplicationUser userfromDb = new ApplicationUser();
            //userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            //Returns list with only homework with same user
            List<Homework> homework = db.Homework.ToList();
            return View(homework);
        }

        public ActionResult HomeworkView()
        {
            //Get the user from the database depending on who is logged in

            //ApplicationUser userfromDb = new ApplicationUser();
            //userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            //Returns list with only homework with same user

            IEnumerable<Homework> model = db.Homework.ToList();
            if (model == null)
            {
                model = new List<Homework>();
            }
            return View(model);
        }

        public ActionResult ShowHomework(int id = 0)
        {
            Subject subject = db.Subject.Find(id);

            return View(subject.Homework);
        }

        public ActionResult Edit(int id = 0)
        {
            Homework homework = db.Homework.Find(id);
            HomeworkViewModel homeworkViewModel = new HomeworkViewModel();

            if (homework != null)
            {
                homeworkViewModel.Id = homework.Id;
                homeworkViewModel.Description = homework.Description;
                homeworkViewModel.DueDate = homework.DueDate;
                homeworkViewModel.Semester = homework.Semester;
                homeworkViewModel.Status = homework.Status;
                homeworkViewModel.SubjectId = homework.SubjectId;
            }
            homeworkViewModel.AllSubjects = db.Subject.OrderBy(t => t.Name).ToList();

            return View(homeworkViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Homework homework)
        {
            if (ModelState.IsValid)
            {
                //edited
                if (homework.Id > 0)
                {
                    Homework homeworkFromDb = db.Homework.Find(homework.Id);
                    homeworkFromDb.Description = homework.Description;
                    homeworkFromDb.DueDate = homework.DueDate;
                    homeworkFromDb.Semester = homework.Semester;
                    homeworkFromDb.Status = homework.Status;
                    homeworkFromDb.SubjectId = homework.SubjectId;

                }
                //newly created homework
                else
                {
                    //Get the user from the database depending on who is logged in

                    //ApplicationUser userfromDb = new ApplicationUser();
                    //userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    //homework.User = userfromDb;
                    //homework.User.Id = userfromDb.Id;

                    db.Homework.Add(homework);
                }
                db.SaveChanges();
                return RedirectToAction("HomeworkView", "Homework");
            }
            return View(homework);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Homework homework)
        {
            //Das Restaurant muss neu aus der Datenbank geladen werden, 
            // da es ursprünglich aus einem anderen DB Kontext geladen wurde. 
            Homework homeworkToDelete = db.Homework.Find(homework.Id);
            db.Homework.Remove(homeworkToDelete);
            db.SaveChanges(); return RedirectToAction("HomeworkView", "Homework");
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