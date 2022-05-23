using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektM151ManageWithRaamel.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int Semester { get; set; }
        public DateTime Examdate { get; set; }
        public int GradesIndex { get; set; }
        public ApplicationUser User { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}