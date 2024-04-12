using System.ComponentModel.DataAnnotations;
using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class MealMenuVM
    {
        public List<BasicMeal> BasicMeal { get; set; }
        public List<SideMeal> SideMeal { get; set; }
        public List<MainMeal> MainMeal { get; set; }
        
    }
}
