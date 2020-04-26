using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio
{
    public class Precio
    {

        [Key]
        public int PrecioId { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecioActual { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Promocion { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
