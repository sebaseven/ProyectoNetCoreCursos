
    using System;
    using System.Collections.Generic;
namespace Dominio
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Precio PrecioPromocion { get; set; }
        //One to One
        public ICollection<Comentario> ComentarioLista { get; set; }
        //One to Many
        public ICollection<CursoInstructor> InstructorLink { get; set; }

    }
}