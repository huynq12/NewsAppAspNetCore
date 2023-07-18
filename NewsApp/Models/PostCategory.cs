using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Models
{
	public class PostCategory
	{
		public int PostId { get; set; }
		public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]

        public Post Post { get; set; }

		[ForeignKey(nameof(CategoryId))]
        public virtual Category	Category { get; set; }
	}
}
