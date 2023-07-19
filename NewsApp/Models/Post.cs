using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Models
{
	public class Post
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Content { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public ICollection<PostCategory> PostCategories{ get; set; }
		
	}
}
