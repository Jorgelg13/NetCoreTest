using Dominio;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerador
    {
         string crearToken(Usuario usuario);
    }
}