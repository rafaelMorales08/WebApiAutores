using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{

    [ApiController]
    [Route("api/autores")] //ruta
    public class AutoresController : ControllerBase
    {
        
        private readonly ApplicationDbContext context;
        private readonly IMapper maper;

        public AutoresController(ApplicationDbContext context, IMapper maper)
        {

            this.context = context;
            this.maper = maper;
        }


        //retonar una lista de autores**


        //dato especifico que no tiene que ver con ActionRsult
        //usando programacion sincrona - no asincrona

        [HttpGet] //api/autores
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")] // listado
        public async Task<List<AutorDTo>> Get()
        {
            var autores =   await context.Autores.ToListAsync();
            return maper.Map<List<AutorDTo>>(autores);

        }


        //obtener  autor po id
        [HttpGet("primerId")]
        public async Task<ActionResult<Autor>> primerAutor([FromHeader] int miValor)
        {
            return await context.Autores.FirstOrDefaultAsync();
        }





        //obtener los datos de autores por id 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDTo>> Get(int id) 
        { 
            var existe =  await context.Autores.FirstOrDefaultAsync(x=> x.Id == id);
            if (existe == null)
            {
                return NotFound();
            }

            return maper.Map<AutorDTo>(existe);
    
        }


        //obtener los datos de autores por nombre
        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<AutorDTo>>> Get([FromRoute] string nombre)
        {
            var existe = await context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();

            return maper.Map<List<AutorDTo>>(existe);
        }





        //crear una lista de autores

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacionDto)
        {

            //validaciones o reglas contra la BD
            var existeElAutorConElMismoNombre =  await context.Autores.AnyAsync(x => x.Nombre == autorCreacionDto.Nombre);

            if (existeElAutorConElMismoNombre)
            {
                return BadRequest($"ya existe un autor con el nombre {autorCreacionDto.Nombre}");
            }

            var autor = maper.Map<Autor>(autorCreacionDto);
            

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