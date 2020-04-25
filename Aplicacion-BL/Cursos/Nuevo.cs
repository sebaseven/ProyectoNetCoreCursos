using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion_BL.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            //validar con anotations
           // [Required(ErrorMessage ="Por Favor Ingrese titulo")]
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
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context) {
                this._context = context;
            }  
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };
                _context.Curso.Add(curso);
               var valor = await _context.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }
                else 
                {
                    throw new Exception("No se inserto el curso");
                }

            }
        }
    }
}