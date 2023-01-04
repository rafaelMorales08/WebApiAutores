using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios}")]
    public class ComentariosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context ,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult <List<ComentarioDTo>>> Get(int libroId)
        {
            var existeLiibro = await context.Libros.AnyAsync(libroBd => libroBd.Id == libroId);

            if (!existeLiibro)
            {

                return NotFound();

            }


            var comentario = await context.Comentarios
                .Where(ComentarioDB =>  ComentarioDB.LibroId == libroId).ToListAsync();

            return mapper.Map<List<ComentarioDTo>>(comentario);
        }



        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTo comentarioCreacionDTo)
        {
            var existeLiibro = await context.Libros.AnyAsync(libroBd => libroBd.Id == libroId);

            if (!existeLiibro)
            {

                return NotFound();

            }

            var comentario = mapper.Map<Comentario>(comentarioCreacionDTo);

            comentario.LibroId= libroId;
           context.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
