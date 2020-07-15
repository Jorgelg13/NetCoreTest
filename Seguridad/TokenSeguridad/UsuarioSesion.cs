using System.Linq;
using Aplicacion.Contratos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Seguridad
{
    public class UsuarioSesion :IUsuarioSesion
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioSesion(IHttpContextAccessor httpContextAccessor){
            _httpContextAccessor =httpContextAccessor;
        }
        public string ObtenerUsuarioSesion(){
            var user = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault( x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return user;
            
        }
    }
}