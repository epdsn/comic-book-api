using Microsoft.AspNetCore.Mvc;
using ComicBookApi.Models;
using ComicBookApi.Repositories;

namespace ComicBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicBooksController : ControllerBase
    {
        private readonly IComicBookRepository _repository;

        public ComicBooksController(IComicBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetComicBooks()
        {
            var comicBooks = await _repository.GetAllAsync();
            return Ok(comicBooks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComicBookById(int id)
        {
            var comicBook = await _repository.GetByIdAsync(id);
            if (comicBook == null)
                return NotFound();
            
            return Ok(comicBook);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComicBook([FromBody] ComicBook comicBook)
        {
            var createdComicBook = await _repository.CreateAsync(comicBook);
            return CreatedAtAction(nameof(GetComicBookById), new { id = createdComicBook.Id }, createdComicBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComicBook(int id, [FromBody] ComicBook comicBook)
        {
            if (id != comicBook.Id)
                return BadRequest();
            
            var updatedComicBook = await _repository.UpdateAsync(comicBook);
            return Ok(updatedComicBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComicBook(int id)
        {
            var deletedComicBook = await _repository.DeleteAsync(id);
            if (deletedComicBook == null)
                return NotFound();
            
            return Ok();
        }
    }
}
