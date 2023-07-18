namespace NewsApp.ViewModels.Seedwork
{
    public class ReportedList<T,U>
    {
        public List<T> Items { get; set; }   
        public List<U> ListCount { get; set; }
        public ReportedList(List<T> items,List<U> listCount){
            Items = items;
            ListCount = listCount;
        }
       

    }
}
