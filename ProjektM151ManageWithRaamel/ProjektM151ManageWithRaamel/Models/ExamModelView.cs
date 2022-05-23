using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektM151ManageWithRaamel.Models
{
    public class ExamModelView
    {
        public int Id { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public DateTime Examdate { get; set; }
        [Required]
        public int GradesIndex { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        public int SubjectId { get; set; }

        public virtual ICollection<Subject> AllSubjects { get; set; }
    }
}