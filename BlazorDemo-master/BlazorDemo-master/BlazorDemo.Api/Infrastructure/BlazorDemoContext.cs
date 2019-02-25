using BlazorDemo.Model.DataModel;
using Microsoft.EntityFrameworkCore;

namespace BlazorDemo.Api.Infrastructure
{
    public class BlazorDemoContext
        : DbContext
    {
        public BlazorDemoContext(DbContextOptions options)
            : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var bookBuilder = modelBuilder.Entity<Book>();
            bookBuilder.HasKey(m => m.Id);
            bookBuilder.Property(m => m.Id).ValueGeneratedOnAdd();
            bookBuilder.Property(m => m.BookTitle).IsRequired();
            bookBuilder.Property(m => m.ISBN);
            bookBuilder.Property(m => m.PublisherName).IsRequired();
            bookBuilder.ToTable("Book", "BlazorDemo");

            bookBuilder.HasData(
                new Book { Id = 1, BookTitle = "C#", ISBN = "12312", PublisherName = "Oreally" },
                new Book { Id = 2, BookTitle = "dot net", ISBN = "781273", PublisherName = "Wrox" }
                );
        }
    }
}