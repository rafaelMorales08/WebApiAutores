using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebApiAutores.Entidades;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/autores")] //ruta
    public class AutoresController : ControllerBase
    {
        
        private readonly ApplicationDbContext context;
        private readonly IServicio servicio;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, IServicio servicio, ILogger<AutoresController> logger)
        {

            this.context = context;
            this.servicio = servicio;
            this.logger = logger;
        }


        //retonar una lista de autores**


        //dato especifico que no tiene que ver con ActionRsult
        //usando programacion sincrona - no asincrona

        [HttpGet] //api/autores
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")] // listado
        public async Task<List<Autor>> Get()
        {
            logger.LogInformation("Estiy accediendo a al lista de autores");
            return  await context.Autores.Include(x=> x.Libros).ToListAsync();

        }

        //obtener el primer autor
        [HttpGet("primerId")]
        public async Task<ActionResult<Autor>> primerAutor([FromHeader] int miValor)
        {
            return await context.Autores.FirstOrDefaultAsync();
        }


        [HttpGet("primerId2")]
        public ActionResult<Autor> primerAutor2()
        {
            return new Autor()
            {
                Nombre="Inventado"
            };
        }




        //obtener los datos de autores por id 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id) 
        { 
            var existe =  await context.Autores.FirstOrDefaultAsync(x=> x.Id == id);
            if (existe == null)
            {
                return NotFound();
            }


            return existe;
        }


        //obtener los datos de autores por nombre
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get([FromRoute] string nombre)
        {
            var existe = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
            if (existe == null)
            {
                return NotFound();
            }


            return existe;
        }





        //crear una lista de autores

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {

            //validaciones o reglas contra la BD
            var existeElAutorConElMismoNombre =  await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            if (existeElAutorConElMismoNombre)
            {
                return BadRequest($"ya existe un autor con el nombre {autor.Nombre}");
            }

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();

        }

        //actualizar una lista de autores

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("el id no coincide con el que desea modificar");
            }


            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delate(int id)
        {

            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor { Id = id });
            await context.SaveChangesAsync();
            return Ok(); 
        }

    }
}