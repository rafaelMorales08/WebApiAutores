using AutoMapper;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfiles: Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTo>();

            CreateMap<LibroCreacionDTo, Libro>();
            CreateMap<Libro,LibroDto>();
            CreateMap<ComentarioCreacionDTo, Comentario>();
            CreateMap<Comentario, ComentarioDTo>();
        }


    }
}
