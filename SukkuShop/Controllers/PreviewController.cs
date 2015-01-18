using System.Collections.Generic;
using System.Web.Mvc;
using Postal;
using SukkuShop.Models;

namespace SukkuShop.Controllers
{
    public partial class PreviewController : Controller
    {
        public virtual ActionResult Example()
        {
            var list = new List<OrderItemSummary>
            {
                new OrderItemSummary
                {
                    Image = "1",
                    Name = "produkt 1",
                    Price = 15.12m,
                    Quantity = 5,
                    TotalValue = 130m
                },
                new OrderItemSummary
                {
                    Image = "1",
                    Name = "produkt 1",
                    Price = 15.12m,
                    Quantity = 5,
                    TotalValue = 130m
                },
                new OrderItemSummary
                {
                    Image = "1",
                    Name = "produkt 1",
                    Price = 15.12m,
                    Quantity = 5,
                    TotalValue = 130m
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
                    TotalTotalValue = 150,
                    OrderPayment = new SharedShippingOrderSummaryModels
                    {
                        Description = "Payment opis",
                        Name = "Payment name",
                        Price = 55.21m
                    },
                    OrderShipping = new SharedShippingOrderSummaryModels
                    {
                        Description = "Shipping Opis",
                        Name = "Shipping name",
                        Price = 55.21m
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
                        TotalValue = 55.21m,
                        OrderProductList = list
                    }
                }
            };

            return new EmailViewResult(email);
        }
    }
}