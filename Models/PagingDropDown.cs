



using Microsoft.AspNetCore.Mvc.Rendering;

namespace PaggingSample.Models
{
    public class PagingDropDown
    {
        public int SelectedPageSize { get; set; } 
        public IEnumerable<SelectListItem> PageSizeOptions { get; set; }

        public PagingDropDown() 
        {
            SelectedPageSize = 10; // デフォルト値
            PageSizeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "10", Text = "10件" },
                new SelectListItem { Value = "20", Text = "20件" },
                new SelectListItem { Value = "50", Text = "50件" }
            };
        }
    }
}
