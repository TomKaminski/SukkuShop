#region

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SukkuShop.Areas.Admin.Models;
using SukkuShop.Models;

#endregion

namespace SukkuShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        //[HttpPost]
        //public virtual JsonResult ChangeOrderState(int id, string value, string packageNumber)
        //{
        //    var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == id);
        //    if (order != null && order.OrderInfo != value)
        //    {
        //        order.OrderInfo = value;
        //        _dbContext.Orders.AddOrUpdate(order);
        //        _dbContext.SaveChanges();

        //        var discounvalue = ((order.OrderDetails.Sum(orderdetail => orderdetail.SubTotalPrice)*order.Discount)*
        //                            100)/100;
        //        var email = new ChangeOrderStateEmail
        //        {
        //            To = order.Email,
        //            Id = order.OrderId,
        //            State = value,
        //            StateDescription = SetStateDescription(value),
        //            PackageName = value=="Wysłane"?packageNumber:null,
        //            OrderViewModelsSummary = new OrderViewModelsSummary
        //            {
        //                Firma = order.NazwaFirmy != null,
        //                TotalTotalValue = order.TotalPrice,
        //                Discount = order.Discount,
        //                DiscountValue = discounvalue.ToString("c"),
        //                OrderPayment = new SharedShippingOrderSummaryModels
        //                {
        //                    Description = order.Payment.PaymentDescription,
        //                    Name = order.Payment.PaymentName,
        //                    Price = order.Payment.PaymentPrice
        //                },
        //                OrderShipping = new SharedShippingOrderSummaryModels
        //                {
        //                    Description = order.Shipping.ShippingDescription,
        //                    Name = order.Shipping.ShippingName,
        //                    Price = order.Shipping.ShippingPrice
        //                },
        //                UserAddressModel = new CartAddressModel
        //                {
        //                    Email = order.Email,
        //                    Imie = order.Name,
        //                    KodPocztowy = order.PostalCode,
        //                    Miasto = order.City,
        //                    NazwaFirmy = order.NazwaFirmy,
        //                    Nazwisko = order.Surname,
        //                    Nip = order.OrderNip,
        //                    Numer = order.Number,
        //                    Telefon = order.Phone,
        //                    Ulica = order.Street
        //                },
        //                OrderViewItemsTotal = new OrderViewItemsTotal
        //                {
        //                    TotalValue = order.OrderDetails.Sum(x => x.SubTotalPrice),
        //                    OrderProductList = order.OrderDetails.Select(m => new OrderItemSummary
        //                    {
        //                        Image = m.Products.IconName ?? "NoPhoto_small",
        //                        Name = m.Products.Name,
        //                        Price = m.Products.Price ?? 0,
        //                        Quantity = m.Quantity,
        //                        TotalValue = m.SubTotalPrice,
        //                        Packing = m.Products.Packing
        //                    }).ToList()
        //                }
        //            }
        //        };
        //        email.Send();
        //    }
        //    return Json(GetOrderChangeOptions(value), JsonRequestBehavior.AllowGet);
        //}

        public virtual PartialViewResult ChangeOrderStateFromDetails(int id, string value, string packageNumber)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == id);
            if (order != null && order.OrderInfo != value)
            {
                if (value == "Wysłane")
                {
                    foreach (var item in order.OrderDetails)
                    {
                        item.Products.ReservedQuantity -= item.Quantity;
                        item.Products.Quantity -= item.Quantity;
                    }
                }
                order.OrderInfo = value;
                _dbContext.Orders.AddOrUpdate(order);
                _dbContext.SaveChanges();
                ViewBag.AjaxOrderState = order.OrderInfo;
                ViewBag.OrderId = id;
                var discounvalue = ((order.OrderDetails.Sum(orderdetail => orderdetail.SubTotalPrice) * order.Discount) *
                                    100) / 100;
                var email = new ChangeOrderStateEmail
                {
                    To = order.Email,
                    Id = order.OrderId,
                    State = value,
                    StateDescription = SetStateDescription(value),
                    PackageName = value == "Wysłane" ? packageNumber : null,
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
                            Price = order.FreeShippingPayment ? 0 : order.Payment.PaymentPrice
                        },
                        OrderShipping = new SharedShippingOrderSummaryModels
                        {
                            Description = order.Shipping.ShippingDescription,
                            Name = order.Shipping.ShippingName,
                            Price = order.FreeShippingPayment ? 0 : order.Shipping.ShippingPrice
                        },
                        UserAddressModel = new CartAddressModel
                        {
                            Email = order.Email,
                            Imie = order.Name,
                            KodPocztowy = order.PostalCode,
                            Miasto = order.City,
                            NazwaFirmy = order.NazwaFirmy,
                            Nazwisko = order.Surname,
                            Nip = order.OrderNip,
                            Numer = order.Number,
                            Telefon = order.Phone,
                            Ulica = order.Street
                        },
                        OrderViewItemsTotal = new OrderViewItemsTotal
                        {
                            TotalValue = order.OrderDetails.Sum(x => x.SubTotalPrice),
                            OrderProductList = order.OrderDetails.Select(m => new OrderItemSummary
                            {
                                Image = m.Products.IconName ?? "NoPhoto_small",
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
            var list = GetOrderDetailsStateList(value);
            return PartialView(MVC.Admin.Zamowienia.Views._ChangeOrderStateFromDetails,list);
        }

        public virtual ActionResult SzczegolyZamowienia(int id=1)
        {
            var order = _dbContext.Orders.First(m => m.OrderId == id);
            var model = new AdminOrderViewModelsSummary
            {
                OrderWeight = order.OrderWeight,
                Id = id,
                Firma = order.OrderNip != null,
                UserOrderInfo = order.UserHints,
                UserId = order.UserId,
                OrderPayment = new SharedShippingOrderSummaryModels
                {
                    Description = order.Payment.PaymentDescription,
                    Id = order.PaymentId,
                    Name = order.Payment.PaymentName,
                    Price = order.FreeShippingPayment ? 0 : order.Payment.PaymentPrice
                },
                OrderShipping = new SharedShippingOrderSummaryModels
                {
                    Description = order.Shipping.ShippingDescription,
                    Id = order.ShippingId,
                    Name = order.Shipping.ShippingName,
                    Price = order.FreeShippingPayment ? 0 : order.Shipping.ShippingPrice
                },
                TotalTotalValue = order.TotalPrice.ToString("c"),
                OrderInfo = order.OrderInfo,
                OrderDat = order.OrderDate.ToShortDateString(),
                Discount = order.Discount,
                DiscountValue =
                    (order.TotalPrice -
                     (order.ProductsPrice + (order.FreeShippingPayment?0: order.Payment.PaymentPrice) + (order.FreeShippingPayment?0:order.Shipping.ShippingPrice))).ToString("c"),
                UserAddressModel = new CartAddressModel
                {
                    Imie = order.Name,
                    KodPocztowy = order.PostalCode,
                    Miasto = order.City,
                    NazwaFirmy = order.NazwaFirmy,
                    Nazwisko = order.Surname,
                    Nip = order.OrderNip,
                    Numer = order.Number,
                    Telefon = order.Phone,
                    Ulica = order.Street,
                    Email = order.Email
                },
                OrderViewItemsTotal = new OrderViewItemsTotal
                {
                    OrderProductList = order.OrderDetails.Select(x => new OrderItemSummary
                    {
                        Name = x.Products.Name,
                        Image = x.Products.IconName ?? "NoPhoto_small",
                        Price = x.ProdPrice,
                        Quantity = x.Quantity,
                        TotalValue = x.SubTotalPrice,
                        Packing = x.Products.Packing,
                    }).ToList(),
                    TotalValue = order.ProductsPrice
                }
            };
            if (order.OrderInfo != "Wysłane" && order.OrderInfo != "Anulowane")
                ViewBag.OrderStateList = GetOrderDetailsStateList(order.OrderInfo);
            return View(model);
        }

        public virtual FileResult DownloadInvoice()
        {
            var workStream = CreatePdf();
            var byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf");
        }

        private List<SelectListItem> GetOrderDetailsStateList(string stan)
        {
            switch (stan)
            {
                case "Przyjęte":
                {
                    var list = new List<SelectListItem>
                    {
                        new SelectListItem
                        {
                            Selected = true,
                            Text = "Przyjęte",
                            Value = "Przyjęte"
                        },
                        new SelectListItem
                        {
                            Selected = false,
                            Text = "Realizowane",
                            Value = "Realizowane"
                        },
                        new SelectListItem
                        {
                            Selected = false,
                            Text = "Wysłane",
                            Value = "Wysłane"
                        }
                    };
                    return list;
                }

                case "Oczekujące":
                {
                    var list = new List<SelectListItem>
                    {
                        new SelectListItem
                        {
                            Selected = true,
                            Text = "Oczekujące",
                            Value = "Oczekujące"
                        },
                        new SelectListItem
                        {
                            Selected = false,
                            Text = "Realizowane",
                            Value = "Realizowane"
                        },
                        new SelectListItem
                        {
                            Selected = false,
                            Text = "Wysłane",
                            Value = "Wysłane"
                        }
                    };
                    return list;
                }

                case "Realizowane":
                {
                    var list = new List<SelectListItem>
                    {
                        new SelectListItem
                        {
                            Selected = true,
                            Text = "Realizowane",
                            Value = "Realizowane"
                        },
                        new SelectListItem
                        {
                            Selected = false,
                            Text = "Wysłane",
                            Value = "Wysłane"
                        }
                    };
                    return list;
                }
                default:
                    return new List<SelectListItem>();
            }
        }

        protected MemoryStream CreatePdf()
        {
            // Create a Document object
            var document = new Document();
            //MemoryStream
            var pdfData = new MemoryStream();
            PdfWriter.GetInstance(document, pdfData).CloseStream = false;
            //Full path to the Unicode Arial file
            var segoeUniTtf = Path.Combine(HttpRuntime.AppDomainAppPath, "Content/fonts/segoeui.ttf");
            //Create a base font object making sure to specify IDENTITY-H
            var bff = BaseFont.CreateFont(segoeUniTtf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //Create a specific font object
            var fNormal = new Font(bff, 10, Font.NORMAL);
            var fBold = new Font(bff, 10, Font.BOLD);
            var fTitleB = new Font(bff, 14, Font.BOLD);
            var fTitleN = new Font(bff, 14, Font.NORMAL);
            // Open the Document for writing
            document.Open();
            document.NewPage();
            var headerPhrase = new Paragraph("Kęty, dnia 01.10.1993", fNormal)
            {
                Alignment = Element.ALIGN_RIGHT,
                SpacingAfter = 10
            };
            document.Add(headerPhrase);
            var headerTable = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            headerTable.SetWidths(new[] {4.5f, 1, 4.5f});
            headerTable.DefaultCell.Border = Rectangle.NO_BORDER;
            var leftHeaderTable = new PdfPTable(1) {WidthPercentage = 45};
            leftHeaderTable.DefaultCell.Border = Rectangle.BOX;
            var sellerTitleCell = new PdfPCell(new Phrase("Sprzedawca", fBold))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER,
                BorderWidth = 0.5f
            };
            leftHeaderTable.AddCell(sellerTitleCell);
            var sellerNameCell = new PdfPCell(new Phrase("Tomasz Kamiński", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidth = 0.5f
            };
            leftHeaderTable.AddCell(sellerNameCell);
            var streetCell = new PdfPCell(new Phrase("ul. Maków 12", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidth = 0.5f
            };
            leftHeaderTable.AddCell(streetCell);
            var postalecodeCell = new PdfPCell(new Phrase("43-200 Pszczyna", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidth = 0.5f
            };
            leftHeaderTable.AddCell(postalecodeCell);
            var nipcodeCell = new PdfPCell(new Phrase("NIP 1234567890", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                BorderWidth = 0.5f
            };
            leftHeaderTable.AddCell(nipcodeCell);

            headerTable.AddCell(leftHeaderTable);
            headerTable.AddCell("");

            var rightHeaderTable = new PdfPTable(1) {WidthPercentage = 45};
            rightHeaderTable.DefaultCell.Border = Rectangle.BOX;
            var buyerTitleCell = new PdfPCell(new Phrase("Nabywca", fBold))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER,
                BorderWidth = 0.5f
            };
            rightHeaderTable.AddCell(buyerTitleCell);
            var buyerNameCell = new PdfPCell(new Phrase("Tomasz Kamiński", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidth = 0.5f
            };
            rightHeaderTable.AddCell(buyerNameCell);
            var buyerCell = new PdfPCell(new Phrase("ul. Maków 12", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidth = 0.5f
            };
            rightHeaderTable.AddCell(buyerCell);
            var buyerpostalcodeCell = new PdfPCell(new Phrase("43-200 Pszczyna", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                BorderWidth = 0.5f
            };
            rightHeaderTable.AddCell(buyerpostalcodeCell);
            var buyernipcodeCell = new PdfPCell(new Phrase("NIP 1234567890", fNormal))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER,
                BorderWidth = 0.5f
            };
            rightHeaderTable.AddCell(buyernipcodeCell);
            headerTable.AddCell(rightHeaderTable);


            document.Add(headerTable);
            var fakturaNrTable = new PdfPTable(2)
            {
                WidthPercentage = 55
            };
            fakturaNrTable.SetWidths(new[] {6, 4});
            fakturaNrTable.DefaultCell.Border = Rectangle.NO_BORDER;
            fakturaNrTable.SpacingBefore = 10;
            var leftfakturaTable = new PdfPTable(1) {WidthPercentage = 60};
            leftfakturaTable.DefaultCell.Border = Rectangle.BOX;
            var sfakturaTableTitleCell = new PdfPCell(new Phrase("FAKTURA Nr", fTitleB))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            leftfakturaTable.AddCell(sfakturaTableTitleCell);
            fakturaNrTable.AddCell(leftfakturaTable);
            var rightFakturaHeaderTable = new PdfPTable(1) {WidthPercentage = 40};
            rightFakturaHeaderTable.DefaultCell.Border = Rectangle.BOX;
            var rightfakturaheadercell = new PdfPCell(new Phrase("52517242", fTitleN))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            rightFakturaHeaderTable.AddCell(rightfakturaheadercell);
            fakturaNrTable.AddCell(rightFakturaHeaderTable);
            document.Add(fakturaNrTable);
            document.Close();
            return pdfData;
        }

        private static string SetStateDescription(string state)
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
                        String.Format("Stan Twojego zamówienia został zmieniony na {0}.", state);
                    break;
            }
            return stateDescription;
        }

        private static object[] GetOrderChangeOptions(string stan)
        {
            switch (stan)
            {
                case "Przyjęte":
                {
                    object[] obj =
                    {
                        new {label = "Przyjęte", value = 1, selected = true},
                        new {label = "Realizowane", value = 3},
                        new {label = "Wysłane", value = 4}
                    };
                    return obj;
                }

                case "Oczekujące":
                {
                    object[] obj =
                    {
                        new {label = "Oczekujące", value = 2, selected = true},
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
                        new {label = "Realizowane", value = 3, selected = true},
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