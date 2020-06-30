using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Agregar : IRequest{
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime FechaPublicacion {get; set;}
        }

        public class Manejador : IRequestHandler<Agregar>
        {
            private readonly CursosContext _context;
            public Manejador(CursosContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Agregar request, CancellationToken cancellationToken)
            {
               var nuevo = new Curso{
                   Titulo = request.Titulo,
                   Descripcion = request.Descripcion,
                   FechaPublicacion = request.FechaPublicacion
               };

               _context.Curso.Add(nuevo);
               var valor = await _context.SaveChangesAsync();

               if(valor > 0){
                   return Unit.Value;
               }

               throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}