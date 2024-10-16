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
    public class ProductsController : Controller
    {
        private readonly aspnet_mongoContext _context;

        public ProductsController(aspnet_mongoContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            ContextMongoDb contextMongo = new ContextMongoDb(); 
            return View(await contextMongo.Product.Find(p => true).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextMongoDb contextMongo = new ContextMongoDb();
            var product = await contextMongo.Product.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description")] Product product)
        {
            ContextMongoDb contextMongo = new ContextMongoDb();
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();

                await contextMongo.Product.InsertOneAsync(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextMongoDb contextMongo = new ContextMongoDb();

            var product = await contextMongo.Product.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Price,Description")] Product product)
        {
            ContextMongoDb contextMongo = new ContextMongoDb();
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // ele irá atualizar o usuário cujo id no banco seja igual o id do objeto usuário
                    await contextMongo.Product.ReplaceOneAsync(p => p.Id == product.Id, product);
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

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextMongoDb contextMongo = new ContextMongoDb();
            var product = await contextMongo.Product.Find(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            ContextMongoDb contextMongo = new ContextMongoDb();
            await contextMongo.Product.DeleteOneAsync(p => p.Id == id);

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            ContextMongoDb contextMongo = new ContextMongoDb();
            return contextMongo.Product.Find(e => e.Id == id).Any();
        }
    }
}
