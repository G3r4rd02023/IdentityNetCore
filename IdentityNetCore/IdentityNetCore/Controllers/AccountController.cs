using IdentityNetCore.Data;
using IdentityNetCore.Data.Entities;
using IdentityNetCore.Data.Enums;
using IdentityNetCore.Migrations;
using IdentityNetCore.Models;
using IdentityNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServicioUsuario _usuario;
        private readonly DataContext _context;

        public AccountController(IServicioUsuario usuario, DataContext context)
        {
            _usuario = usuario;
            _context = context;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _usuario.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _usuario.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult Register()
        {
            AddUserViewModel model = new()
            {
                Id = Guid.Empty.ToString(),
                Rol = Rol.User,
                Contrasena = "123456",
                CorreoElectronico = "glanza007@gmail.com"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.CorreoElectronico = model.Username;
                model.Contrasena = model.Password;
                Usuario user = await _usuario.AddUserAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    return View(model);
                }
                LoginViewModel loginViewModel = new()
                {
                    Password = model.Password,
                    RememberMe = false,
                    Username = model.Username
                };
                var result2 = await _usuario.LoginAsync(loginViewModel);
                if (result2.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}