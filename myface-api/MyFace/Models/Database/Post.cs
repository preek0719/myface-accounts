using System;
using System.Collections.Generic;

namespace MyFace.Models.Database
{
    public class Post
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PostedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
    }
}