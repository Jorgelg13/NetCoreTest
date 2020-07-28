using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorErrores;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class EditarCurso : IRequest {
            public Guid CursoId {get;set;}
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime ? FechaPublicacion {get; set;}
            public List<Guid> ListaInstructor {get; set;} 
        }

        public class Validaciones : AbstractValidator<EditarCurso>
        {
            public Validaciones()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
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
                   // throw new Exception("No se pudo eliminar el curso");
                   throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso"} );
                }
              
              curso.Titulo = request.Titulo ?? curso.Titulo;
              curso.Descripcion = request.Descripcion ?? curso.Descripcion;
              curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

              if(request.ListaInstructor != null ){
                  if(request.ListaInstructor.Count > 0){
                      /*primero se eliminan los instructores del curso en la base de datos*/
                      var instructoresDb = _context.CursoInstructor.Where(x => x.CursoId == request.CursoId);
                      foreach(var instructorEliminar in instructoresDb){
                          _context.CursoInstructor.Remove(instructorEliminar);
                      }

                      //procedimiento para agregar instructores que vienen del cliente
                      foreach(var id in request.ListaInstructor){
                          var nuevoInstructor = new CursoInstructor {
                              CursoId = request.CursoId,
                              InstructorId = id
                          };
                          _context.CursoInstructor.Add(nuevoInstructor);
                      }

                  }
              }

              var resultado = await _context.SaveChangesAsync();
             
              if(resultado >0){
                  return Unit.Value;
              }

              throw new Exception("No se guardaron los cambios en el curso");
            } 
        }
    }
}