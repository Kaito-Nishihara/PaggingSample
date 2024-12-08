using X.PagedList;

namespace PaggingSample.Models
{
#nullable disable
    public class ProductViewModel
    {
        public PagingDropDown PagingDropDown { get; set; }
        public IPagedList<Product> PagedList { get; set; }
    }
}
