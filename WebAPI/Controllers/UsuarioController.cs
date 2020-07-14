using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Seguridad;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros){
            return await Mediator.Send(parametros);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros){
            return await Mediator.Send(parametros);
        }

    }
}