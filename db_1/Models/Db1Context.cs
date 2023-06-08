using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace db_1.Models;

public partial class Db1Context : DbContext
{
    public Db1Context()
    {
    }

    public Db1Context(DbContextOptions<Db1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<AsignaturasEstudiante> AsignaturasEstudiantes { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) 
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Codigo).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(300);
            entity.Property(e => e.FechaActualizacion).HasColumnName("Fecha_Actualizacion");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<AsignaturasEstudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignaturas_estudiantes");

            entity.HasIndex(e => e.AsignaturaId, "fk_estudiante_has_asignatura_asignatura1_idx");

            entity.HasIndex(e => e.EstudianteId, "fk_estudiante_has_asignatura_estudiante_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturaId).HasColumnType("int(11)");
            entity.Property(e => e.EstudianteId).HasColumnType("int(11)");
            entity.Property(e => e.FechaRegistro).HasColumnName("Fecha_Registro");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.AsignaturasEstudiantes)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiante_has_asignatura_asignatura1");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.AsignaturasEstudiantes)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiante_has_asignatura_estudiante");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiante");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Edad).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Rut).HasMaxLength(45);
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notas");

            entity.HasIndex(e => e.AsignaturaId, "fk_notas_asignatura1_idx");

            entity.HasIndex(e => e.EstudianteId, "fk_notas_estudiante1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturaId).HasColumnType("int(11)");
            entity.Property(e => e.EstudianteId).HasColumnType("int(11)");
            entity.Property(e => e.Nota1).HasColumnName("Nota");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Nota)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notas_asignatura1");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Nota)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notas_estudiante1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
