using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.AspNet.Identity.Owin;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public class SklepController : Controller
    {
        private ApplicationDbContext _dbContext;

        public SklepController()
        {
        }

        public SklepController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext
        {
            get { return _dbContext ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>(); }
            private set { _dbContext = value; }
        }


        // GET: Produkty
        public ActionResult Produkty(string category, SortMethod method = SortMethod.Nowości, string search = null,
            int page = 1)
        {
            var category1 = category;
            if (search == null)
               search = category1;

            var listaProd = DbContext.Products.Where(c => category1 == null || c.Categories.Name == category1);
            var categorylist = DbContext.Categories.Select(x => x.Name).Distinct().ToList();
            if (!categorylist.Contains(category) && search != null)
            {
                ViewBag.SearchString = search;
                category = search;
                listaProd = DbContext.Products.Where(c => c.Name.Contains(category));
            }
            var paginator = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 3,
                TotalItems = listaProd.Count()
            };
            switch (method)
            {
                case SortMethod.Popularność:
                    listaProd = listaProd.OrderByDescending(o => o.OrdersCount);
                    break;
                case SortMethod.Promocje:
                    listaProd = listaProd.OrderByDescending(o => o.Promotion);
                    break;
                case SortMethod.CenaMalejąco:
                    listaProd = listaProd.OrderByDescending(o => o.Price);
                    break;
                case SortMethod.CenaRosnąco:
                    listaProd = listaProd.OrderBy(o => o.Price);
                    break;
                default:
                    listaProd = listaProd.OrderByDescending(o => o.DateAdded);
                    break;
            }
            var viewModel = new ProductsListViewModel
            {
                Products = listaProd.Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    ImageName = x.ImageName,
                    Price = x.Price,
                    Promotion = x.Promotion ?? 0,
                    QuantityInStock = x.Quantity,
                    Category = x.Categories.Name,
                    Id = x.ProductId
                }).Skip((page - 1)*paginator.ItemsPerPage)
                    .Take(paginator.ItemsPerPage),
                CurrentCategory = category,
                CurrentSortMethod = method,
                PagingInfo = paginator
            };
            return View(viewModel);
        }

        public ActionResult SzczegółyProduktu(int id, string returnUrl)
        {

            var product = DbContext.Products.FirstOrDefault(x => x.ProductId == id);
            return View(product);
        }
    }
}