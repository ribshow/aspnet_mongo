using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnet_mongo.Data;
using aspnet_mongo.Models;
using MongoDB.Driver;

namespace aspnet_mongo.Controllers
{
    public class Products1Controller : Controller
    {
        private readonly ContextMongoDb _context;

        public Products1Controller(ContextMongoDb context)
        {
            _context = context;
        }

        // GET: Products1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.Find(p=> true).ToListAsync());
        }

        // GET: Products1/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.Find(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Image_url")] Product product, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if(Image != null && Image.Length > 0)
                {
                    // Caminho para salvar a imagem na pasta wwwroot/assets/products
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/products", fileName);

                    // salvar a imagem no disco
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    // armazenar a url no banco de dados
                    product.Image_url = fileName;
                }
                product.Id = Guid.NewGuid();

                await _context.Product.InsertOneAsync(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products1/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Price,Description,Image_url")] Product product, IFormFile Image)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null && Image.Length > 0)
                    {
                        // Caminho para salvar a imagem na pasta wwwroot/assets/products
                        var fileName = Path.GetFileName(Image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/products", fileName);

                        // salvar a imagem no disco
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }

                        // armazenar a url no banco de dados
                        product.Image_url = fileName;
                    }
                    await _context.Product.ReplaceOneAsync(p => p.Id == id, product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products1/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _context.Product.DeleteOneAsync(p => p.Id == id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Find(e => e.Id == id).Any();
        }
    }
}
