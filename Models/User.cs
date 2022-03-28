using System.Collections.Generic;
using dotnet_users_posts.Models;

namespace backend_user_post.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Posts> Posts {get; set;}
    }
}