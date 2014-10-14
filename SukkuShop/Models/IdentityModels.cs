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
            Zamowienia= new List<Zamowienia>();
        }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public string Number { get; set; }
        public string KodPocztowy { get; set; }
        public ICollection<Zamowienia> Zamowienia { get; set; } 

    }

    [Table("Kategorie")]
    public sealed class Kategorie
    {
        public Kategorie()
        {
            Produkty = new List<Produkty>();
        }
        [Key]
        public int KategoriaId { get; set; }

        public string Nazwa { get; set; }

        public int Promocja { get; set; }

        public ICollection<Produkty> Produkty { get; set; } 
    }

    [Table("Produkty")]
    public class Produkty
    {
        public Produkty()
        {
            SzczegZamowienia = new LinkedList<SzczegZamowienia>();
        }

        [Key]
        public int ProduktId { get; set; }

        public string Nazwa { get; set; }

        public string Opis { get; set; }

        public decimal Cena { get; set; }

        public int Ilosc { get; set; }

        public int KategoriaId { get; set; }

        public string FilePath { get; set; }

        public string Producent { get; set; }

        public int Promocja { get; set; }

        public virtual Kategorie Kategorie { get; set; }

        public ICollection<SzczegZamowienia> SzczegZamowienia { get; set; } 
    }

    [Table("SzczegZamowienia")]
    public class SzczegZamowienia
    {
        [Key]
        public int SzczegZamId { get; set; }
        
        public int ProduktId { get; set; }
        public int ZamowienieId { get; set; }
        public int Ilosc { get; set; }

        public virtual Produkty Produkty { get; set; }

        public virtual Zamowienia Zamowienia { get; set; }


    }

    [Table("Zamowienia")]
    public class Zamowienia
    {

        public Zamowienia()
        {
            SzczegZamowienia = new LinkedList<SzczegZamowienia>();
        }
        [Key]
        public int ZamowienieId { get; set; }

        public int UserId { get; set; }
        public DateTime DataZamowienia { get; set; }
        public DateTime DataWysylki { get; set; }
        public string StanZamowienia { get; set; }
        public ICollection<SzczegZamowienia> SzczegZamowienia { get; set; }
        public virtual ApplicationUser User { get; set; }

    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, RoleIntPk, int,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Produkty> Produkty { get; set; }
        public DbSet<Kategorie> Kategorie { get; set; }

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
                UserName = "Admin@example.com",
                Email = "Admin@example.com"
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