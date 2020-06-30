using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class EditarCurso : IRequest {
            public int CursoId {get;set;}
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime ? FechaPublicacion {get; set;}
        }

        public class Manejador : IRequestHandler<EditarCurso>
        {
            private readonly CursosContext _context;
            public Manejador(CursosContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EditarCurso request, CancellationToken cancellationToken)
            {
               var curso = await _context.Curso.FindAsync(request.CursoId);
               if(curso == null){
                   throw new Exception("El curso ingresado no existe");
               }
              
              curso.Titulo = request.Titulo ?? curso.Titulo;
              curso.Descripcion = request.Descripcion ?? curso.Descripcion;
              curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
              var resultado = await _context.SaveChangesAsync();
             
              if(resultado >0){
                  return Unit.Value;
              }

              throw new Exception("No se guardaron los cambios en el curso");
            } 
        }
    }
}