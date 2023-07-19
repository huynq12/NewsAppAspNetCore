namespace NewsApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        //public string Title { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Upload { get; set; }
        public List<CategoryImages> CategoryImages { get; set; }

    }
}
