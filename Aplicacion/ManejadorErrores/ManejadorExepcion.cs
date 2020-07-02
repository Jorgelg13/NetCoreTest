using System;
using System.Net;

namespace Aplicacion.ManejadorErrores
{
    public class ManejadorExepcion : Exception
    {
        public HttpStatusCode Codigo {get;} 
        public object Errores {get;}
        public ManejadorExepcion(HttpStatusCode codigo,object errores = null)
        {
            Codigo = codigo;
            Errores = errores;
        }
    }
}