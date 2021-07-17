using CleanApplication.Domain.Entities;
using CleanApplication.Domain.ValueObjects;
using CleanApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApplication.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var administratorRole = new ApplicationRole("Administrator");
            var userRole = new ApplicationRole("User");
            var GuestRole = new ApplicationRole("Guest");
            if (roleManager.Roles.All(r => r.Name != GuestRole.Name))
            {
                await roleManager.CreateAsync(GuestRole);
            }
            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }
            if (roleManager.Roles.All(r => r.Name != userRole.Name))
            {
                await roleManager.CreateAsync(userRole);
            }
            var administrator = new ApplicationUser { UserName = "r.man.abi@gmail.com", Email = "r.man.abi@gmail.com" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                //PasswordHasher: Administrator1!
                // hash md5
                await userManager.CreateAsync(administrator, "9FD6F1B118B85684173958AF7F38C96C");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Colour = Colour.Blue,
                    Items =
                    {
                        new TodoItem { Title = "Peugeot", Done = true },
                        new TodoItem { Title = "Renault", Done = true },
                        new TodoItem { Title = "Chrysler", Done = true },
                        new TodoItem { Title = "Chevrolet" },
                        new TodoItem { Title = "Benz" },
                        new TodoItem { Title = "Opel" },
                        new TodoItem { Title = "Subaru" },
                        new TodoItem { Title = "Suzuki" }
                    }
                });

                await context.SaveChangesAsync();
            }
            if (!context.Contents.Any())
            {
                context.Contents.AddRange(
                    new Content
                    {
                        Name = "Content1",
                        IsActive = true,
                        Priority = 1,
                        Slot = "HomePage",
                        Description = "no content"

                    }, new Content
                    {
                        Name = "Content2",
                        IsActive = true,
                        Priority = 2,
                        Slot = "HomePage",
                        Description = "no content"

                    }
                    , new Content
                    {
                        Name = "Content3",
                        IsActive = true,
                        Priority = 3,
                        Slot = "HomePage",
                        Description = "no content"

                    }, new Content
                    {
                        Name = "Content4",
                        IsActive = true,
                        Priority = 4,
                        Slot = "HomePage",
                        Description = "no content"

                    }, new Content
                    {
                        Name = "Content5",
                        IsActive = true,
                        Priority = 5,
                        Slot = "HomePage",
                        Description = "no content"

                    },
                     new Content
                     {
                         Name = "Content6",
                         IsActive = true,
                         Priority = 6,
                         Slot = "HomePageSlider",
                         Description = "no content"

                     },
                      new Content
                      {
                          Name = "Content7",
                          IsActive = true,
                          Priority = 7,
                          Slot = "HomePageSlider",
                          Description = "no content"

                      },
                       new Content
                       {
                           Name = "Content8",
                           IsActive = true,
                           Priority = 8,
                           Slot = "HomePageSlider",
                           Description = "no content"

                       });

                await context.SaveChangesAsync();
            }
        }
    }
}
