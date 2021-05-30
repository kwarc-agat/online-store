using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace lab12_1.ViewModels
{
    public class ArticleCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Too short")]
        [MaxLength(40, ErrorMessage = "Too long")]
        public string Name { get; set; }
        

        public string Price { get; set; }
        public IFormFile Photo { get; set; }
        public string PhotoFilename { get; set; }
        [Required]
        public int Category { get; set; }

    }
}
