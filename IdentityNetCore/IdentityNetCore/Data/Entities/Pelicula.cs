using System.ComponentModel.DataAnnotations;

namespace IdentityNetCore.Data.Entities
{
    public class Pelicula
    {
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Autor { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Genero { get; set; } = null!;

        public DateTime FechaPublicacion { get; set; }
    }
}