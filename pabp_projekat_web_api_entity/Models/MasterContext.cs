using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pabp_projekat_web_api_entity.Models;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ispit> Ispits { get; set; }

    public virtual DbSet<IspitniRok> IspitniRoks { get; set; }

    public virtual DbSet<Predmet> Predmets { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentPredmet> StudentPredmets { get; set; }

    public virtual DbSet<Zapisnik> Zapisniks { get; set; }

    public virtual DbSet<Prijava_brojIndeksa> Prijavas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=BEOKROS\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ispit>(entity =>
        {
            entity.HasKey(e => e.IdIspita).HasName("PK__ispit__29C6AD7FDD02363B");

            entity.ToTable("ispit");

            entity.Property(e => e.IdIspita).HasColumnName("ID_ISPITA");
            entity.Property(e => e.Datum).HasColumnName("DATUM");
            entity.Property(e => e.IdPredmeta).HasColumnName("ID_PREDMETA");
            entity.Property(e => e.IdRoka).HasColumnName("ID_ROKA");

            entity.HasOne(d => d.IdRokaNavigation).WithMany(p => p.Ispits)
                .HasForeignKey(d => d.IdRoka)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ispit_ispitni_rok");
        });

        modelBuilder.Entity<IspitniRok>(entity =>
        {
            entity.HasKey(e => e.IdRoka).HasName("PK__ispitni___C7D0FE7210C9F95B");

            entity.ToTable("ispitni_rok");

            entity.Property(e => e.IdRoka).HasColumnName("ID_ROKA");
            entity.Property(e => e.Naziv)
                .HasMaxLength(15)
                .HasColumnName("NAZIV");
            entity.Property(e => e.SkolskaGod)
                .HasMaxLength(7)
                .HasColumnName("SKOLSKA_GOD");
            entity.Property(e => e.StatusRoka)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("STATUS_ROKA");
        });

        modelBuilder.Entity<Predmet>(entity =>
        {
            entity.HasKey(e => e.IdPredmeta).HasName("PK__predmet__2C40E4E24B422B41");

            entity.ToTable("predmet");

            entity.Property(e => e.IdPredmeta)
                .ValueGeneratedNever()
                .HasColumnName("ID_PREDMETA");
            entity.Property(e => e.Espb).HasColumnName("ESPB");
            entity.Property(e => e.IdProfesora).HasColumnName("ID_PROFESORA");
            entity.Property(e => e.Naziv)
                .HasMaxLength(50)
                .HasColumnName("NAZIV");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("STATUS");

            entity.HasOne(d => d.IdProfesoraNavigation).WithMany(p => p.Predmets)
                .HasForeignKey(d => d.IdProfesora)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_predmet_profesor");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.IdProfesora).HasName("PK__profesor__63597FD79C4E8F7A");

            entity.ToTable("profesor");

            entity.Property(e => e.IdProfesora)
                .ValueGeneratedNever()
                .HasColumnName("ID_PROFESORA");
            entity.Property(e => e.DatumZap).HasColumnName("DATUM_ZAP");
            entity.Property(e => e.Ime)
                .HasMaxLength(25)
                .HasColumnName("IME");
            entity.Property(e => e.Prezime)
                .HasMaxLength(50)
                .HasColumnName("PREZIME");
            entity.Property(e => e.Zvanje)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ZVANJE");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdStudenta).HasName("PK__student__0FD2897837FE30F5");

            entity.ToTable("student");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.Broj).HasColumnName("BROJ");
            entity.Property(e => e.GodinaUpisa)
                .HasMaxLength(4)
                .HasColumnName("GODINA_UPISA");
            entity.Property(e => e.Ime)
                .HasMaxLength(25)
                .HasColumnName("IME");
            entity.Property(e => e.Prezime)
                .HasMaxLength(40)
                .HasColumnName("PREZIME");
            entity.Property(e => e.Smer)
                .HasMaxLength(5)
                .HasColumnName("SMER");
        });

        modelBuilder.Entity<StudentPredmet>(entity =>
        {
            entity.HasKey(e => new { e.IdStudenta, e.IdPredmeta, e.SkolskaGodina }).HasName("PK__student___BB07D41166382CA2");

            entity.ToTable("student_predmet");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.IdPredmeta).HasColumnName("ID_PREDMETA");
            entity.Property(e => e.SkolskaGodina)
                .HasMaxLength(7)
                .HasColumnName("SKOLSKA_GODINA");

            entity.HasOne(d => d.IdPredmetaNavigation).WithMany(p => p.StudentPredmets)
                .HasForeignKey(d => d.IdPredmeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_student_predmet_predmet");

            entity.HasOne(d => d.IdStudentaNavigation).WithMany(p => p.StudentPredmets)
                .HasForeignKey(d => d.IdStudenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_student_predmet_student");
        });

        modelBuilder.Entity<Zapisnik>(entity =>
        {
            entity.HasKey(e => new { e.IdStudenta, e.IdIspita }).HasName("PK__zapisnik__ED4EE3AFE84A496D");

            entity.ToTable("zapisnik");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.IdIspita).HasColumnName("ID_ISPITA");
            entity.Property(e => e.Bodovi)
                .HasMaxLength(3)
                .HasColumnName("BODOVI");
            entity.Property(e => e.Ocena).HasColumnName("OCENA");

            entity.HasOne(d => d.IdIspitaNavigation).WithMany(p => p.Zapisniks)
                .HasForeignKey(d => d.IdIspita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapisnik_ispit");

            entity.HasOne(d => d.IdStudentaNavigation).WithMany(p => p.Zapisniks)
                .HasForeignKey(d => d.IdStudenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zapisnik_student");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
