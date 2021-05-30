using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab12_1.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Too short")]
        [MaxLength(20, ErrorMessage = "Too long")]
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }

        public Category()
        {

        }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
