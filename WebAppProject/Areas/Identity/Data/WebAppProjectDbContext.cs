using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Areas.Identity.Data;
using WebAppProject.Models;

namespace WebAppProject.Areas.Identity.Data;

public class WebAppProjectDbContext : IdentityDbContext<AppUser>
{
    public WebAppProjectDbContext(DbContextOptions<WebAppProjectDbContext> options)
        : base(options)
    {
    }
    public DbSet<BasicMeal> basicMeals { get; set; }
    public DbSet<SideMeal> sideMeals { get; set; }
    public DbSet<MainMeal> mainMeals { get; set; }
    public DbSet<RegisterMealInfo> registerMealInfos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
