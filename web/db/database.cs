using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.db
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {}

        public DbSet<Book> Book { get; set; }

        public DbSet<Category> Category { get; set; }
    }
}
