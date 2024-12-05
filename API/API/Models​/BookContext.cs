using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models​;

public partial class BookContext : DbContext
{
    public BookContext()
    {
    }

    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DAMQUOCDAN;Database=book;uid=sa;pwd=1234;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3213E83F3D809200");

            entity.HasIndex(e => e.BookCode, "UQ__Books__3BB8DAE694FBB8EC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .HasColumnName("author");
            entity.Property(e => e.BookCode)
                .HasMaxLength(50)
                .HasColumnName("bookCode");
            entity.Property(e => e.BookName)
                .HasMaxLength(255)
                .HasColumnName("bookName");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .HasColumnName("category");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PublishDate)
                .HasColumnType("date")
                .HasColumnName("publishDate");
            entity.Property(e => e.Publisher)
                .HasMaxLength(255)
                .HasColumnName("publisher");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Summary).HasColumnName("summary");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
