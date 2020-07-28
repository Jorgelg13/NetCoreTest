using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dominio;
using Aplicacion.Cursos;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CursoDto>>> GetTask(){
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> Detalle(Guid id){
            return await Mediator.Send(new ConsultaId.CursoUnico{id = id} );
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Agregar data){
           return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>>Editar(Guid id, Editar.EditarCurso data){
            data.CursoId = id;

            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Unit>> Eliminar(Guid id){
            return await Mediator.Send(new Eliminar.EliminarCurso{Id = id});
        }
    }
}