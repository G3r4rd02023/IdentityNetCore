using IdentityNetCore.Data.Entities;
using IdentityNetCore.Data.Enums;
using IdentityNetCore.Services;

namespace IdentityNetCore.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IServicioUsuario _usuario;

        public SeedDb(DataContext context, IServicioUsuario usuario)
        {
            _context = context;
            _usuario = usuario;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckMoviesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("SuperAdmin", "tecnologershn@gmail.com", "123456");
        }

        private async Task CheckMoviesAsync()
        {
            if (!_context.Peliculas.Any())
            {
                _context.Peliculas.Add(new Pelicula
                {
                    Titulo = "Titanic",
                    Autor = "James Cameron",
                    FechaPublicacion = DateTime.Today.AddYears(-25),
                    Genero = "Drama"
                });

                _context.Peliculas.Add(new Pelicula
                {
                    Titulo = "Alien",
                    Autor = "Ridley Scott",
                    FechaPublicacion = DateTime.Today.AddYears(-35),
                    Genero = "Terror"
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task<Usuario> CheckUserAsync(string nombre, string correo, string password)
        {
            Usuario user = await _usuario.GetUserAsync(correo);
            if (user == null)
            {
                user = new Usuario
                {
                    Nombre = nombre,
                    UserName = correo,
                    CorreoElectronico = correo,
                    Contrasena = password,
                    Email = correo,
                    Rol = Rol.Admin
                };
                await _usuario.AddUserAsync(user, "123456");
                await _usuario.AddUserToRoleAsync(user, Rol.Admin.ToString());

                string token = await _usuario.GenerateEmailConfirmationTokenAsync(user);
                await _usuario.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _usuario.CheckRoleAsync(Rol.Admin.ToString());
            await _usuario.CheckRoleAsync(Rol.User.ToString());
        }
    }
}