using Microsoft.EntityFrameworkCore;
using ToDoList_DomainModel.Models;
using Mission = ToDoList_DomainModel.Models.Mission;

namespace ToDoList_AccessModel.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
