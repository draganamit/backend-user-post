using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_users_posts.Models
{
    public class Posts
    {
        public int Id { get; set; }

        public string Text { get; set; }="The book is good.";
    }
}