using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorErrores;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData >{
            public string Nombre {get; set;}
            public string Apellido {get; set;}
            public string Email {get; set;}
            public string Password {get;set;}
            public string UserName {get;set;}
        }

        public class Validacion : AbstractValidator<Ejecuta>
        {
            public Validacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta,UsuarioData>
        {
            private readonly CursosContext _context;
            private readonly UserManager<Usuario> _userManager;

            private readonly IJwtGenerador _jwtGenerador;

            public Manejador(CursosContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador){
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
               var existe = await _context.Users.Where(u => u.Email == request.Email).AnyAsync();

               if(existe){
                   throw new ManejadorExepcion(HttpStatusCode.BadRequest,new {mensaje="El email ingresado ya existe"});
               }

               var existeUsername = _context.Users.Where(x =>  x.UserName == request.UserName).AnyAsync();
                if(existe){
                   throw new ManejadorExepcion(HttpStatusCode.BadRequest,new {mensaje="El Nombre de usuario ingresado ya existe"});
               }

               var usuario = new Usuario{
                   NombreCompleto = request.Nombre + " " +request.Apellido,
                   Email = request.Email,
                   UserName = request.UserName
               };

               var resultado = await _userManager.CreateAsync(usuario,request.Password);

               if(resultado.Succeeded){
                   return new UsuarioData{
                       NombreCompleto = usuario.NombreCompleto,
                       Token = _jwtGenerador.crearToken(usuario),
                       UserName = usuario.UserName,
                       Email = usuario.Email
                   };
               }

               throw new Exception("No se pudo insertar el usuario");
            }
        }
    }
}