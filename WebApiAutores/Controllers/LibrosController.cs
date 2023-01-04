using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
   [ Route("api/libros")]
    public class LibrosController:ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {

            this.context= context;
            this.mapper = mapper;
        }



        //obtener los libros por ID

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDto>> Get(int id)
        {

          var libro =  await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<LibroDto>(libro);
        }

        //context para agregar ala bd
        //agregar libros


        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTo libroCreacionDTo)
        {

          
            var libro = mapper.Map<Libro>(libroCreacionDTo);



            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
