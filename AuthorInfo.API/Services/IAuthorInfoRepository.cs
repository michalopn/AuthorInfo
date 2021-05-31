using AuthorInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorInfo.API.Services
{
    public interface IAuthorInfoRepository
    {
        IEnumerable<Author> GetAuthors();

        Author GetAuthor(int authorId, bool includeBooks);

        IEnumerable<Book> GetBooksForAuthor(int authorId);

        Book GetBookForAuthor(int authorId, int bookId);

        bool AuthorExists(int authorId);

        void AddBookForAuthor(int authorId, Book book);

        void UpdateBookForAuthor(int authorId, Book book);

        void DeleteBook(Book book);

        bool Save();
    }
}
