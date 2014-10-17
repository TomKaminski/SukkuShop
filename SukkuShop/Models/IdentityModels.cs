using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SukkuShop.Models
{
    public class ApplicationUser : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            Orders = new List<Orders>();
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }

    [Table("Categories")]
    public sealed class Categories
    {
        public Categories()
        {
            Products = new List<Products>();
        }

        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Promotion { get; set; }

        public ICollection<Products> Products { get; set; }
    }

    [Table("Products")]
    public class Products
    {
        public Products()
        {
            OrderDetails = new List<OrderDetails>();
        }

        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string FilePath { get; set; }
        public string Producer { get; set; }
        public int Promotion { get; set; }

        public virtual Categories Categories { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }

    [Table("OrderDetails")]
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Products Products { get; set; }

        public virtual Orders Orders { get; set; }
    }

    [Table("Orders")]
    public class Orders
    {
        public Orders()
        {
            OrderDetails = new List<OrderDetails>();
        }

        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime SentDate { get; set; }
        public string OrderInfo { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
        
        public virtual ApplicationUser User { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public bool Seed(ApplicationDbContext context)
        {
            var roleManager = new ApplicationRoleManager(new RoleStore<RoleIntPk, int, UserRoleIntPk>(context));
            roleManager.Create(new RoleIntPk("Admin", "Admin Role"));


            //Create Admin acc
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser, RoleIntPk, int,
                UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>(context));

            var user = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@sukku.pl"
            };

            userManager.Create(user, "Admin123456");
            var success = userManager.AddToRole(user.Id, "Admin");
            return success.Succeeded;
        }

        /// Context Initializer
        public class DropCreateInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public ApplicationDbContext Context { get; set; }
    }

    //New drived classes 
    public class UserRoleIntPk : IdentityUserRole<int>
    {
    }

    public class UserClaimIntPk : IdentityUserClaim<int>
    {
    }

    public class UserLoginIntPk : IdentityUserLogin<int>
    {
    }

    public class RoleIntPk : IdentityRole<int, UserRoleIntPk>
    {
        public RoleIntPk()
        {
        }

        public RoleIntPk(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Description { get; set; }
    }

    public class UserStoreIntPk : UserStore<ApplicationUser, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public UserStoreIntPk(DbContext context)
            : base(context)
        {
        }
    }

    public class RoleStoreIntPk : RoleStore<RoleIntPk, int, UserRoleIntPk>
    {
        public RoleStoreIntPk(DbContext context)
            : base(context)
        {
        }
    }
}