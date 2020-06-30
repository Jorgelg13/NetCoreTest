using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Persistencia
{
    public class CursosContext : DbContext
    {
        public CursosContext(DbContextOptions options): base (options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new {ci.InstructorId, ci.CursoId});
        }

        public DbSet<Comentario> Comentario{get;set;}
        public DbSet<Curso> Curso {get;set;}
        public DbSet<CursoInstructor> CursoInstructor {get;set;}
        public DbSet<Instructor> Instructor {get;set;}
        public DbSet<Precio> Precio {get;set;}
    }
}