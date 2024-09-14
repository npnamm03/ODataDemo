using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ODataSample.Data.Models;

namespace ODataSample.Data.Context
{
    public class BookStoreContext: DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> option) : base(option) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().OwnsOne(c => c.Location);
        }
    }
}
