﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektM151.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bitte geben Sie einen Namen ein.")]
        public string Name { get; set; }
        public float Average { get; set; }
    }
}