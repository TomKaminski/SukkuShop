using System.Collections.Generic;
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

        public string Price { get; set; }
        public int? Promotion { get; set; }
        public string Packing { get; set; }
        public int? Category { get; set; }
        public int? SubCategory { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public bool PublishAfterCreate { get; set; }
        public string Weight { get; set; }
    }

    public class ProductEditModel
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageBig { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        public string Title { get; set; }

        public string Price { get; set; }
        public int? Promotion { get; set; }
        public string Packing { get; set; }
        public int? Category { get; set; }
        public int? SubCategory { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
        public bool PublishAfterCreate { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
        public bool TrueImageDeleted { get; set; }
        public string Weight { get; set; }
    }

    public class CateogriesCreateEditProduct
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class CategoriesEditCreateProductModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<CateogriesCreateEditProduct> SubCategoryList { get; set; }
    }
}