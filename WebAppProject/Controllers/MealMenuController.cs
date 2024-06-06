using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
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
            DateTime today = DateTime.Today;
            int daysuntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime SaturdayThisWeek = today.AddDays(daysuntilSaturday);
            DateTime SaturdayWeekBefore = SaturdayThisWeek.AddDays(-7);
            DateTime SaturdayNextWeek = SaturdayThisWeek.AddDays(7);
            var get_day = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday_this_week = today.AddDays(-get_day);
            var sunday_this_week = monday_this_week.AddDays(6);
            var monday_next_week = monday_this_week.AddDays(7);
            var sunday_next_week = monday_next_week.AddDays(6);
            var register_user = _context.registerMealInfos.Where(s => s.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).FirstOrDefault(s => s.User_Id == user.Id);


            MealMenuVM mealmenu = new MealMenuVM();
            mealmenu.SideMeal = await _context.sideMeals.Where(s => s.week_create == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).ToListAsync();
            mealmenu.MainMeal = await _context.mainMeals.Where(s => s.week_create == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).ToListAsync();
            mealmenu.BasicMeal = await _context.basicMeals.Where(s => s.week_create == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).ToListAsync();
            mealmenu.user_id = user.Id;
            mealmenu.user_name = user.FullName;

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
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            DateTime today = DateTime.Today;
            var get_day = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday_this_week = today.AddDays(-get_day);
            var sunday_this_week = monday_this_week.AddDays(6);
            var monday_next_week = monday_this_week.AddDays(7);
            var sunday_next_week = monday_next_week.AddDays(6);

            var register_user = _context.registerMealInfos.Where(s => s.week_register == monday_next_week.ToString("dd/MM/yyyy") + "-" + sunday_next_week.ToString("dd/MM/yyyy")).FirstOrDefault(s => s.User_Id == user.Id);


            MealMenuVM mealmenu = new MealMenuVM();
            mealmenu.SideMeal = await _context.sideMeals.Where(s => s.week_create == monday_next_week.ToString("dd/MM/yyyy") + "-" + sunday_next_week.ToString("dd/MM/yyyy")).ToListAsync();
            mealmenu.MainMeal = await _context.mainMeals.Where(s => s.week_create == monday_next_week.ToString("dd/MM/yyyy") + "-" + sunday_next_week.ToString("dd/MM/yyyy")).ToListAsync();
            mealmenu.BasicMeal = await _context.basicMeals.Where(s => s.week_create == monday_next_week.ToString("dd/MM/yyyy") + "-" + sunday_next_week.ToString("dd/MM/yyyy")).ToListAsync();
            mealmenu.user_id = user.Id;
            mealmenu.user_name = user.FullName;

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
            DateTime today = DateTime.Today;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var get_day = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday_this_week = today.AddDays(-get_day);
            var sunday_this_week = monday_this_week.AddDays(6);
            var monday_next_week = monday_this_week.AddDays(7);
            var sunday_next_week = monday_next_week.AddDays(6);
            var existingUser = _context.registerMealInfos.Where(s => s.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).FirstOrDefault(u => u.User_Id == user.Id); 

            if (existingUser != null)
            {
                existingUser.Register_Time = DateTime.Now;
                existingUser.Monday = register_user.Monday;
                existingUser.Tuesday = register_user.Tuesday;
                existingUser.Wednesday = register_user.Wednesday;
                existingUser.Thursday = register_user.Thursday;
                existingUser.Friday = register_user.Friday;
            }
            else
            {
                register_user.Register_Time = DateTime.Now;
                register_user.FullName = user.FullName;
                register_user.User_Id = user.Id;
                register_user.week_register = monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy");
                _context.registerMealInfos.Add(register_user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Index2(RegisterMealInfo register_user)
        {
            var user =  await _userManager.GetUserAsync(HttpContext.User);
            DateTime today = DateTime.Now;
            var get_day = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday_this_week = today.AddDays(-get_day);
            var sunday_this_week = monday_this_week.AddDays(6);
            var monday_next_week = monday_this_week.AddDays(7);
            var sunday_next_week = monday_next_week.AddDays(6);
            var existingUser = _context.registerMealInfos.Where(s => s.week_register == monday_next_week.ToString("dd/MM/yyyy") + "-" + sunday_next_week.ToString("dd/MM/yyyy")).FirstOrDefault(u => u.User_Id == user.Id);

            if (existingUser != null)
            {
                existingUser.Register_Time = DateTime.Now;
                existingUser.Monday = register_user.Monday;
                existingUser.Tuesday = register_user.Tuesday;
                existingUser.Wednesday = register_user.Wednesday;
                existingUser.Thursday = register_user.Thursday;
                existingUser.Friday = register_user.Friday;
            }
            if (existingUser == null)
            {
                register_user.Register_Time = DateTime.Now;
                register_user.FullName = user.FullName;
                register_user.User_Id = user.Id;
                register_user.week_register = monday_next_week.ToString("dd/MM/yyyy") + "-" + sunday_next_week.ToString("dd/MM/yyyy");
                _context.registerMealInfos.Add(register_user);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index2");
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                mainmeal.CreateTime = DateTime.Now;
                mainmeal.Creator_id = user.Id;
                _context.Add(mainmeal);
                await _context.SaveChangesAsync();
            }
            return View();
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                basicmeal.CreateTime = DateTime.Now;
                basicmeal.Creator_id = user.Id;
                _context.Add(basicmeal);
                await _context.SaveChangesAsync();
            }
            return View();
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                sidemeal.CreateTime = DateTime.Now;
                sidemeal.Creator_id = user.Id;
                _context.Add(sidemeal);
                await _context.SaveChangesAsync();
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMainMeal(int? id)
        {

            DateTime today = DateTime.Today;
            var get_day = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday_this_week = today.AddDays(-get_day);
            var sunday_this_week = monday_this_week.AddDays(6);
            var monday_next_week = monday_this_week.AddDays(7);
            var sunday_next_week = monday_next_week.AddDays(6);
            MealMenuVM mealmenu1 = new MealMenuVM();
            int pageSize = 10;
            if (id == null)
            {
                id = 1;
            }
            int currentPage = id.Value;
            mealmenu1.current_page = currentPage;
            mealmenu1.mainMeal_count = await _context.mainMeals.CountAsync();
            mealmenu1.MainMeal = await _context.mainMeals.OrderBy(p => p.week_create).ThenBy(p => p.Day).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            mealmenu1.monday_count = await _context.registerMealInfos.Where(p => p.Monday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.tuesday_count = await _context.registerMealInfos.Where(p => p.Tuesday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.wednesday_count = await _context.registerMealInfos.Where(p => p.Wednesday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.thursday_count = await _context.registerMealInfos.Where(p => p.Thursday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.friday_count = await _context.registerMealInfos.Where(p => p.Friday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            
            return View(mealmenu1);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditSideMeal(int? id)
        {
            DateTime today = DateTime.Today;
            var get_day = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime monday_this_week = today.AddDays(-get_day);
            var sunday_this_week = monday_this_week.AddDays(6);
            var monday_next_week = monday_this_week.AddDays(7);
            var sunday_next_week = monday_next_week.AddDays(6);
            MealMenuVM mealmenu1 = new MealMenuVM();
            int pageSize = 10;
            if (id == null)
            {
                id = 1;
            }
            int currentPage = id.Value;
            mealmenu1.current_page = currentPage;
            mealmenu1.sideMeal_count = await _context.sideMeals.CountAsync();
            mealmenu1.SideMeal = await _context.sideMeals.OrderBy(p => p.week_create).ThenBy(p => p.Day).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            mealmenu1.monday_count = await _context.registerMealInfos.Where(p => p.Monday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.tuesday_count = await _context.registerMealInfos.Where(p => p.Tuesday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.wednesday_count = await _context.registerMealInfos.Where(p => p.Wednesday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.thursday_count = await _context.registerMealInfos.Where(p => p.Thursday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.friday_count = await _context.registerMealInfos.Where(p => p.Friday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();

            return View(mealmenu1);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditBasicMeal(int? id)
        {
            DateTime today = DateTime.Today;
            var get_day = ((int)today.DayOfWeek -(int)DayOfWeek.Monday + 7) % 7;
            DateTime monday_this_week = today.AddDays(-get_day);
            var sunday_this_week = monday_this_week.AddDays(6);
            var monday_next_week = monday_this_week.AddDays(7);
            var sunday_next_week = monday_next_week.AddDays(6);
            MealMenuVM mealmenu1 = new MealMenuVM();
            int pageSize = 10;
            if (id == null)
            {
                id = 1;
            }
            int currentPage = id.Value;
            mealmenu1.current_page = currentPage;
            mealmenu1.basicMeal_count = await _context.basicMeals.CountAsync();
            mealmenu1.BasicMeal = await _context.basicMeals.OrderBy(p => p.week_create).ThenBy(p => p.Day).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            mealmenu1.monday_count = await _context.registerMealInfos.Where(p => p.Monday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.tuesday_count = await _context.registerMealInfos.Where(p => p.Tuesday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.wednesday_count = await _context.registerMealInfos.Where(p => p.Wednesday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.thursday_count = await _context.registerMealInfos.Where(p => p.Thursday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();
            mealmenu1.friday_count = await _context.registerMealInfos.Where(p => p.Friday == true && p.week_register == monday_this_week.ToString("dd/MM/yyyy") + "-" + sunday_this_week.ToString("dd/MM/yyyy")).CountAsync();

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
            return RedirectToAction(nameof(EditMainMeal));
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
                return RedirectToAction(nameof(EditMainMeal));
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
                return RedirectToAction(nameof(EditMainMeal));
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteAll()
        {
            var basicMealModel = await _context.basicMeals.ToListAsync();
            var mainMealModel = await _context.mainMeals.ToListAsync();
            var sideMealModel = await _context.sideMeals.ToListAsync();
            _context.sideMeals.RemoveRange(sideMealModel);
            _context.mainMeals.RemoveRange(mainMealModel);
            _context.basicMeals.RemoveRange(basicMealModel);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EditMainMeal));

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> delete_register_all()
        {
            var registerMealModel = await _context.registerMealInfos.ToListAsync();
            _context.registerMealInfos.RemoveRange(registerMealModel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EditMainMeal));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateMeal()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateMeal(CreateMealVM createmeal)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                var currentTime = DateTime.Now;

                // Adding BasicMeals
                if (createmeal.BasicMealName1 != null)
                {
                    var basicmeal1 = new BasicMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.BasicMealName1,
                        CreateTime = currentTime,
                        week_create = createmeal.Week,
                    };
                    _context.Add(basicmeal1);
                }
                if (createmeal.BasicMealName2 != null)
                {
                    var basicmeal2 = new BasicMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.BasicMealName2,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(basicmeal2);
                }
                if (createmeal.BasicMealName3 != null)
                {
                    var basicmeal3 = new BasicMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.BasicMealName3,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(basicmeal3);
                }

                // Adding SideMeals
                if (createmeal.SideMealName1 != null)
                {
                    var sidemeal1 = new SideMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.SideMealName1,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(sidemeal1);
                }
                if (createmeal.SideMealName2 != null)
                {
                    var sidemeal2 = new SideMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.SideMealName2,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(sidemeal2);
                }
                if (createmeal.SideMealName3 != null)
                {
                    var sidemeal3 = new SideMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.SideMealName3,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(sidemeal3);
                }

                // Adding MainMeals
                if (createmeal.MainMealName1 != null)
                {
                    var mainmeal1 = new MainMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.MainMealName1,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(mainmeal1);
                }
                if (createmeal.MainMealName2 != null)
                {
                    var mainmeal2 = new MainMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.MainMealName2,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(mainmeal2);
                }
                if (createmeal.MainMealName3 != null)
                {
                    var mainmeal3 = new MainMeal
                    {
                        Day = createmeal.Day,
                        Creator_id = user.Id,
                        Name = createmeal.MainMealName3,
                        CreateTime = currentTime,
                        week_create = createmeal.Week
                    };
                    _context.Add(mainmeal3);
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception and show an error message to the user
                    Console.WriteLine($"DbUpdateException: {ex.InnerException?.Message}");
                    ModelState.AddModelError("", "Unable to save changes to the database. Please check the input values.");
                    return View(createmeal);
                }
            }

            return RedirectToAction("Index");
            
        }

    }
}
