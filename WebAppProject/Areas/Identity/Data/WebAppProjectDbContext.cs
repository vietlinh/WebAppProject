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
        builder.Entity<RegisterMealInfo>()
            .HasOne(m => m.AppUser)
            .WithMany(n => n.RegisterMealInfos)
            .HasForeignKey(x => x.User_Id);
        builder.Entity<BasicMeal>()
            .HasOne(m => m.AppUser)
            .WithMany(n => n.BasicMeals)
            .HasForeignKey(x => x.Creator_id);
        builder.Entity<SideMeal>()
            .HasOne(m => m.AppUser)
            .WithMany(n => n.SideMeals)
            .HasForeignKey(x => x.Creator_id);
        builder.Entity<MainMeal>()
            .HasOne(m => m.AppUser)
            .WithMany(n => n.MainMeals)
            .HasForeignKey(x => x.Creator_id);
    }
}
