using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SukkuShop.Areas.Admin.Models
{
    public class ProductUploadModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageBig { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        public string Title { get; set; }

        public decimal? Price { get; set; }
        public int? Promotion { get; set; }
        public string Packing { get; set; }
        public int? Category { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
    }
}