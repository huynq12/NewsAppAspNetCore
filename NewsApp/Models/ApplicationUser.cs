using Microsoft.AspNetCore.Identity;

namespace NewsApp.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get;set; }
		public bool? IsLocked { get; set; }
		public List<Comment>? Comments { get; set; }
		public Rating? Rating { get; set; }
	}
}
