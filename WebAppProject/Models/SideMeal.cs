﻿using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class SideMeal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string Name { get; set; }

    }
}