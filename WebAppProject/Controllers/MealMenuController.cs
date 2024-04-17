using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public MealMenuController(WebAppProjectDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var register_user = _context.registerMealInfos.FirstOrDefault(u => u.Id_user == user.Id);

            MealMenuVM mealmenu = new MealMenuVM();
            mealmenu.SideMeal = await _context.sideMeals.ToListAsync();
            mealmenu.MainMeal = await _context.mainMeals.ToListAsync();
            mealmenu.BasicMeal = await _context.basicMeals.ToListAsync();

            if (register_user != null)
            {
                mealmenu.Monday = register_user.Monday;
                mealmenu.Tuesday = register_user.Tuesday;
                mealmenu.Wednesday = register_user.Wednesday;
                mealmenu.Thursday = register_user.Thursday;
                mealmenu.Friday = register_user.Friday;
            }

            return View(mealmenu);
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterMealInfo register_user)  
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var existingUser = _context.registerMealInfos.FirstOrDefault(u => u.Id_user == user.Id); 

            if (existingUser != null)
            {
                existingUser.Monday = register_user.Monday;
                existingUser.Tuesday = register_user.Tuesday;
                existingUser.Wednesday = register_user.Wednesday;
                existingUser.Thursday = register_user.Thursday;
                existingUser.Friday = register_user.Friday;
            }
            else
            {
                register_user.FullName = user.FullName;
                register_user.Id_user = user.Id;
                _context.Add(register_user);
            }

            await _context.SaveChangesAsync();
            MealMenuVM mealmenu = new MealMenuVM();
            mealmenu.SideMeal = await _context.sideMeals.ToListAsync();
            mealmenu.MainMeal = await _context.mainMeals.ToListAsync();
            mealmenu.BasicMeal = await _context.basicMeals.ToListAsync();
            return View(mealmenu);

        }
        [Authorize(Roles = "Admin")]

        public IActionResult CreateMainMeal()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult CreateBasicMeal()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult CreateSideMeal()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMeal()
        {
            MealMenuVM mealmenu1 = new MealMenuVM();
            mealmenu1.SideMeal = await _context.sideMeals.ToListAsync();
            mealmenu1.MainMeal = await _context.mainMeals.ToListAsync();
            mealmenu1.BasicMeal = await _context.basicMeals.ToListAsync();
            return View(mealmenu1);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
