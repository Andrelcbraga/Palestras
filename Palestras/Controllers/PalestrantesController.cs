using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Palestras.Models;
using Palestras.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Palestras.Controllers
{
    public class PalestrantesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PalestrantesController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Palestrantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Palestrantes.ToListAsync());
        }

        // GET: Palestrantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palestrante = await _context.Palestrantes
                .FirstOrDefaultAsync(m => m.Id == id);

            var palestranteViewModel = new PalestranteViewModel()
            {
                Id = palestrante.Id,
                Nome = palestrante.Nome,
                Qualificacao = palestrante.Qualificacao,
                Experiencia = palestrante.Experiencia,
                DataPalestra = palestrante.DataPalestra,
                HoraPalestra = palestrante.HoraPalestra,
                Local = palestrante.Local,
                ImagemExistente = palestrante.Foto
            };

            if (palestrante == null)
            {
                return NotFound();
            }

            return View(palestrante);
        }

        // GET: Palestrantes/Create
        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PalestranteViewModel model)
        {
            if (ModelState.IsValid)
            {
                string nomeArquivoImagem = ProcessaUploadedFile(model);

                Palestrante palestrante = new Palestrante
                {
                    Nome = model.Nome,
                    Qualificacao = model.Qualificacao,
                    Experiencia = model.Experiencia,
                    DataPalestra = model.DataPalestra,
                    HoraPalestra = model.HoraPalestra,
                    Local = model.Local,
                    Foto = nomeArquivoImagem
                };

                _context.Add(palestrante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private string ProcessaUploadedFile(PalestranteViewModel model)
        {
            string nomeArquivoImagem = null;

            if (model.PalestranteFoto != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                nomeArquivoImagem = Guid.NewGuid().ToString() + "_" + model.PalestranteFoto.FileName;
                string filePath = Path.Combine(uploadsFolder, nomeArquivoImagem);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PalestranteFoto.CopyTo(fileStream);
                }
            }
            return nomeArquivoImagem;
        }

        // GET: Palestrantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palestrante = await _context.Palestrantes.FindAsync(id);

            var palestranteViewModel = new PalestranteViewModel()
            {
                Id = palestrante.Id,
                Nome = palestrante.Nome,
                Qualificacao = palestrante.Qualificacao,
                Experiencia = palestrante.Experiencia,
                DataPalestra = palestrante.DataPalestra,
                HoraPalestra = palestrante.HoraPalestra,
                Local = palestrante.Local,
                ImagemExistente = palestrante.Foto
            };

            if (palestrante == null)
            {
                return NotFound();
            }
            return View(palestranteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PalestranteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var palestrante = await _context.Palestrantes.FindAsync(model.Id);

                    palestrante.Nome = model.Nome;
                    palestrante.Qualificacao = model.Qualificacao;
                    palestrante.Experiencia = model.Experiencia;
                    palestrante.DataPalestra = model.DataPalestra;
                    palestrante.HoraPalestra = model.HoraPalestra;
                    palestrante.Local = model.Local;

                    if (model.PalestranteFoto != null)
                    {
                        if (model.ImagemExistente != null)
                        {
                            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ImagemExistente);
                            System.IO.File.Delete(filePath);
                        }

                        palestrante.Foto = ProcessaUploadedFile(model);
                    }

                    _context.Update(palestrante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Palestrantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palestrante = await _context.Palestrantes
                .FirstOrDefaultAsync(m => m.Id == id);

            var palestranteViewModel = new PalestranteViewModel()
            {
                Id = palestrante.Id,
                Nome = palestrante.Nome,
                Qualificacao = palestrante.Qualificacao,
                Experiencia = palestrante.Experiencia,
                DataPalestra = palestrante.DataPalestra,
                HoraPalestra = palestrante.HoraPalestra,
                Local = palestrante.Local,
                ImagemExistente = palestrante.Foto
            };

            if (palestrante == null)
            {
                return NotFound();
            }

            return View(palestranteViewModel);
        }

        // POST: Palestrantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var palestrante = await _context.Palestrantes.FindAsync(id);

            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", palestrante.Foto);
            
            _context.Palestrantes.Remove(palestrante);

            if (await _context.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        private bool PalestranteExists(int id)
        {
            return _context.Palestrantes.Any(e => e.Id == id);
        }
    }
}
