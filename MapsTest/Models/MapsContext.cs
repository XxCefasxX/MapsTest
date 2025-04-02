using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MapsTest.Models;

public partial class MapsContext : DbContext
{
    public MapsContext()
    {
    }

    public MapsContext(DbContextOptions<MapsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Marcador> Marcadores { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-BE6LQSO\\PEDRODEV;Initial Catalog=MapaTest;User ID=sa;Password=123456;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Idcliente);

            entity.Property(e => e.Idcliente).HasColumnName("IDCliente");
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });
        modelBuilder.Entity<Marcador>().ToTable("Marcadores");
        modelBuilder.Entity<Marcador>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Calle1).HasMaxLength(50);
            entity.Property(e => e.Calle2).HasMaxLength(50);
            entity.Property(e => e.Categoria).HasMaxLength(50);
            entity.Property(e => e.Colonia).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.Latitud).HasMaxLength(50);
            entity.Property(e => e.Longitud).HasMaxLength(50);
            entity.Property(e => e.Precio).HasColumnType("money");
            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
