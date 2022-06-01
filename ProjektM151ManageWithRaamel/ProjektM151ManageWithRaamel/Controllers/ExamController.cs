using ProjektM151ManageWithRaamel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektM151ManageWithRaamel.Controllers
{
    public class ExamController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Shows the exams of the user depending on which semester he wants to see
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public ActionResult ExamView(int semester = 0)
        {
            //Get the user from the database depending on who is logged in
            //ApplicationUser userfromDb = new ApplicationUser();
            //userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            //semester == 0 means the person wants to see all exams
            if (semester == 0)
            {
                //Returns list with all exams and newest exam at the top
                IEnumerable<Exam> model = db.Exam.OrderBy(r => r.Examdate).ToList();
                if (model == null)
                {
                    model = new List<Exam>();
                }
                return View(model);
            }
            else
            {
                //Returns list with same user and semester from dropdownlist
                IEnumerable<Exam> model = db.Exam.Where(s => s.Semester == semester).OrderBy(r => r.Examdate).ToList();
                if (model == null)
                {
                    model = new List<Exam>();
                }
                return View(model);
            }
        }
        /// <summary>
        /// Shows all exams
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowExam(int id = 0)
        {
            //Gets subject from database
            Subject subject = db.Subject.Find(id);

            return View(subject.Exam);
        }
        /// <summary>
        /// Shows the average of all exams the logged on user has
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAllGradeAverage()
        {
            List<Subject> model = new List<Subject>();
            //Get the user from the database depending on who is logged in
            ApplicationUser userfromDb = new ApplicationUser();
            userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            //List with exams with same subject and same user.
            List<Subject> subject = db.Subject.Where(s => s.User.Id == userfromDb.Id).ToList();
            foreach (Subject asubject in subject)
            {
                float average = 0;
                float counter = 0;
                IEnumerable<Exam> exam = db.Exam.Where(r => r.SubjectId == asubject.Id).ToList();

                if (asubject.Exam.Count != 0)
                {
                    foreach (Exam aexam in exam)
                    {
                        for (int i = 0; i < aexam.GradesIndex; i++)
                        {
                            average += aexam.Grade;
                        }
                        counter += aexam.GradesIndex;
                    }
                    asubject.Average = average / counter;
                    Subject sub = db.Subject.FirstOrDefault(s => s.Id == asubject.Id && s.User.Id == userfromDb.Id);
                    sub.Average = asubject.Average;
                    db.SaveChanges();
                }
                else
                {
                    asubject.Average = 0;
                }
                model.Add(asubject);
            }

            return View(model);
        }
        /// <summary>
        /// Shows the average of one subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowGradeAverage(int id = 0)
        {
            Subject subject = db.Subject.Find(id);
            Subject returnValue = new Subject();
            //Gets subject from db with the same subjectid
            IEnumerable<Exam> model = db.Exam.Where(r => r.SubjectId == subject.Id).ToList();
            float average = 0;
            float counter = 0;
            if (model != null)
            {
                foreach (Exam aExam in model)
                {
                    for (int i = 0; i < aExam.GradesIndex; i++)
                    {
                        average += aExam.Grade;
                    }
                    counter += aExam.GradesIndex;
                }
                returnValue.Average = average / counter;
            }
            else
            {
                returnValue.Average = 0;
            }
            return View(returnValue);
        }
        /// <summary>
        /// Returns View with Exam Model to fill out for creating a new Exam
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            Exam exam = db.Exam.Find(id);
            ExamModelView examModelView = new ExamModelView();

            if (exam != null)
            {
                examModelView.Id = exam.Id;
                examModelView.Description = exam.Description;
                examModelView.Grade = exam.Grade;
                examModelView.Examdate = exam.Examdate;
                examModelView.Semester = exam.Semester;
                examModelView.GradesIndex = exam.GradesIndex;
                examModelView.SubjectId = exam.SubjectId;
            }
            examModelView.AllSubjects = db.Subject.OrderBy(t => t.Name).ToList();

            return View(examModelView);
        }
        /// <summary>
        /// When pressing the Save Button it saves the model in to the database
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exam exam)
        {
            if (ModelState.IsValid)
            {
                //only edited
                if (exam.Id > 0)
                { //The exam object is getting manual reassigned that it can be saved modified.
                    Exam examFromDb = db.Exam.Find(exam.Id);
                    examFromDb.Description = exam.Description;
                    examFromDb.Grade = exam.Grade;
                    examFromDb.Examdate = exam.Examdate;
                    examFromDb.Semester = exam.Semester;
                    examFromDb.GradesIndex = exam.GradesIndex;
                    examFromDb.SubjectId = exam.SubjectId;

                }
                //Created a new exam
                else
                {
                    //Get the user from the database depending on who is logged in
                    ApplicationUser userfromDb = new ApplicationUser();
                    userfromDb = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    exam.User = userfromDb;
                    exam.User.Id = userfromDb.Id;
                    db.Exam.Add(exam);
                }
                db.SaveChanges();
                return RedirectToAction("ExamView", "Exam");
            }
            return View(exam);
        }
        /// <summary>
        /// When pressing the delete button, the exam will be removed from the database
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Exam exam)
        {
            //The Exam has to be reloaded 
            Exam examToDelete = db.Exam.Find(exam.Id);
            //Removes exam from database
            db.Exam.Remove(examToDelete);
            db.SaveChanges(); return RedirectToAction("ExamView", "Exam");
        }

        protected override void Dispose(bool disposing)
        {
            //If the db class is still instantized -> delete 
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}