using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor : IValidatableObject  //sirve para implementar reglas a nivel de clase
    {


        public int Id { get; set; }


        //se establecen reglas alas variables*****
        [Required(ErrorMessage ="El campo nombre es requerido")]
        [StringLength(maximumLength:20, ErrorMessage ="el campo {0} no puede ser de mas de {1} caracteres") ]
       // [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        //[Range(18,30)]
        //[NotMapped]
        //public int edad { get; set; }

        //[NotMapped]
        //public int Mayor { get; set; }

        //[NotMapped]
        //public int Menor { get; set; }
        public List<Libro> Libros { get; set; }



        //metodo implementao por IValidatableObject
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe de ser mayuscula", 
                        new string[] { nameof(Nombre) });
                }
            } 


            //********************

            //if(Menor > Mayor) 
            //{
            //    yield return new ValidationResult($"el numero {Menor} no puede ser mayor que el numero {Mayor}",
            //        new string[] { nameof(Menor) });
            
            //}
            

        
        }
   
    
    
    }
}
