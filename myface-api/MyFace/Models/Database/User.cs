
﻿using System.Collections.Generic;
using System.Reflection.Metadata;

namespace MyFace.Models.Database
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CoverImageUrl { get; set; }
        public string ProfileImageUrl { get; set; }
        public string HashedPassword { get; set; }
        public string Salt {get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
    }
}
