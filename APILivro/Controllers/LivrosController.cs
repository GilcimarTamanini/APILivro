using APILivro.Models;
using APILivro.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APILivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivrosController(LivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public ActionResult<List<Livro>> Get() =>
            _livroService.Get();

        [HttpGet("{id:length(24)}", Name = "GetLivro")]
        public ActionResult<Livro> Get(string id)
        {
            var livro = _livroService.Get(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        [HttpGet("titulo", Name = "GetTitulo")]
        public ActionResult<Livro> GetTitulo(string titulo)
        {
            var livro = _livroService.GetTitulo(titulo);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        [HttpGet("autor", Name = "GetAutor")]
        public ActionResult<Livro> GetAutor(string autor)
        {
            var livro = _livroService.GetAutor(autor);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        [HttpPost]
        public ActionResult<Livro> Create(Livro livro)
        {
            _livroService.Create(livro);

            return CreatedAtRoute("GetLivro", new { id = livro.Id.ToString() }, livro);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Livro livroIn)
        {
            var livro = _livroService.Get(id);

            if (livro == null)
            {
                return NotFound();
            }

            _livroService.Update(id, livroIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var livro = _livroService.Get(id);

            if (livro == null)
            {
                return NotFound();
            }

            _livroService.Remove(livro.Id);

            return NoContent();
        }
    }
}
