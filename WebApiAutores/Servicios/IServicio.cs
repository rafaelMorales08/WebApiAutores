namespace WebApiAutores.Servicios
{
    public interface IServicio
    {
        public void realizarTarea();
    }


    public class ServicioA : IServicio
    {

        private readonly ILogger<ServicioA> logger;
        public ServicioA(ILogger<ServicioA> logger)
        { 
            this.logger = logger;
        }
        

        

        public void realizarTarea()
        {
            throw new NotImplementedException();
        }
    }


    public class ServicioB : IServicio
    {
        public void realizarTarea()
        {
            throw new NotImplementedException();
        }
    }


}
