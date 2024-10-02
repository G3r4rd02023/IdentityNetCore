using IdentityNetCore.Data.Entities;
using IdentityNetCore.Migrations;
using IdentityNetCore.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityNetCore.Services
{
    public interface IServicioUsuario
    {
        Task<Usuario> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(Usuario user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(Usuario user, string roleName);

        Task<bool> IsUserInRoleAsync(Usuario user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<Usuario> AddUserAsync(AddUserViewModel model);
    }
}