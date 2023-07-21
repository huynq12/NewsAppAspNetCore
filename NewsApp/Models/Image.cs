

namespace NewsApp.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string MyImage { get; set; }
        public string ImageSize { get; set; }
        public string ImageDescription { get; set; }
        public DateTime UploadTime { get; set; }
        public List<CategoryImages> CategoryImages { get; set; }

    }
}
