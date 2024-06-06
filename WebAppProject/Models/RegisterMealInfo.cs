using System.ComponentModel.DataAnnotations;
using WebAppProject.Areas.Identity.Data;

namespace WebAppProject.Models
{
    public class RegisterMealInfo
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public DateTime Register_Time { get; set; }
        public string? week_register { get; set; }


        public string User_Id { get; set; }
        public AppUser AppUser { get; set; }

    }
}
