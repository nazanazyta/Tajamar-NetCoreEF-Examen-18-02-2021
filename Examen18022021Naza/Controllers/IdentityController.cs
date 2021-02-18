using Examen18022021Naza.Filters;
using Examen18022021Naza.Models;
using Examen18022021Naza.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Examen18022021Naza.Controllers
{
    public class IdentityController : Controller
    {
        IRepository repo;

        public IdentityController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String email, String pass)
        {
            Usuario usu = this.repo.Validar(email, pass);
            if (usu == null)
            {
                return View();
            }
            else
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usu.IdUsuario.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, usu.Nombre));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddMinutes(2)
                    });
                String action = TempData["action"].ToString();
                String controller = TempData["controller"].ToString();
                return RedirectToAction(action, controller);
            }
        }

        [AuthorizeUser]
        public IActionResult Detalles()
        {
            return View(this.repo.GetUsuario(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
