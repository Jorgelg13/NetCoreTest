using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Agregar : IRequest{
            //[Required(ErrorMessage="Por favor ingrese el titulo")]
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime ? FechaPublicacion {get; set;}
            public List<Guid> ListaInstructor {get;set;}
        }


        public class Validaciones : AbstractValidator<Agregar>
        {
            public Validaciones()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
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
                Guid _cursoId = Guid.NewGuid();
               var nuevo = new Curso{
                   CursoId =_cursoId,
                   Titulo = request.Titulo,
                   Descripcion = request.Descripcion,
                   FechaPublicacion = request.FechaPublicacion
               };

               _context.Curso.Add(nuevo);

               if(request.ListaInstructor != null ){

                   foreach(var id in request.ListaInstructor){
                      var cursoInstructor = new CursoInstructor{
                           CursoId = _cursoId,
                           InstructorId = id
                       };

                       _context.CursoInstructor.Add(cursoInstructor);
                   }
               }

               var valor = await _context.SaveChangesAsync();

               if(valor > 0){
                   return Unit.Value;
               }

               throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}