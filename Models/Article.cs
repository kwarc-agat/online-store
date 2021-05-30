using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace lab12_1.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Too short")]
        [MaxLength(40, ErrorMessage = "Too long")]
        public string Name { get; set; }
        
        public double Price { get; set; }
        public string Photo { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Article()
        {

        }
        public Article(int id, string name, double price, string photoFilename, int categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            Photo = photoFilename;
            CategoryId = categoryId;
        }
    }
}
