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

        Task<IdentityResult> ChangePasswordAsync(Usuario user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(Usuario user);

        Task<Usuario> GetUserAsync(Guid userId);

        Task<string> GenerateEmailConfirmationTokenAsync(Usuario user);

        Task<IdentityResult> ConfirmEmailAsync(Usuario user, string token);

        Task<string> GeneratePasswordResetTokenAsync(Usuario user);

        Task<IdentityResult> ResetPasswordAsync(Usuario user, string token, string password);
    }
}