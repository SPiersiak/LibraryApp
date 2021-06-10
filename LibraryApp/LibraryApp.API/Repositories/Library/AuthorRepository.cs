using LibraryApp.API.Data;
using LibraryApp.API.Models;
using LibraryApp.API.Repositories.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public AuthorRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<bool> AddAuthor(AuthorDto item)
        {
            var findAuthorDuplicate = await _libraryDbContext.Authors.Where(x => x.FirstName.Equals(item.FirstName) && x.LastName.Equals(item.LastName)).FirstOrDefaultAsync();
            if (findAuthorDuplicate != null)
            {
                return false;
                throw new Exception("Istnieje juz taki autor");
            }
            var author = new Author()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                BirthDate = item.BirthDate,
                Country = item.Country
            };

            await _libraryDbContext.Authors.AddAsync(author);
            await _libraryDbContext.SaveChangesAsync();
            return true;

        }
    }
}
