#region

using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
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

        [HttpPost]
        public virtual JsonResult ChangeOrderState(int id, string value)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == id);
            if (order != null && order.OrderInfo != value)
            {
                order.OrderInfo = value;
                _dbContext.Orders.AddOrUpdate(order);
                _dbContext.SaveChanges();

                var discounvalue = ((order.OrderDetails.Sum(orderdetail => orderdetail.SubTotalPrice)*order.Discount)*
                                    100)/100;
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
            return Json(GetOrderChangeOptions(value), JsonRequestBehavior.AllowGet);
        }

        public virtual FileResult DownloadInvoice()
        {
            var workStream = CreatePdf();
            var byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf");
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
            var f = new Font(bff, 12, Font.NORMAL);

            // Open the Document for writing
            document.Open();
            //Add elements to the document here

            #region Top table

            // Create the header table 
            var headertable = new PdfPTable(3)
            {
                HorizontalAlignment = 0,
                WidthPercentage = 100
            };
            headertable.SetWidths(new float[] {4, 2, 4}); // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.BOX; //for testing
            headertable.SpacingAfter = 30;
            var nested = new PdfPTable(1);
            nested.DefaultCell.Border = Rectangle.BOX;
            var nextPostCell1 = new PdfPCell(new Phrase("ABC Co.,Ltd", f))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER
            };
            nested.AddCell(nextPostCell1);
            var nextPostCell2 = new PdfPCell(new Phrase("111/206 Moo 9, Ramkhamheang Road,", f))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER
            };
            nested.AddCell(nextPostCell2);
            var nextPostCell3 = new PdfPCell(new Phrase("Nonthaburi 11120", f))
            {
                Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER
            };
            nested.AddCell(nextPostCell3);
            var nesthousing = new PdfPCell(nested)
            {
                Rowspan = 4,
                Padding = 0f
            };
            headertable.AddCell(nesthousing);

            headertable.AddCell("");
            var invoiceCell = new PdfPCell(new Phrase("INVOICE", f))
            {
                HorizontalAlignment = 2,
                Border = Rectangle.NO_BORDER
            };
            headertable.AddCell(invoiceCell);
            var noCell = new PdfPCell(new Phrase("No :", f))
            {
                HorizontalAlignment = 2,
                Border = Rectangle.NO_BORDER
            };
            headertable.AddCell(noCell);
            headertable.AddCell(new Phrase("5", f));
            var dateCell = new PdfPCell(new Phrase("Date :", f))
            {
                HorizontalAlignment = 2,
                Border = Rectangle.NO_BORDER
            };
            headertable.AddCell(dateCell);
            headertable.AddCell(new Phrase(DateTime.Now.ToShortDateString(), f));
            var billCell = new PdfPCell(new Phrase("Bill To :", f))
            {
                HorizontalAlignment = 2,
                Border = Rectangle.NO_BORDER
            };
            headertable.AddCell(billCell);
            headertable.AddCell(new Phrase("CustomerName" + "\n" + "CustomerAddress", f));
            document.Add(headertable);

            #endregion

            #region Items Table

            //Create body table
            var itemTable = new PdfPTable(4) {HorizontalAlignment = 0, WidthPercentage = 100};
            itemTable.SetWidths(new float[] {10, 40, 20, 30}); // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            var cell1 = new PdfPCell(new Phrase("NO", f)) {HorizontalAlignment = 1};
            itemTable.AddCell(cell1);
            var cell2 = new PdfPCell(new Phrase("ITEM", f)) {HorizontalAlignment = 1};
            itemTable.AddCell(cell2);
            var cell3 = new PdfPCell(new Phrase("QUANTITY", f)) {HorizontalAlignment = 1};
            itemTable.AddCell(cell3);
            var cell4 = new PdfPCell(new Phrase("AMOUNT(USD)", f)) {HorizontalAlignment = 1};
            itemTable.AddCell(cell4);

            //foreach (DataRow row in dt.Rows)
            //{
            //    PdfPCell numberCell = new PdfPCell(new Phrase(row["NO"].ToString(), bodyFont));
            //    numberCell.HorizontalAlignment = 0;
            //    numberCell.PaddingLeft = 10f;
            //    numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //    itemTable.AddCell(numberCell);

            //    PdfPCell descCell = new PdfPCell(new Phrase(row["ITEM"].ToString(), bodyFont));
            //    descCell.HorizontalAlignment = 0;
            //    descCell.PaddingLeft = 10f;
            //    descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //    itemTable.AddCell(descCell);

            //    PdfPCell qtyCell = new PdfPCell(new Phrase(row["QUANTITY"].ToString(), bodyFont));
            //    qtyCell.HorizontalAlignment = 0;
            //    qtyCell.PaddingLeft = 10f;
            //    qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //    itemTable.AddCell(qtyCell);

            //    PdfPCell amtCell = new PdfPCell(new Phrase(row["AMOUNT"].ToString(), bodyFont));
            //    amtCell.HorizontalAlignment = 1;
            //    amtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            //    itemTable.AddCell(amtCell);

            //}
            // Table footer
            var totalAmtCell1 = new PdfPCell(new Phrase("")) {Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER};
            itemTable.AddCell(totalAmtCell1);
            var totalAmtCell2 = new PdfPCell(new Phrase("")) {Border = Rectangle.TOP_BORDER};
            itemTable.AddCell(totalAmtCell2);
            var totalAmtStrCell = new PdfPCell(new Phrase("Total Amount", f))
            {
                Border = Rectangle.TOP_BORDER,
                HorizontalAlignment = 1
            };
            itemTable.AddCell(totalAmtStrCell);
            var totalAmtCell = new PdfPCell(new Phrase(60.ToString("#,###.00"), f)) {HorizontalAlignment = 1};
            itemTable.AddCell(totalAmtCell);

            var cell =
                new PdfPCell(new Phrase("*** Please note that ABC Co., Ltd’s bank account is USD Bank Account ***", f))
                {
                    Colspan = 4,
                    HorizontalAlignment = 1
                };
            itemTable.AddCell(cell);
            document.Add(itemTable);

            #endregion

            var transferBank = new Chunk("Your Bank Account:", f);
            transferBank.SetUnderline(0.1f, -2f); //0.1 thick, -2 y-location
            document.Add(transferBank);
            document.Add(Chunk.NEWLINE);

            // Bank Account Info
            var bottomTable = new PdfPTable(3) {HorizontalAlignment = 0, TotalWidth = 300f};
            bottomTable.SetWidths(new[] {90, 10, 200});
            bottomTable.LockedWidth = true;
            bottomTable.SpacingBefore = 20;
            bottomTable.DefaultCell.Border = Rectangle.NO_BORDER;
            bottomTable.AddCell(new Phrase("Account No", f));
            bottomTable.AddCell(":");
            bottomTable.AddCell(new Phrase(5.ToString(), f));
            bottomTable.AddCell(new Phrase("Account Name", f));
            bottomTable.AddCell(":");
            bottomTable.AddCell(new Phrase("AccName", f));
            bottomTable.AddCell(new Phrase("Branch", f));
            bottomTable.AddCell(":");
            bottomTable.AddCell(new Phrase("Asd", f));
            bottomTable.AddCell(new Phrase("Bank", f));
            bottomTable.AddCell(":");
            bottomTable.AddCell(new Phrase("Millenium Bank", f));
            document.Add(bottomTable);

            // Close the Document without closing the underlying stream
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
                        new {label = "Wysłane", value = 4},
                        new {label = "Realizowane", value = 3}
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