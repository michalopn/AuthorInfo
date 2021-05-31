using AuthorInfo.API.Contexts;
using AuthorInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Services
{
    public class AuthorInfoRepository : IAuthorInfoRepository
    {
        private readonly AuthorInfoContext _context;

        public AuthorInfoRepository(AuthorInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Author GetAuthor(int authorId, bool includeBooks)
        {
            if (includeBooks)
                return _context.Authors.Include(a => a.Books).Where(a => a.Id == authorId).FirstOrDefault();

            return _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.OrderBy(a => a.Name).ToList();
        }

        public Book GetBookForAuthor(int authorId, int bookId)
        {
            return _context.Books.Where(b => b.AuthorId == authorId && b.Id == bookId).FirstOrDefault();
        }

        public IEnumerable<Book> GetBooksForAuthor(int authorId)
        {
            return _context.Books.Where(b => b.AuthorId == authorId).ToList();
        }

        public bool AuthorExists(int authorId)
        {
            return _context.Authors.Any(a => a.Id == authorId);
        }

        public void AddBookForAuthor(int authorId, Book book)
        {
            var author = GetAuthor(authorId, false);

            author.Books.Add(book);
        }

        public void UpdateBookForAuthor(int authorId, Book book)
        {
            //in this implementation HAPPENS to be empty because Entity Framework Core automatically tracks its entities and applies the changes
            //in another implementation of the Repository Pattern that doesn't have that type of automated change tracking, you do need to write code here to update your entity
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }
    }
}
