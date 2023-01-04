﻿using Microsoft.Extensions.Logging;

namespace WebApiAutores.Middleware
{


    public static class LoguearRespuestaHTTPMiddlewareExtensions
    {

        public static IApplicationBuilder useLoguearRespuestaHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
        }

    
    }





    public class LoguearRespuestaHTTPMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LoguearRespuestaHTTPMiddleware> logger;

        public LoguearRespuestaHTTPMiddleware(RequestDelegate siguiente,
             ILogger<LoguearRespuestaHTTPMiddleware> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }


        //invoke o invokeASYNC

        public async Task InvokeAsync(HttpContext contexto)
        {

            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;


                await siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();

                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoOriginalRespuesta);
                contexto.Response.Body = cuerpoOriginalRespuesta;

                logger.LogInformation(respuesta);
            }


        }


    }
}
