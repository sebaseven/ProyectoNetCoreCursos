using System.Threading.Tasks;
using Aplicacion_BL.Seguridad;
using Dominio_ML;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UsuarioController : MiControllerBase
    {
        //http://localhost:5000/api/usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login(Login.Ejecuta paramatros) { 
            return await mediator.Send(paramatros);
        }
    }
}