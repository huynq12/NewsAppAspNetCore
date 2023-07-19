﻿namespace NewsApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentMsg { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
