using AuthorInfo.API.Models;
using AuthorInfo.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/books")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IMailService _mailService;
        private readonly IAuthorInfoRepository _authorInfoRepository;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IMailService mailService, IAuthorInfoRepository authorInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _authorInfoRepository = authorInfoRepository ?? throw new ArgumentNullException(nameof(authorInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetBooks(int authorId)
        {
            try
            {
                if (!_authorInfoRepository.AuthorExists(authorId))
                {
                    _logger.LogInformation($"Author with id {authorId} was not found when accessing Books.");
                    return NotFound();
                }

                var booksForAuthor = _authorInfoRepository.GetBooksForAuthor(authorId);

                return Ok(_mapper.Map<IEnumerable<BookDto>>(booksForAuthor));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting books for author with id {authorId}", ex);
                return StatusCode(500, "Problem while handling your request.");
            }

        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook(int authorId, int id)
        {
            if (!_authorInfoRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var book = _authorInfoRepository.GetBookForAuthor(authorId, id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public IActionResult CreateBook(int authorId, [FromBody] BookForCreationDto book)
        {
            if (book.Synopsis == book.Title)
            {
                ModelState.AddModelError("Synopsis", "The provided synopsis must be different from the title.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_authorInfoRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var finalBook = _mapper.Map<Entities.Book>(book);

            _authorInfoRepository.AddBookForAuthor(authorId, finalBook);

            _authorInfoRepository.Save();

            var createdBookToReturn = _mapper.Map<BookDto>(finalBook);

            return CreatedAtRoute("GetBook", new { authorId, id = createdBookToReturn.Id }, createdBookToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int authorId, int id, [FromBody] BookForUpdateDto book)
        {
            if (book.Synopsis == book.Title)
            {
                ModelState.AddModelError("Synopsis", "The provided synopsis must be different from the title.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_authorInfoRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookEntity = _authorInfoRepository.GetBookForAuthor(authorId, id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(book, bookEntity);

            _authorInfoRepository.UpdateBookForAuthor(authorId, bookEntity);

            _authorInfoRepository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateBook(int authorId, int id, [FromBody] JsonPatchDocument<BookForUpdateDto> patchDoc)
        {
            if (!_authorInfoRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookEntity = _authorInfoRepository.GetBookForAuthor(authorId, id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            var bookToPatch = _mapper.Map<BookForUpdateDto>(bookEntity);

            patchDoc.ApplyTo(bookToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bookToPatch.Synopsis == bookToPatch.Title)
            {
                ModelState.AddModelError("Synopsis", "The provided synopsis must be different from the title.");
            }

            if (!TryValidateModel(bookToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(bookToPatch, bookEntity);

            _authorInfoRepository.UpdateBookForAuthor(authorId, bookEntity);

            _authorInfoRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int authorId, int id)
        {
            if (!_authorInfoRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookEntity = _authorInfoRepository.GetBookForAuthor(authorId, id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            _authorInfoRepository.DeleteBook(bookEntity);

            _authorInfoRepository.Save();

            _mailService.Send("Book deleted.", $"Book: {bookEntity.Title} with id: {bookEntity.Id} was deleted.");

            return NoContent();
        }
    }
}
