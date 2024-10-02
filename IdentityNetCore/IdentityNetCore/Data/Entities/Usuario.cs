using IdentityNetCore.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityNetCore.Data.Entities
{
    public class Usuario : IdentityUser
    {
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string CorreoElectronico { get; set; } = null!;

        [MaxLength(256, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Contrasena { get; set; } = null!;

        public Rol Rol { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}