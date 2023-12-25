using System;
using System.Collections.Generic;
using Fetch.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fetch.Service.Data;

public partial class FetchContext : DbContext
{
    public FetchContext()
    {
    }

    public FetchContext(DbContextOptions<FetchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pkey");

            entity.ToTable("locations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Shop).WithMany(p => p.Locations)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("locations_shop_id_fkey");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shops_pkey");

            entity.ToTable("shops");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Shop1)
                .HasMaxLength(255)
                .HasColumnName("shop");
            entity.Property(e => e.Specialty).HasColumnName("specialty");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
