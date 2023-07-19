namespace NewsApp.Models
{
    public class ImageCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryImages> CategoryImages { get; set; }
    }
}
