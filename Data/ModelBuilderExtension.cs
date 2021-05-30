using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab12_1.Models;
using Microsoft.EntityFrameworkCore;

namespace lab12_1.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(
                new Article()
                {
                    Id = 1,
                    Name = "odkurzacz",
                    Photo = "odkuracz.jpg",
                    Price = 599.99,
                    CategoryId = 1
                },
                new Article()
                {
                    Id = 2,
                    Name = "radio",
                    Photo = "radio.jpg",
                    Price = 79.95,
                    CategoryId = 2
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "AGD"
                },
                new Category()
                {
                    Id = 2,
                    Name = "RTV"
                }
                ) ;
        }
    }
}
