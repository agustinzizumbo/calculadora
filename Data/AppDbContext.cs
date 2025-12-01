using System;
using System.Collections.Generic;
using CalculadoraKW.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraKW.Api.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aparatos> Aparatos { get; set; }

    public virtual DbSet<UsoAparatos> UsoAparatos { get; set; }

    // OnConfiguring removed so the DbContext is configured through DI in Program.cs

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aparatos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aparatos__3214EC07AD1164FE");

            entity.Property(e => e.ConsumoKWh).HasColumnType("decimal(10, 4)");
            entity.Property(e => e.Marca).HasMaxLength(100);
            entity.Property(e => e.Modelo).HasMaxLength(100);
            entity.Property(e => e.Tamaño).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasMaxLength(100);
        });

        modelBuilder.Entity<UsoAparatos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsoApara__3214EC07886A3C3F");
            entity.Property(e => e.Cantidad).IsRequired();
            entity.Property(e => e.Horas).HasColumnType("decimal(6,2)");
            entity.Property(e => e.Dias).IsRequired();
            entity.Property(e => e.Comentario).HasMaxLength(250);

            entity.HasOne(d => d.IdAparatoNavigation).WithMany(p => p.UsoAparatos)
                .HasForeignKey(d => d.IdAparato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsoAparatos_Aparatos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
