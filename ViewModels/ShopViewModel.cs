using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab12_1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab12_1.ViewModels
{
    public class ShopViewModel
    {
        public List<Article> Articles { get; set; }
        public SelectList Categories { get; set; }
        public string ArticleCategory { get; set; }
    }
}
