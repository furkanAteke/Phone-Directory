using Core.Abstracts.Services;
using Core.Dto;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Rehber.UI.Models;
using System.Diagnostics;

namespace Rehber.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService<User> _service;

        public HomeController(ILogger<HomeController> logger,IUserService<User> service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true"; 
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;

            var users = (await _service.GetAllAsync()).Select(r => new UserDto
            {
                Id = r.Id,
                Ad = r.Ad,
                Soyad = r.Soyad,
                Numara = r.Numara
            }).ToList();

            return View(users);
        }

        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            await _service.AddAsync(user);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            var user = await _service.GetByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login","Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            await _service.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            var user = await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            var user = await _service.Detail(id);
            return View(user);
        }
        public IActionResult About()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn") == "true";
            return View();
        }
    }
}
