using AuthorInfo.API.Models;
using AuthorInfo.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorInfoRepository _authorInfoRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorInfoRepository authorInfoRepository, IMapper mapper)
        {
            _authorInfoRepository = authorInfoRepository ?? throw new ArgumentNullException(nameof(authorInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authorEntities = _authorInfoRepository.GetAuthors();

            return Ok(_mapper.Map<IEnumerable<AuthorWithoutBooksDto>>(authorEntities));
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id, bool includeBooks = false)
        {
            var author = _authorInfoRepository.GetAuthor(id, includeBooks);

            if (author == null)
            {
                return NotFound();
            }

            if (includeBooks)
                return Ok(_mapper.Map<AuthorDto>(author));

            return Ok(_mapper.Map<AuthorWithoutBooksDto>(author));
        }
    }
}
