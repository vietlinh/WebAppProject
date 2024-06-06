using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WebAppProject.Models;

namespace WebAppProject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? Phone {  get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public DateOnly? DateBirth { get; set; }
    public DateTime CreateTime { get; set; }
    public ICollection<RegisterMealInfo>? RegisterMealInfos { get; set; }
    public ICollection<BasicMeal>? BasicMeals { get; set; }
    public ICollection<SideMeal>? SideMeals { get; set; }
    public ICollection<MainMeal>? MainMeals { get; set; }



}

