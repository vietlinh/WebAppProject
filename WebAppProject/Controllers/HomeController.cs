using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> usermanager, WebAppProjectDbContext context,IUserStore<AppUser> userstore)
        {
            _logger = logger;
            _userManager = usermanager;
            _context = context;
            _userStore = userstore;
            _emailStore = GetEmailStore(); 
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            HomeIndexVM userModel = new HomeIndexVM
            {
                Address = user.Address,
                Email = user.Email,
                City = user.City,
                Id = user.Id,
                BirthDate = user.DateBirth,
                Phone = user.Phone,
                FullName = user.FullName,
                Country = user.Country
            };

            return View(userModel);
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

        [Authorize (Roles = "Admin")]
        [HttpGet]
        public IActionResult Create_Acc()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create_Acc(CreateUserVM inputUser)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FullName = inputUser.FullName,
                    Phone = inputUser.Phone,
                    DateBirth = inputUser.DateBirth,
                    Address = inputUser.Address,
                    City = inputUser.City,
                    Country = inputUser.Country,
                    EmailConfirmed = true,
                    CreateTime = DateTime.Now,
                };
                await _userStore.SetUserNameAsync(user, inputUser.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user,inputUser.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, inputUser.PassWord);

            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AccManager()
        {
            AccDisplayVM account = new AccDisplayVM
            {
                Users = await _context.Users.ToListAsync()
            };
            return View(account);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AccDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var user_role = await _userManager.GetRolesAsync(user);
            if (user != null && !user_role.Contains("Admin"))
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("AccManager");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]

        public async Task<IActionResult> AccDetail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("AccManager");
            }

            AccDetailVM user_detail = new AccDetailVM
            {
                FullName = user.FullName,
                BirthDate = user.DateBirth,
                Id = user.Id,
                Email = user.Email,
                Country = user.Country,
                City = user.City,
                Phone = user.Phone,
                Address = user.Address,
                CreateTime = user.CreateTime
            };
            return View(user_detail);
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
    }
}
