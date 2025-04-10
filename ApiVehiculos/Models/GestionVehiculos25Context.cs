using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiVehiculos.Models;

public partial class GestionVehiculos25Context : DbContext
{
    public GestionVehiculos25Context()
    {
    }

    public GestionVehiculos25Context(DbContextOptions<GestionVehiculos25Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=GestionVehiculos25.mssql.somee.com;database=GestionVehiculos25;uid=instrusenati2024_SQLLogin_1;pwd=SENATI2024;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("usuario");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NombreUser)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("password");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("vehiculo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnioFabVeh).HasColumnName("anioFabVeh");
            entity.Property(e => e.DescripVehiculo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImagenVeh)
                .HasColumnType("text")
                .HasColumnName("imagenVeh");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlacaVeh)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("placaVeh");
            entity.Property(e => e.PropietarioVeh)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("propietarioVeh");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
