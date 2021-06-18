using LibraryApp.API.Data;
using LibraryApp.API.Models;
using LibraryApp.API.Repositories.Library.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BookRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<bool> AddBook(BookDto item)
        {
            var findBookDuplicate = await _libraryDbContext.Books.Where(x => x.ISBN.Equals(item.ISBN)).FirstOrDefaultAsync();
            if(findBookDuplicate != null)
            {
                return false;
                throw new Exception("Istnieje juz taka książka, dodaj kopie");
            }
            var book = new Book()
            {
                BookName = item.BookName,
                ReleaseDate = item.ReleaseDate,
                Description = item.Description,
                BookLanguage = item.BookLanguage,
                Image = item.Image,
                ISBN = item.ISBN,
                AuthorId = item.AuthorId,
                PublisherId = item.PublisherId,
                CategoryId = item.CategoryId
            };

            await _libraryDbContext.Books.AddAsync(book);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Book> FindBook(string searched)
        {
            IEnumerable<Book> books = _libraryDbContext.Books.Where(x => x.BookName.Contains(searched)).ToList();

            return books;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            IEnumerable<Book> books = _libraryDbContext.Books.ToList();
            return books;
        }

        public Book GetBookById(long bokkId)
        {
            var result = _libraryDbContext.Books.Single(x => x.BookId == bokkId);
            return result;
        }
    }
}
