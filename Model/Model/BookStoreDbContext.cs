using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Model.Model;

public partial class BookStoreDbContext : DbContext
{
    private readonly IConfiguration configuration;
    public BookStoreDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBookstore> TblBookstores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
                => optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBookstore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Id");

            entity.ToTable("TBL_BOOKSTORE");

            entity.Property(e => e.BookCategory)
                .HasMaxLength(50)
                .HasColumnName("BOOK_CATEGORY");
            entity.Property(e => e.BookCategoryId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("BOOK_CATEGORY_ID");
            entity.Property(e => e.BookName)
                .HasMaxLength(50)
                .HasColumnName("BOOK_NAME");
            entity.Property(e => e.Stock)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("STOCK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
