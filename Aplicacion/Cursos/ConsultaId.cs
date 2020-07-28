using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorErrores;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        
        public class CursoUnico: IRequest<CursoDto>{
            public Guid id {get; set;}

        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDto>
        {
            private readonly CursosContext _context;
            private readonly IMapper _mapper;
            public Manejador(CursosContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso
                .Include(x => x.InstructorLink)
                .ThenInclude(y => y.Instructor)
                .FirstOrDefaultAsync(a => a.CursoId == request.id);

                if(curso == null){
                   // throw new Exception("No se pudo eliminar el curso");
                   throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso"} );
                }

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);  
                return cursoDto;
            }
        }

    }
}