using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        //clase que representa lo que va a devolver cuando ejecute la clase consulta envolviendo la lista
        public class ListaCursos : IRequest<List<Curso>>
        {

        }
        //dos params primero que quiero devolver y despues el formato en que lo va a devolver segundo param
        public class Manejador : IRequestHandler<ListaCursos, List<Curso>>
        {
            private readonly CursosOnlineContext _context;
            //inyectamos la propiedad context como objeto del constor. manejador
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }
            // aca adentro va la logica de lo que vamos a devolver
            public async Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
               var cursos = await _context.Curso.ToListAsync();
               return cursos;
            }
        }

    }
}