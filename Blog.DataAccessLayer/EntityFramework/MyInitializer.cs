using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Blog.Entities;

namespace Blog.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            // Adding admin user..
            BlogUser admin = new BlogUser()
            {
                Name = "Berkin",
                Surname = "Akkaya",
                Email = "berkin@berkin.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "berkinakkaya",
                ProfileImageFilename = "user_boy.png",
                Password = "123",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "berkinakkaya"
            };

            // Adding standart user..
            BlogUser standartUser = new BlogUser()
            {
                Name = "Beko",
                Surname = "Ak",
                Email = "bekoak@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "bekoak",
                Password = "123",
                ProfileImageFilename = "user_boy.png",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "bekoak"
            };
            Category cat = new Category()
            {
                Title = "Test - Kategori",
                Description = "Kategori Testi için oluşturulmuştur",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "bekoak"
            };
            context.Categories.Add(cat);
            cat = new Category()
            {
                Title = "Test - Kategori -2",
                Description = "Kategori - 2 Testi için oluşturulmuştur",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "berkinakkaya"
            };
            context.Categories.Add(cat);
            Note note = new Note()
            {
                Title = "Yazı - 1",
                Text = "Yazı Metni -1",
                IsDraft = false,
                LikeCount = 0,
                CategoryId = 1,
                Owner = standartUser,
                ImagePath = "/Images/125.59.2022.png",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "berkinakkaya"
            };
            context.Notes.Add(note);
            Note note2 = new Note()
            {
                Title = "Yazı - 2",
                Text = "Yazı Metni - 2",
                IsDraft = false,
                LikeCount = 0,
                CategoryId = 2,
                Owner = admin,
                ImagePath = "/Images/3f8a3ce485e39521f5271b2ef01deeb425.59.2022.jpg",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "berkinakkaya"
            };
            context.Notes.Add(note2);
            context.BlogUsers.Add(admin);
            context.BlogUsers.Add(standartUser);


            context.SaveChanges();
        }
    }
}
