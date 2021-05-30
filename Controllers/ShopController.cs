using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab12_1.Data;
using lab12_1.Models;
using lab12_1.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace lab12_1.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;

        public ShopController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Shop
        public async Task<IActionResult> Index(string articleCategory)
        {
            IQueryable<string> categoryQuery = from c in _context.Category
                                               orderby c.Name
                                               select c.Name;
            var articles = from a in _context.Article
                           select a;

            if(!string.IsNullOrEmpty(articleCategory))
            {
                articles = articles.Where(a => a.Category.Name == articleCategory);
            }

            var shopView = new ShopViewModel
            {
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                Articles = await articles.Include(c => c.Category).ToListAsync()
            };

            return View(shopView);
        }
        
    }

}
