using AutoMapper;
using NewsApp.Models;
using NewsApp.ViewModels;

namespace NewsApp.Helpers
{
	public class Helper : Profile
	{
		public Helper() {
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<Post, PostDto>();
		}	
	}
}
