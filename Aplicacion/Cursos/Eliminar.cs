using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorErrores;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class EliminarCurso : IRequest{
            public Guid Id {get; set;}
        }

        public class Manejador : IRequestHandler<EliminarCurso>
        {
            private readonly CursosContext _context;
            public Manejador(CursosContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EliminarCurso request, CancellationToken cancellationToken)
            {
                var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach(var instructor in instructoresDB){
                    _context.CursoInstructor.Remove(instructor);
                }

                var curso = await _context.Curso.FindAsync(request.Id);

                if(curso == null){
                   // throw new Exception("No se pudo eliminar el curso");
                   throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso"} );
                }

                _context.Remove(curso);
                var resultado = await _context.SaveChangesAsync();

                if(resultado >0){
                    return Unit.Value;
                }

                throw new Exception("No se pudieron guardar los cambios");
            }
        }
    }
}