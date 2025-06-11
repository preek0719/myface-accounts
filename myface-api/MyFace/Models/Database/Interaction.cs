using System;

namespace MyFace.Models.Database
{
    public enum InteractionType
    {
        LIKE,
        DISLIKE,
    }

    public class Interaction
    {
        public int Id { get; set; }

        public InteractionType Type { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}