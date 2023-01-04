using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class AutorCreacionDTO
    {

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 20, ErrorMessage = "el campo {0} no puede ser de mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

    }   
}
