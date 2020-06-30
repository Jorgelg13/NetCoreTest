using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        
        public class CursoUnico: IRequest<Curso>{
            public int id {get; set;}

        }

        public class Manejador : IRequestHandler<CursoUnico, Curso>
        {
            private readonly CursosContext _context;
            public Manejador(CursosContext context)
            {
                _context = context;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.id);

                return curso;
            }
        }

    }
}