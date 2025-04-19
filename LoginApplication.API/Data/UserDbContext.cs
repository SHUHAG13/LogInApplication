using Microsoft.EntityFrameworkCore;
using LoginApplication.API.Models;

namespace LoginApplication.API.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<users> User { get; set; }
    }
}
