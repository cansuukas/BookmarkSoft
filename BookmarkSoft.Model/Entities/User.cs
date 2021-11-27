using BookmarkSoft.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookmarkSoft.Model.Entities
{
    public class User : CoreEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; } 
        public string LastIPAddress { get; set; }

        public virtual List<Post> Posts { get; set; }

      
    }
}
