namespace NewsApp.Models
{
    public class CategoryImages
    {
        public int ImageId { get; set; }
        public int CatImageId { get; set; }
        public Image Image { get; set; }
        public ImageCategory Category { get; set; }
    }
}
