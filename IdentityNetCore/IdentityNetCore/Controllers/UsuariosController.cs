using IdentityNetCore.Data;
using IdentityNetCore.Data.Entities;
using IdentityNetCore.Data.Enums;
using IdentityNetCore.Migrations;
using IdentityNetCore.Models;
using IdentityNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityNetCore.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IServicioUsuario _usuario;
        private readonly DataContext _context;

        public UsuariosController(IServicioUsuario usuario, DataContext context)
        {
            _usuario = usuario;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            AddUserViewModel model = new()
            {
                Id = Guid.Empty.ToString(),
                Rol = Rol.Admin,
                Contrasena = "123456",
                CorreoElectronico = "glanza007@gmail.com"
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
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
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}