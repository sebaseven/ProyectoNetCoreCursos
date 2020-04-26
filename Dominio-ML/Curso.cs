
    using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        [Column(TypeName="decimal(12,2)")]
        public Precio PrecioPromocion { get; set; }
        //One to One
        public ICollection<Comentario> ComentarioLista { get; set; }
        //One to Many
        public ICollection<CursoInstructor> InstructorLink { get; set; }

    }
}