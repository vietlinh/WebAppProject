using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppProject.Areas.Identity.Data;
using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly WebAppProjectDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> usermanager, WebAppProjectDbContext context)
        {
            _logger = logger;
            _userManager = usermanager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["Phone"] = user.Phone;
            ViewData["Address"] = user.Address;
            ViewData["FullName"] = user.FullName;
            ViewData["Email"] = user.Email;
            ViewData["Id"] = user.Id;
            ViewData["City"] = user.City;
            ViewData["Country"] = user.Country;
            ViewData["BirthDate"] = user.DateBirth;
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            id = user.Id;
            if (id == null)
            {
                return NotFound();
            }
            var userinfo = new AppUser
            {
                FullName = user.FullName,
                Phone = user.Phone,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                DateBirth = user.DateBirth
            };
            return View(userinfo);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,[Bind("FullName,Phone,Address,City,Country,DateBirth")] AppUser inputUser)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            id = user.Id;
            if (id == null)
            {
                return NotFound();
            }

            user.FullName = inputUser.FullName;
            user.Phone = inputUser.Phone;
            user.Address = inputUser.Address;
            user.City = inputUser.City;
            user.Country = inputUser.Country;
            user.DateBirth = inputUser.DateBirth;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(inputUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
