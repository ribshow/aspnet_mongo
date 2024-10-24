﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnet_mongo.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;

namespace aspnet_mongo.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ContextMongoDb _context;

        public UsersController(ContextMongoDb context)
        {
            _context = context;
        }
        // GET: Users
        public async Task<IActionResult> Index()
        { 
            // criando o contexto do mongoDB e usando para a consulta
            return View(await _context.User.Find(u => true).ToListAsync());
        }

        // GET: Users/Details/5
    
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Compara o id do banco de dados, com o id passado no parâmetro 
            var user = await _context.User.Find(m => m.Id == id).FirstOrDefaultAsync();   
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Email, Password, ConfirmPassword, Name, UserName, Gender, Age, Image_url")] ApplicationUser user, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    // Caminho para salvar a imagem na pasta wwwroot/assets/users
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/users", fileName);

                    // salvar a imagem no disco
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    user.Image_url = fileName;
                }
                user.Id = Guid.NewGuid();

                await _context.User.InsertOneAsync(user);
                
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Age")] ApplicationUser user, IFormFile Image)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Image != null && Image.Length > 0)
                    {
                        // Caminho para salvar a imagem na pasta wwwroot/assets/users
                        var fileName = Path.GetFileName(Image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/users", fileName);

                        // salvar a imagem no disco
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }

                        user.Image_url = fileName;
                    }
                    else
                    {
                        user.Image_url = user.Image_url;
                    }
                    // ele irá atualizar o usuário cujo id no banco seja igual o id do objeto usuário
                    await _context.User.ReplaceOneAsync(u => u.Id == user.Id, user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Find(m => m.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _context.User.DeleteOneAsync(u => u.Id == id);

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Find(e => e.Id == id).Any();
        }
    }
}
