using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SukkuShop.Areas.Admin.Models
{
    public class ProductUploadModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageBig { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageNormal { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageSmall { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Promotion { get; set; }
        public string Packing { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}