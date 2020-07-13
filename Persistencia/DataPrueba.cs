using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public  static async Task InsertarData(CursosContext context, UserManager<Usuario> UsuarioManager){
            if(!UsuarioManager.Users.Any()){
                var usuario = new Usuario{
                    NombreCompleto ="Jorge Laj",
                    UserName="jorge.laj",
                    Email="jorge.laj@hotmail.com"
                };
                await UsuarioManager.CreateAsync(usuario,"Admin123#");
            }
        }
    }
}