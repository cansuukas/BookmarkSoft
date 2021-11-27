using BookmarkSoft.Core.Service;
using BookmarkSoft.Model.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookmarkSoft.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICoreService<User> _us;

        public AccountController(ICoreService<User> us)
        {
            _us = us;
        }

       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

     
        [HttpPost]
        public async Task<IActionResult> Login(User item)
        {
            if (_us.Any(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password))
            {
                var logged = _us.GetByDefault(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password);

                var claims = new List<Claim>()
                {
                    new Claim("ID", logged.ID.ToString()),
                    new Claim(ClaimTypes.Name, logged.FirstName),
                    new Claim(ClaimTypes.Surname, logged.LastName),
                    new Claim(ClaimTypes.Email, logged.EmailAddress),
                    new Claim("Image", logged.ImageUrl)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home", new { area = "Administrator" });
            }

            return View(item);
        }

       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
    }
}
