using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Aplicacion_BL.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("API/[controller]")]

    public class CursosController : ControllerBase
    {
        //en el ctor inyecto el mediador con la BL
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        //metodo para devolver al cliente lista de cursos
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            //creamos la transaccion, nombre de la clase que quiero devolver --> consulta, adentro de esa clase esta otra que es justamente ListaCursos
            return await _mediator.Send(new Consulta.ListaCursos());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Curso>> Detalle(int id)
        {
            //creamos la transaccion, nombre de la clase que quiero devolver --> consulta, adentro de esa clase esta otra que es justamente ListaCursos
            return await _mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);

        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Unit>> Editar(int Id , Editar.Ejecuta data)
        {
            data.CursoId = Id;
            return await _mediator.Send(data);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id)
        {
            return await _mediator.Send(new Eliminar.Ejecuta { CursoId = id });
        }
    }
}