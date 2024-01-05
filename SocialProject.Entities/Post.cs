using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProject.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string? PostUrl { get; set; }
        public string? PostBody { get; set; }
        public DateTime PostDate { get; set; }

    }
}
