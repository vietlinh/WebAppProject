using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Models
{
    public class RegisterMealInfo
    {
        [Key]
        public int Id { get; set; }
        public string Id_user { get; set; }
        public string FullName { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }

    }
}
