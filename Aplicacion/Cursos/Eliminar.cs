using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class EliminarCurso : IRequest{
            public int Id {get; set;}
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
                var curso = await _context.Curso.FindAsync(request.Id);

                if(curso == null){
                    throw new Exception("No se pudo eliminar el curso");
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