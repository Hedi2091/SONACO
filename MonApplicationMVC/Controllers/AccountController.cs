using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MonApplicationMVC.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MonApplicationMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace MonApplicationMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.User_log.FirstOrDefaultAsync(u => u.user == model.Username);


            if (user != null && user.password == model.Password) // Comparaison sans hachage
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Username)
        };
                var claimsIdentity = new ClaimsIdentity(claims, "login");

                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Afficher un message d'erreur si le mot de passe est incorrect
                ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect.");
                return View(model);
            }
        }


    }
}
