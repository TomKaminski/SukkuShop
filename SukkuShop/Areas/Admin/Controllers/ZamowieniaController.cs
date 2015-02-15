using System;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SukkuShop.Identity;
using SukkuShop.Migrations;
using SukkuShop.Models;

namespace SukkuShop.Areas.Admin.Controllers
{
    public partial class ZamowieniaController : Controller
    {
        private readonly ApplicationDbContext _dbContext;


        public ZamowieniaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        // GET: Admin/Zamowienia
        public virtual ActionResult Index()
        {
            ViewBag.SelectedOpt = 2;
            return View();
        }

        public virtual JsonResult GetOrdersList()
        {
            var orders = _dbContext.Orders.Select(x => new
            {
                x.Email,
                x.OrderId,
                x.OrderDate,
                x.OrderInfo,
                x.TotalPrice,
                x.UserId
            }).ToList();

            var ordersObj = orders.Select(x => new
            {
                email = x.Email,
                id = x.OrderId,
                data = x.OrderDate.ToShortDateString(),
                stan = x.OrderInfo,
                total = x.TotalPrice,
                userId = x.UserId,
                orderOpts = GetOrderChangeOptions(x.OrderInfo),
                selectedOpt = x.OrderInfo
            }).ToList();

            return Json(ordersObj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult ChangeOrderState(int id, string value)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == id);
            if (order != null && order.OrderInfo!=value)
            {
                order.OrderInfo = value;
                _dbContext.Orders.AddOrUpdate(order);
                _dbContext.SaveChanges();

                var discounvalue = ((order.OrderDetails.Sum(orderdetail => orderdetail.SubTotalPrice)*order.Discount)*100)/100;                
                var email = new ChangeOrderStateEmail
                {
                    To = order.Email,
                    Id = order.OrderId,
                    State = value,
                    StateDescription = SetStateDescription(value),
                    OrderViewModelsSummary = new OrderViewModelsSummary
                    {
                        Firma = order.NazwaFirmy != null,
                        TotalTotalValue = order.TotalPrice,
                        Discount = order.Discount,
                        DiscountValue = discounvalue.ToString("c"),
                        OrderPayment = new SharedShippingOrderSummaryModels
                        {
                            Description = order.Payment.PaymentDescription,
                            Name = order.Payment.PaymentName,
                            Price = order.Payment.PaymentPrice
                        },
                        OrderShipping = new SharedShippingOrderSummaryModels
                        {
                            Description = order.Shipping.ShippingDescription,
                            Name = order.Shipping.ShippingName,
                            Price = order.Shipping.ShippingPrice
                        },
                        UserAddressModel = new CartAddressModel
                        {
                            Email = order.Email,
                            Imie=order.Name,
                            KodPocztowy = order.PostalCode,
                            Miasto = order.City,
                            NazwaFirmy = order.NazwaFirmy,
                            Nazwisko = order.Surname,
                            Nip = order.OrderNip,
                            Numer=order.Number,
                            Telefon = order.Phone,
                            Ulica = order.Street
                        },
                        OrderViewItemsTotal = new OrderViewItemsTotal
                        {
                            TotalValue = order.OrderDetails.Sum(x=>x.SubTotalPrice),
                            OrderProductList = order.OrderDetails.Select(m => new OrderItemSummary
                            {
                                Image = m.Products.IconName,
                                Name = m.Products.Name,
                                Price = m.Products.Price ?? 0,
                                Quantity = m.Quantity,
                                TotalValue = m.SubTotalPrice,
                                Packing = m.Products.Packing
                            }).ToList()
                        }
                    }
                };
                email.Send();
            }
            return Json(GetOrderChangeOptions(value), JsonRequestBehavior.AllowGet);
        }

        public virtual FileResult DownloadInvoice()
        {
            // Create a new PDF document
            var document = new PdfDocument();
            document.Info.Title = "Faktura";
 
            // Create an empty page
            var page = document.AddPage();
 
            // Get an XGraphics object for drawing
            var gfx = XGraphics.FromPdfPage(page);
 
            // Create a font
            var font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
 
            // Draw the text
            gfx.DrawString("Miejscę na fakturę zamówienia", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height),XStringFormats.Center);
            var stream = new MemoryStream();
            document.Save(stream, false);
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }

        private string SetStateDescription(string state)
        {
            string stateDescription;
            switch (state)
            {
                case "Wysłane":
                    stateDescription =
                        "Twoje zamówienie zostało zrealizowane - towar został wysłany (lub oczekuje na odebranie w przypadku odbioru osobistego).";
                    break;
                
                case "Realizowane":
                    stateDescription =
                        "Twoje zamówienie zostało przekazane do realizacji. Stan zamówienia sprawdzisz po zalogowaniu się w zakładce Moje konto / Moje zamówienia.";                    
                        break;

                default:
                    stateDescription =
                        String.Format("Stan Twojego zamówienia został zmieniony na {0}.",state);                    
                        break;
            }
            return stateDescription;
        }

        private object[] GetOrderChangeOptions(string stan)
        {
            switch (stan)
            {
                case "Przyjęte":
                {
                    object[] obj =
                    {
                        new{label="Przyjęte",value=1,selected=true},
                        new {label = "Wysłane", value = 4},
                        new {label = "Realizowane", value = 3}
                    };
                    return obj;
                }

                case "Oczekujące":
                {
                    object[] obj =
                    {
                        new{label="Oczekujące",value=2,selected=true},
                        new {label = "Realizowane", value = 3},
                        new {label = "Wysłane", value = 4}
                    };
                    return obj;
                }
                case "Wysłane":
                {
                    object[] obj =
                    {
                    };
                    return obj;
                }
                case "Anulowane":
                {
                    object[] obj =
                    {
                    };
                    return obj;
                }
                case "Realizowane":
                {
                    object[] obj =
                    {
                        new {label = "Realizowane", value = 3,selected=true},
                        new {label = "Wysłane", value = 4}
                    };
                    return obj;
                }
                default:
                {
                    object[] obj =
                    {
                    };
                    return obj;
                }
            }
        }
    }
}
