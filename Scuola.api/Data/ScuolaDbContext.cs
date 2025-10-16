using Microsoft.EntityFrameworkCore;
using Scuola.api.Models;

namespace Scuola.api.Data;

public partial class ScuolaDbContext : DbContext
{
    public ScuolaDbContext()
    {
    }

    public ScuolaDbContext(DbContextOptions<ScuolaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Corsi> Corsis { get; set; }

    public virtual DbSet<Docenti> Docentis { get; set; }

    public virtual DbSet<Iscrizioni> Iscrizionis { get; set; }

    public virtual DbSet<Studenti> Studentis { get; set; }

    public virtual DbSet<Voti> Votis { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=MOUSSA\\SQLEXPRESS;Initial Catalog=ScuolaDb;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Corsi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Corsi__3214EC07275367BF");

            entity.HasOne(d => d.Docente).WithMany(p => p.Corsis).HasConstraintName("FK_Corsi_Docenti");
        });

        modelBuilder.Entity<Docenti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Docenti__3214EC0716B9F475");
        });

        modelBuilder.Entity<Iscrizioni>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Iscrizio__3214EC07AF79DE1B");

            entity.Property(e => e.DataIscrizione).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Corso).WithMany(p => p.Iscrizionis).HasConstraintName("FK_Iscrizioni_Corsi");

            entity.HasOne(d => d.Studente).WithMany(p => p.Iscrizionis).HasConstraintName("FK_Iscrizioni_Studenti");
        });

        modelBuilder.Entity<Studenti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Studenti__3214EC07FF7660D1");
        });

        modelBuilder.Entity<Voti>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voti__3214EC07487E2CF6");

            entity.Property(e => e.DataVoto).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Corso).WithMany(p => p.Votis).HasConstraintName("FK_Voti_Corsi");

            entity.HasOne(d => d.Studente).WithMany(p => p.Votis).HasConstraintName("FK_Voti_Studenti");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
