using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Data
{
    public class DataInitializer
    {
        public static void SeedData(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            dbContext.Database.Migrate();

            SeedRoles(dbContext);
            SeedUsers(userManager);

            SeedProductCategories(dbContext);
            SeedProducts(dbContext);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            AddUserIfNotExists(userManager, "stefan.holmberg@systementor.se", "Hejsan123#", new string[]{"Admin"});
            AddUserIfNotExists(userManager, "stefan.holmbergmanager@systementor.se", "Hejsan123#", new string[]{"Product Manager"});
        }

        private static void AddUserIfNotExists(UserManager<IdentityUser> userManager,
           string userName, string password, string[] roles)
        {
            if (userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, password).Result;
            var r = userManager.AddToRolesAsync(user, roles).Result;
        }

        private static void SeedRoles(ApplicationDbContext dbContext)
        {
            var role = dbContext.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (role == null)
            {
                dbContext.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "Admin" });
            }

            role = dbContext.Roles.FirstOrDefault(r => r.Name == "Product Manager");
            if (role == null)
            {
                dbContext.Roles.Add(new IdentityRole { Name = "Product Manager", NormalizedName = "Product Manager" });
            }
            dbContext.SaveChanges();
        }

        private static void SeedProductCategories(ApplicationDbContext dbContext)
        {
            var category = dbContext.ProductCategory.FirstOrDefault(r => r.Name == "MobilTelefoner");
            if (category == null)
            {
                dbContext.ProductCategory.Add(new ProductCategory
                {
                    Name = "MobilTelefoner"
                });
            }
            category = dbContext.ProductCategory.FirstOrDefault(r => r.Name == "Ljud och Bild");
            if (category == null)
            {
                dbContext.ProductCategory.Add(new ProductCategory
                {
                    Name = "Ljud och Bild"
                });
            }
            category = dbContext.ProductCategory.FirstOrDefault(r => r.Name == "Dator och Datortillbehör");
            if (category == null)
            {
                dbContext.ProductCategory.Add(new ProductCategory
                {
                    Name = "Dator och Datortillbehör"
                });
            }

            dbContext.SaveChanges();
        }

        private static void SeedProducts(ApplicationDbContext dbContext)
        {
            var product = dbContext.Product.FirstOrDefault(r => r.Name == "Samsung SuperDuper");
            if (product == null)
            {
                dbContext.Product.Add(new Product
                {
                    ProductCategory = dbContext.ProductCategory.First(r => r.Name == "MobilTelefoner"),
                    Name = "Samsung SuperDuper",
                    Description =
                        "Samsungs senaste och bästa super duper telefon med trippla skärmar och galna funktioner",
                    Price = 10000
                });

            }
            else
            {
                product.ProductCategory = dbContext.ProductCategory.First(r => r.Name == "MobilTelefoner");
            }

            product = dbContext.Product.FirstOrDefault(r => r.Name == "Sony 8k 75tums SmartTv");
            if (product == null)
            {
                dbContext.Product.Add(new Product
                {
                    ProductCategory = dbContext.ProductCategory.First(r => r.Name == "Ljud och Bild"),
                    Name = "Sony 8k 75tums SmartTv",
                    Description = "Sonys senaste TV med krispigt ljud och kristallklar bild",
                    Price = 35000
                });
            }
            else
            {
                product.ProductCategory = dbContext.ProductCategory.First(r => r.Name == "Ljud och Bild");
            }
            product = dbContext.Product.FirstOrDefault(r => r.Name == "Intel i7 Superdator");
            if (product == null)
            {
                dbContext.Product.Add(new Product
                {
                    ProductCategory = dbContext.ProductCategory.First(r => r.Name == "Dator och Datortillbehör"),
                    Name = "Intel i7 Superdator",
                    Description = "Intels och antagligen världens snabbaste dator med alla tillbehör inkluderade",
                    Price = 45000
                });
            }

            dbContext.SaveChanges();

        }

    }
}
