using Microsoft.AspNetCore.Mvc;

namespace WebApiAutores.Controllers
{
    [ApiController]
   [ Route("api/libros")]
    public class LibrosController:ControllerBase
    {

        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {

            this.context= context;
        
        }
    }
}
