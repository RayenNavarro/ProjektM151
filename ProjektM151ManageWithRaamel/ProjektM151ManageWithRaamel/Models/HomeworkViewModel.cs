using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektM151ManageWithRaamel.Models
{
    public class HomeworkViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public int Semester { get; set; }
        public ApplicationUser User { get; set; }
        public int SubjectId { get; set; }
        public virtual ICollection<Subject> AllSubjects { get; set; }
    }
}