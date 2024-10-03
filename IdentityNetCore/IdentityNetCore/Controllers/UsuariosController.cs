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
        private readonly IServicioCorreo _correo;

        public UsuariosController(IServicioUsuario usuario, DataContext context, IServicioCorreo correo)
        {
            _usuario = usuario;
            _context = context;
            _correo = correo;
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
                string myToken = await _usuario.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme)!;

                Response response = _correo.SendMail(
               $"{model.Nombre}",
                model.Username,
                "Tecnologers - Confirmación de Email",
                $"<h1>Tecnologers - Confirmación de Email</h1>" +
                $"Para habilitar el usuario por favor hacer clic en el siguiente link:, " +
                $"<p><a href = \"{tokenLink}\">Confirmar Email</a></p>");
                if (response.IsSuccess)
                {
                    ViewBag.Message = "Las instrucciones para habilitar el usuario han sido enviadas al correo.";
                    return View(model);
                }
                ModelState.AddModelError(string.Empty, response.Message!);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}