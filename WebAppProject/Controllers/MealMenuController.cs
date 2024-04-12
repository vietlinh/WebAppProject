using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Areas.Identity.Data;
using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    [Authorize]
    public class MealMenuController : Controller
    {
        private readonly WebAppProjectDbContext _context;
        public MealMenuController(WebAppProjectDbContext context)
        {
            _context  = context;    
        }
        public async Task<IActionResult> Index()
        {
            MealMenuVM mealmenu = new MealMenuVM();
            mealmenu.SideMeal = await _context.sideMeals.ToListAsync();
            mealmenu.MainMeal = await _context.mainMeals.ToListAsync();
            mealmenu.BasicMeal = await _context.basicMeals.ToListAsync();
            return View(mealmenu);
        }
        public IActionResult CreateMainMeal()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMainMeal(MainMeal mainmeal) 
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainmeal);
                await _context.SaveChangesAsync();
                return View();
            }
            return View(mainmeal);
        }
        public IActionResult CreateBasicMeal()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBasicMeal(BasicMeal basicmeal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basicmeal);
                await _context.SaveChangesAsync();
                return View();
            }
            return View(basicmeal);
        }
        public IActionResult CreateSideMeal()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSideMeal(SideMeal sidemeal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sidemeal);
                await _context.SaveChangesAsync();
                return View();
            }
            return View(sidemeal);
        }

        public async Task<IActionResult> EditMeal()
        {
            MealMenuVM mealmenu1 = new MealMenuVM();
            mealmenu1.SideMeal = await _context.sideMeals.ToListAsync();
            mealmenu1.MainMeal = await _context.mainMeals.ToListAsync();
            mealmenu1.BasicMeal = await _context.basicMeals.ToListAsync();
            return View(mealmenu1);
        }
        [HttpPost]
        public async Task<IActionResult> BasicMealDelete(int id)
        {
            {
            var userInfoModel = await _context.basicMeals.FindAsync(id);
            if (userInfoModel != null)
            {
                _context.basicMeals.Remove(userInfoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EditMeal));
            }
        }
        [HttpPost]
        public async Task<IActionResult> MainMealDelete(int id)
        {
            {
                var userInfoModel = await _context.mainMeals.FindAsync(id);
                if (userInfoModel != null)
                {
                    _context.mainMeals.Remove(userInfoModel);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EditMeal));
            }
        }
        [HttpPost]
        public async Task<IActionResult> SideMealDelete(int id)
        {
            {
                var userInfoModel = await _context.sideMeals.FindAsync(id);
                if (userInfoModel != null)
                {
                    _context.sideMeals.Remove(userInfoModel);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EditMeal));
            }
        }
    }
}
