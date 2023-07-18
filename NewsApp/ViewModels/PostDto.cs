using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
	public class PostDto
	{
		public int Id { get; set; }	
		public string Title { get; set; }
		public string Content { get; set; }
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public List<SelectListItem> SelectListCats { get; set; }
		[Display(Name = "Categories")]
		public int[]? CategoryIds { get; set; }
		

	}
}
