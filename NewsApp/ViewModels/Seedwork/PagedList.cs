namespace NewsApp.ViewModels.Seedwork
{
	public class PagedList<T>
	{
		public List<T> Items { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int StartPage { get; set; }
		public int EndPage { get; set; }
		public PagedList() { }
		public PagedList(List<T> items, int count, int pageNumber, int pageSize)
		{

			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			Items = items;
			int startPage = pageNumber - 5;
			int endPage = pageNumber + 4;
			if (startPage < 0)
			{
				endPage = endPage - (startPage - 1);
				startPage = 1;
			}
			if (endPage > TotalPages)
			{
				endPage = TotalPages;
				if (endPage > 10)
				{
					startPage = endPage - 9;
				}
			}
			StartPage = startPage;
			EndPage = endPage;
		}
	}
}
