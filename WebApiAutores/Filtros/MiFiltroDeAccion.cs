using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutores.Filtros
{
    public class MiFiltroDeAccion : IActionFilter
    {
        private readonly ILogger<MiFiltroDeAccion> loger;

        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> loger)
        {
            this.loger = loger;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            loger.LogInformation("Antes de ejecutar la accion");
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            loger.LogInformation("despues de ejecutar la accion");
        }

    }
}
