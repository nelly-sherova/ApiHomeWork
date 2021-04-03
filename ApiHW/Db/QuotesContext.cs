using ApiHW.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiHW.Db
{
    public class QuotesContext : DbContext
    {
        public DbSet<Quotes> Quotes { get; set; }
        public QuotesContext (DbContextOptions<QuotesContext> options)
            :base (options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Quotes>().HasData(
                 new Quotes
                 {
                     Id = 1,
                     Text = "Что разум человека может постигнуть и во что он может поверить, того он способен достичь",
                     Author = "Наполеон Хилл",
                     InsertDate = DateTime.Parse("15.10.2020")
                 },
                    new Quotes
                    {
                        Id = 2,
                        Text = "Стремитесь не к успеху, а к ценностям, которые он дает",
                        Author = "Альберт Эйнштейн",
                        InsertDate = DateTime.Parse("02.11.1890")
                    },
                    new Quotes
                    {
                        Id = 3,
                        Text = "Надо любить жизнь больше, чем смысл жизни.",
                        Author = "Достоевский",
                        InsertDate = DateTime.Parse("06.06.1890")
                    },
                    new Quotes
                    {
                        Id = 4,
                        Text = "Сложнее всего начать действовать, все остальное зависит только от упорства",
                        Author = "Амелия Эрхарт",
                        InsertDate = DateTime.Parse("25.05.1800")
                    },
                    new Quotes
                    {
                        Id = 5,
                        Text = "Начинать всегда стоит с того, что сеет сомнения.",
                        Author = "Борис Стругацкий",
                        InsertDate = DateTime.Parse("1.1.1100")
                    }
                    );
        }
    }
}
