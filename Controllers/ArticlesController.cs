using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using lab12_1.Data;
using lab12_1.Models;
using lab12_1.ViewModels;
using System.IO;
using System.Globalization;

namespace lab12_1.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public ArticlesController(MyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostingEnvironment = webHostEnvironment;

        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var articleWithCategoryContext = _context.Article.Include(c => c.Category);
            return View(await articleWithCategoryContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Photo,Category")] ArticleCreateViewModel articleView)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadFolder(articleView);

                Article article = new Article()
                {
                    Id = articleView.Id,
                    Name = articleView.Name,
                    CategoryId = articleView.Category,
                    Photo = uniqueFileName,
                    Price = Double.Parse(articleView.Price)
                };
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", articleView.Category);
            return View(articleView);
        }

        private string UploadFolder(ArticleCreateViewModel articleView)
        {
            string uniqueFileName = null;

            if(articleView.Photo != null)
            {
                string uploadFolder = Path.Combine(_webHostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + articleView.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    articleView.Photo.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = "no-image.png";
            }
            return uniqueFileName;
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", article.CategoryId);
            ArticleCreateViewModel articleView = new ArticleCreateViewModel()
            {
                Id = article.Id,
                Name = article.Name,
                Category = article.CategoryId,
                PhotoFilename = article.Photo,
                Price = article.Price.ToString()
            };
            return View(articleView);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Photo,Category")] ArticleCreateViewModel articleView)
        {

            if (id != articleView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var article = await _context.Article.FindAsync(id);
                    article.Id = articleView.Id;
                    article.Name = articleView.Name;
                    article.CategoryId = articleView.Category;
                    article.Price = Double.Parse(articleView.Price);
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(articleView.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", articleView.Category);
            return View(articleView);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.FindAsync(id);
            RemoveImage(article);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void RemoveImage(Article article)
        {
            if(!article.Photo.Equals("no-image.png"))
            {
                string uploadFolder = Path.Combine(_webHostingEnvironment.WebRootPath, "images");
                if(System.IO.File.Exists(Path.Combine(uploadFolder, article.Photo)))
                {
                    System.IO.File.Delete(Path.Combine(uploadFolder, article.Photo));
                }
            }
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
