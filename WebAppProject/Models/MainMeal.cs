using System.ComponentModel.DataAnnotations;
using WebAppProject.Areas.Identity.Data;

namespace WebAppProject.Models
{
    public class MainMeal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Day { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public string? week_create { get; set; }

        public string? Creator_id { get; set; } = null!;
        public AppUser? AppUser { get; set; }
    }
}
