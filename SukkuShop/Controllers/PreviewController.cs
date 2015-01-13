using System.Collections.Generic;
using System.Web.Mvc;
using Postal;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public class PreviewController : Controller
    {
        public ActionResult Example()
        {
            var list = new List<OrderItemSummary>
            {
                new OrderItemSummary
                {
                    Image = "1",
                    Name = "produkt 1",
                    Price = "15.12",
                    Quantity = 5,
                    TotalValue = 130.ToString("c")
                },
                new OrderItemSummary
                {
                    Image = "1",
                    Name = "produkt 4",
                    Price = "15.12",
                    Quantity = 5,
                    TotalValue = (130.51).ToString("c")
                },
                new OrderItemSummary
                {
                    Image = "1",
                    Name = "produkt 2",
                    Price = "15.12",
                    Quantity = 5,
                    TotalValue = 130.ToString("c")
                }
            };
            var email = new OrderSumEmail
            {

                To = "dasasdads@wp.pl",
                CallbackUrl = Url.Action(MVC.Konto.HistoriaZamowien()),
                Id = 5,
                OrderViewModelsSummary = new OrderViewModelsSummary
                {
                    Firma = false,
                    TotalTotalValue = (150).ToString("c"),
                    OrderPayment = new OrderPaymentSummary
                    {
                        Description = "Payment opis",
                        Name = "Payment name",
                        Price = (55.21).ToString("c")
                    },
                    OrderShipping = new OrderShippingSummary
                    {
                        Description = "Shipping Opis",
                        Name = "Shipping name",
                        Price = (545.21).ToString("c")
                    },
                    UserAddressModel = new CartAddressModel
                    {
                        Imie = "Tomasz",
                        KodPocztowy = "43-200",
                        Miasto = "Pszczyna",
                        NazwaFirmy = "",
                        Nazwisko = "Kamiński",
                        Nip = "",
                        Numer="12",
                        Telefon = "789252925",
                        Ulica = "Maków"
                    },
                    OrderViewItemsTotal = new OrderViewItemsTotal
                    {
                        TotalValue = (421.21).ToString("c"),
                        OrderProductList = list
                    }
                }
            };

            return new EmailViewResult(email);
        }
    }
}