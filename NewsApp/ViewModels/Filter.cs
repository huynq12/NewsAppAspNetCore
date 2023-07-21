using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Enums;
using NewsApp.ViewModels.Seedwork;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
	public class Filter : PagingParameters
	{
		public FilterOrder? FilterOrder { get; set; }

        public List<SelectListItem>? SelectListCats { get; set; }
        [Display(Name = "Categories")]
        public int[]? CategoryIds { get; set; }
    }
}
