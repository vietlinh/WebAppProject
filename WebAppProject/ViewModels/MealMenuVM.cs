using System.ComponentModel.DataAnnotations;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class MealMenuVM
    {
        public List<BasicMeal> BasicMeal { get; set; }
        public List<SideMeal> SideMeal { get; set; }
        public List<MainMeal> MainMeal { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get;set; }
        public bool Thursday { get; set;}
        public bool Friday { get; set;}
        public bool DisableMonday { get; set; }
        public bool DisableTuesday { get;set; }
        public bool DisableWednesday { get; set; }
        public bool DisableThursday { get; set; }
        public bool DisableFriday { get;set; }
        public string user_id { get; set; }
        public string user_name { get; set;}
        public int monday_count { get; set; }
        public int tuesday_count { get;set; }
        public int wednesday_count { get; set; }
        public int thursday_count { get; set; }
        public int friday_count { get; set;}
        
    }
}
