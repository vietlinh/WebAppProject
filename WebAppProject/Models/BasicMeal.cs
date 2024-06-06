using System.ComponentModel.DataAnnotations;
using WebAppProject.Areas.Identity.Data;

namespace WebAppProject.Models
{
    public class BasicMeal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string? week_create { get; set; }

        public string? Creator_id { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
