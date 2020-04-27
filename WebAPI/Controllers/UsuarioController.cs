using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Aplicacion_BL.Seguridad;
using Dominio_ML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seguridad;

namespace WebAPI.Controllers
{
    public class UsuarioController : MiControllerBase
    {
        //http://localhost:5000/api/usuario/login
        [AllowAnonymous]

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login([FromBody] Login.Ejecuta paramatros) {
            return await mediator.Send(paramatros);
        }
        //http://localhost:5000/api/usuario/registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta data)
        {
            return await mediator.Send(data);

        }
    }
}