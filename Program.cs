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
            // context.Users.Add(new User
            // {

            //     Bio = "11x Microsof MVP",
            //     Email = "thyago11x@euclides.io",
            //     Image = "https://thyago.io",
            //     Name = "Thyago Euclides",
            //     PasswordHash = "1234",
            //     Slug = "thyago11x-euclides"

            // });
            // context.SaveChanges();

            var user = context.Users.FirstOrDefault();
            var post = new Post
            {

                Author = user,
                Body = "Meu artigo",
                Category = new Category
                {
                    Name = "FrontEnd",
                    Slug = "frontend"
                },
                CreateDate = System.DateTime.Now,
                // LastUpdateDate=
                Slug = "meu-artigo",
                Summary = "Neste artigo vamos conferir ... ",
                // Tags=null,
                Title = "Meu artigo",
            };
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}