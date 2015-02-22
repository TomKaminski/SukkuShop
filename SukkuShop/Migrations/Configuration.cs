using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SukkuShop.Identity;
using SukkuShop.Models;

namespace SukkuShop.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
                //var roleManager = new ApplicationRoleManager(new RoleStore<RoleIntPk, int, UserRoleIntPk>(context));
                //roleManager.Create(new RoleIntPk("Admin", "Admin Role"));

                ////Create Admin acc
                //var userManager = new ApplicationUserManager(new UserStore<ApplicationUser, RoleIntPk, int,
                //    UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>(context));

                //var user = new ApplicationUser
                //{
                //    Name = "Admin",
                //    Email = "Admin@sukku.pl",
                //    UserName = "Admin@sukku.pl"
                //};

                //userManager.Create(user, "Admin123456");
                //userManager.AddToRole(user.Id, "Admin");

                context.Categories.AddOrUpdate(p => p.CategoryId,
                        new Categories { CategoryId = 1, Name = "Kosmetyki", UpperCategoryId = 0 },
                        new Categories { CategoryId = 2, Name = "Przyprawy", UpperCategoryId = 0 },
                        new Categories { CategoryId = 3, Name = "Herbaty", UpperCategoryId = 0 },
                        new Categories { CategoryId = 4, Name = "Bakalie", UpperCategoryId = 0 },
                        new Categories { CategoryId = 5, Name = "Inne", UpperCategoryId = 0 },
                        new Categories { CategoryId = 6, Name = "Pielêgnacja Cia³a", UpperCategoryId = 1 },
                        new Categories { CategoryId = 7, Name = "Pielêgnacja Twarzy", UpperCategoryId = 1 },
                        new Categories { CategoryId = 8, Name = "Ostre", UpperCategoryId = 2 },
                        new Categories { CategoryId = 9, Name = "£agodne", UpperCategoryId = 2 }
                    );

                context.PaymentTypes.AddOrUpdate(p => p.PaymentId,
                        new PaymentType
                        {
                            PaymentId = 1,
                            PaymentName = "Przedp³ata na konto",
                            PaymentPrice = 0,
                            PaymentDescription = "Przedp³ata na konto OPIS",
                            Active = true
                        },
                        new PaymentType
                        {
                            PaymentId = 2,
                            PaymentName = "P³atnoœæ za pobraniem",
                            PaymentPrice = 5,
                            PaymentDescription = "P³atnoœæ za pobraniem OPIS",
                            Active = true
                        },
                        new PaymentType
                        {
                            PaymentId = 3,
                            PaymentName = "PayU",
                            PaymentPrice = 1,
                            PaymentDescription = "PayU OPIS",
                            Active = true
                        }
                    );

                context.ShippingTypes.AddOrUpdate(p => p.ShippingId,
                    new ShippingType
                    {
                        ShippingId = 1,
                        ShippingName = "List polecony ekonomiczny",
                        ShippingPrice = 5,
                        ShippingDescription = "list polecony ekonomiczny bardzo tani",
                        Active = true
                    },
                    new ShippingType
                    {
                        ShippingId = 2,
                        ShippingName = "List polecony priorytetowy",
                        ShippingPrice = 7,
                        ShippingDescription = "list polecony zwyk³y",
                        Active = true
                    },
                    new ShippingType
                    {
                        ShippingId = 3,
                        ShippingName = "Paczka ekonomiczna",
                        ShippingPrice = 10,
                        ShippingDescription = "Paaaaaaczkaaaa ekonomiczna opis",
                        Active = true
                    },
                    new ShippingType
                    {
                        ShippingId = 4,
                        ShippingName = "Paczka priorytetowa",
                        ShippingPrice = 15,
                        ShippingDescription = "Paka priorytet opis",
                        Active = true
                    },
                    new ShippingType
                    {
                        ShippingId = 5,
                        ShippingName = "Odbiór osobisty",
                        ShippingPrice = 0,
                        ShippingDescription = "Odbiór osobisty OPIS",
                        Active = true
                    }
                    );
                base.Seed(context);
            }
        }
    }

