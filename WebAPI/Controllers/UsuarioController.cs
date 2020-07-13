using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Seguridad;

namespace WebAPI.Controllers
{
    public class UsuarioController : MiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros){
            return await Mediator.Send(parametros);
        }

    }
}