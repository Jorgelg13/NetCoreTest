using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorErrores;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta: IRequest<UsuarioData>{
            public string Email {get; set;}
            public string password {get;set;}

        }

        public class Validacion : AbstractValidator<Ejecuta>{
            public Validacion(){
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            public Manejador(UserManager<Usuario> userManager,SignInManager<Usuario> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
               var usuario = await _userManager.FindByEmailAsync(request.Email);

               if(usuario == null){
                   throw new ManejadorExepcion(HttpStatusCode.Unauthorized);
               }

               var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.password,false);

               if(resultado.Succeeded){
                   return new UsuarioData{
                       NombreCompleto = usuario.NombreCompleto,
                       Token = "data del token",
                       UserName = usuario.UserName,
                       Email = usuario.Email,
                       Imagen = null

                   };
               }

               throw new ManejadorExepcion(HttpStatusCode.Unauthorized);
            }
        }
    }
}