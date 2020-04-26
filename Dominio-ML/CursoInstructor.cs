using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio
{
    public class CursoInstructor
    {

        [Key]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

    }
}
