using Dominio;
using Dominio_ML;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Persistencia
{
    public class CursosOnlineContext : IdentityDbContext<Usuario>
    {
        public CursosOnlineContext(DbContextOptions options) : base(options)
        {

        }
        //Uno a muchos le agregams la fk
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            //Relacion entre clases
            modelbuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstructorId, ci.CursoId });
        }
        //Seteo referencias a las clases DB
        public DbSet<Curso> Curso { set; get; }
        public DbSet<Precio> Precio { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<CursoInstructor> CursoInstructor { get; set; }
    }
}