namespace NewsApp.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
