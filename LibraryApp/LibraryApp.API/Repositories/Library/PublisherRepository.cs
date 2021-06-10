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
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public PublisherRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<bool> AddPublisher(PublisherDto item)
        {
            var findPublisherDuplicate = await _libraryDbContext.Publishers.Where(x => x.PublisherName.Equals(item.PublisherName)).FirstOrDefaultAsync();

            if (findPublisherDuplicate != null)
            {
                return false;
                throw new Exception("Istnieje juz taki wydawca");
            }

            var publisher = new Publisher()
            {
                PublisherName = item.PublisherName,
                PublisherDescription = item.PublisherDescription
            };

            await _libraryDbContext.Publishers.AddAsync(publisher);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }
    }
}
