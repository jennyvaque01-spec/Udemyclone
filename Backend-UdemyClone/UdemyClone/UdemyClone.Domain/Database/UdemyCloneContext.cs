using Microsoft.EntityFrameworkCore;
using UdemyClone.Domain.Entities;


namespace UdemyClone.Domain.Database
{
    public class UdemyCloneContext : DbContext
    {
        public UdemyCloneContext(DbContextOptions<UdemyCloneContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Instructor> Instructores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<TipoLeccion> TiposLeccion { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<CursoInstructor> CursoInstructores { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Leccion> Lecciones { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Resena> Resenas { get; set; }
        public DbSet<ListaDeseo> ListaDeseos { get; set; }
        public DbSet<Progreso> Progresos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<Curso>().ToTable("Cursos");
            modelBuilder.Entity<Instructor>().ToTable("Instructores");
            modelBuilder.Entity<Estudiante>().ToTable("Estudiantes");
            modelBuilder.Entity<TipoLeccion>().ToTable("TipoLeccion");
            modelBuilder.Entity<CursoInstructor>().ToTable("CursoInstructor");
            modelBuilder.Entity<Seccion>().ToTable("Seccion");
            modelBuilder.Entity<Leccion>().ToTable("Leccion");
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripciones");
            modelBuilder.Entity<Resena>().ToTable("Resenas");
            modelBuilder.Entity<ListaDeseo>().ToTable("ListaDeseo");
            modelBuilder.Entity<Progreso>().ToTable("Progreso");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");


            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.CategoriaPadre)
                .WithMany(c => c.Subcategorias)
                .HasForeignKey(c => c.CategoriaPadreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CursoInstructor>()
                .HasKey(ci => new { ci.CursoId, ci.InstructorId });

            modelBuilder.Entity<Inscripcion>()
                .HasIndex(i => new { i.EstudianteId, i.CursoId })
                .IsUnique();

            modelBuilder.Entity<Resena>()
                .HasKey(r => new { r.EstudianteId, r.CursoId });

            modelBuilder.Entity<Resena>()
                .Property(r => r.ResenaTexto)
                .HasColumnName("Resena");

            modelBuilder.Entity<ListaDeseo>()
                .HasKey(l => new { l.CursoId, l.EstudianteId });

            modelBuilder.Entity<Progreso>()
                .HasKey(p => new { p.EstudianteId, p.LeccionId });

            modelBuilder.Entity<Instructor>()
                .HasIndex(i => i.Email).IsUnique();

            modelBuilder.Entity<Estudiante>()
                .HasIndex(e => e.Email).IsUnique();

            modelBuilder.Entity<Curso>()
                .Property(c => c.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Inscripcion>()
                .Property(i => i.PrecioPagado)
                .HasPrecision(18, 2);
        }
    }
}
