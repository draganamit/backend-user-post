using backend_user_post.Models;
using dotnet_users_posts.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_user_post.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Posts> Posts{get; set;}
        public DbSet<User> User{get; set;}
    }
}