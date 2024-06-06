namespace WebAppProject.ViewModels
{
    public class CreateMealVM
    {
        
        public string? BasicMealName1 { get; set; }
        public string? BasicMealName2 { get; set; }
        public string? BasicMealName3 { get; set; }

        public string? MainMealName1 { get; set; }
        public string? MainMealName2 { get; set; }
        public string? MainMealName3 { get; set; }

        public string? SideMealName1 { get; set; }
        public string? SideMealName2 { get; set; }
        public string? SideMealName3 { get; set; }
        public DateTime CreateTime { get; set; }
        public string? Day { get; set; } = null!;
        public string? Creator_Id { get; set; }
        public string? Week { get; set; }



    }
}
