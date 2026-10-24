using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new BlogDataContext())
        {
            // var user = new User
            // {
            //     Name = "Thyago Euclides",
            //     Slug = "thyagoeuclides",
            //     Email = "thyagomvp@euclides.io",
            //     Bio = "9x Microsoft MVP",
            //     Image = "https://thyago.io",
            //     PasswordHash = "123098457"
            // };

            // var category = new Category
            // {
            //     Name = "Backend",
            //     Slug = "backend"
            // };

            // var post = new Post
            // {
            //     Author = user,
            //     Category = category,
            //     Body = "<p>Hello world</p>",
            //     Slug = "comecando-com-ef-core",
            //     Summary = "Neste artigo vamos aprender EF core",
            //     Title = "Começando com EF Core",
            //     CreateDate = DateTime.Now,
            //     LastUpdateDate = DateTime.Now,
            // };

            // context.Posts.Add(post);
            // context.SaveChanges();

            // var posts = context
            //     .Posts
            //     .AsNoTracking()
            //     .Include(x => x.Author)
            //     .Include(x => x.Category)
            //     .OrderByDescending(x => x.LastUpdateDate)
            //     .ToList();

            // foreach (var post in posts)
            //     Console.WriteLine($"{post.Title} escrito por {post.Author?.Name} em {post.Category?.Name}");

            var post = context
                .Posts
                // .AsNoTracking() PRECISA DO TRACKING
                .Include(x => x.Author)
                .Include(x => x.Category)
                .OrderBy(x => x.LastUpdateDate)
                .FirstOrDefault(); // Pegando o primeiro item
            
            if (post?.Author is not null)
            {
                post.Author.Name = "Thyagão";

                context.Posts.Update(post);
                context.SaveChanges();
            }
        }
    }
}