using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //CursosOnlineContext clase inyectada mediante el constructor de CursosOnlineContext (tambien se puede usar una prop)
        private readonly CursosOnlineContext context;
        public WeatherForecastController(CursosOnlineContext _context)
        {
            this.context = _context;
        }
        [HttpGet]
        public IEnumerable<Curso> get()
        {
           // string[] nombres = new[] { "Seba", "Rodrigo", "Pedro" };
            return context.Curso.ToList();
        }
    }
}
