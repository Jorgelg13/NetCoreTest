using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dominio;
using Aplicacion.Cursos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator){
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> GetTask(){
            return await _mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> Detalle(int id){
            return await _mediator.Send(new ConsultaId.CursoUnico{id = id} );
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Agregar data){
           return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>>Editar(int id, Editar.EditarCurso data){
            data.CursoId = id;

            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Unit>> Eliminar(int id){
            return await _mediator.Send(new Eliminar.EliminarCurso{Id = id});
        }
    }
}