using System.ComponentModel.DataAnnotations;

namespace PaggingSample.Models
{
    public class PrivateModel
    {
        [DisplayFormat(DataFormatString = "{0:#,##0.#}", ApplyFormatInEditMode = true)] // 小数点以下を表示しない        
        public decimal DecimalValue { get; set; }
    }
}
