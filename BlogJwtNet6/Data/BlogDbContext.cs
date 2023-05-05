using BlogJwtNet6.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogJwtNet6.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
