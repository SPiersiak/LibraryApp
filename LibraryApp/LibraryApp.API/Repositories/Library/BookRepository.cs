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
    /// <summary>
    /// Repozytorium dla książek, odpowiada za operacje crud na bazie danych 
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _libraryDbContext;
        /// <summary>
        /// konstruktor inicjalizujący odwołanie do dbContextu bazy danych
        /// </summary>
        /// <param name="libraryDbContext">jako parametr przyjmuje odwołanie do dbContextu bazy danych dzieki czemu klasa wie do jakich tabel sie odwołuje,
        /// wie jakie sa pola w tabelach i encje pomiedzy tabelami, Wykorzystywany jest wzorzec Dependecy Injection
        /// </param>
        public BookRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        /// <summary>
        /// metoda dodajaca nowa ksiazkę do bazy danych
        /// </summary>
        /// <param name="item">jako parametr przyjmuje obiekt ksiązki który odwzorowuje tabele w bazie danych</param>
        /// <returns>jezeli ksiazka została dodana zostanie zwrócone true, jezeli nie zostanie zwrócone false</returns>
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
        /// <summary>
        /// metoda wyszukująca książki z bazy danych na podstawie podanego parametru
        /// </summary>
        /// <param name="searched">zmienna w postaci ciągu znaków zawierających szukana nazwe</param>
        /// <returns>zwracana jest książka lub lista książek jeżeli podana nazwa zawiera sie w nazwie książki</returns>
        public IEnumerable<Book> FindBook(string searched)
        {
            IEnumerable<Book> books = _libraryDbContext.Books.Where(x => x.BookName.Contains(searched)).ToList();

            return books;
        }

        /// <summary>
        /// metoda wybierająca wszystkie ksiązki z bazy danych
        /// </summary>
        /// <returns>zwraca liste ksiązek zapisanych w bazie danych</returns>
        public IEnumerable<Book> GetAllBooks()
        {
            IEnumerable<Book> books = _libraryDbContext.Books.ToList();
            return books;
        }
        /// <summary>
        /// metoda wybierajaca ksiazke po numerze id
        /// </summary>
        /// <param name="bokkId">numer Id szukanej ksiazki</param>
        /// <returns>zwraca ksiazke w postaci modelu odwzorowanego na podstawie bazy danych</returns>
        public Book GetBookById(long bokkId)
        {
            var result = _libraryDbContext.Books.Single(x => x.BookId == bokkId);
            return result;
        }
    }
}
