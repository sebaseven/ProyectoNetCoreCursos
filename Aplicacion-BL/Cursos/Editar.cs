using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        //Cabecera
        public class Ejecuta : IRequest
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
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
                    throw new ManejadorErrores.ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "No se encontro el curso" });
                }
                else
                {
                    curso.Titulo = request.Titulo ?? curso.Titulo; // si no recibe titulo deja el que tenia
                    curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                    curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        return Unit.Value;
                    }
                    else {
                        throw new Exception("NO se guardo");
                    }
                }
            }
        }
    }
}
