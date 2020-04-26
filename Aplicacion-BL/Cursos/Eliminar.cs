using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Aplicacion.Cursos
{
   public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int CursoId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta> 
        {
            public readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                    // throw new Exception("No se encontro el curso");
                    throw new ManejadorErrores.ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,new { mensaje = "No se encontro el curso"  });
                }
                else
                {
                    _context.Remove(curso);
                    var result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        return Unit.Value;
                    }
                    else
                    {
                        throw new Exception("No se elimino");
                    }
                }
            }
        }
    }
}
