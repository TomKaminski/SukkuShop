using System.Web;

namespace SukkuShop.Areas.Admin.Models
{
    public class FileUploadModel
    {
        public HttpPostedFileBase MyFile { get; set; }
    }
}