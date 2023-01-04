namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }


        public List<Comentario> comentarios { get; set; }

        



    }
}
