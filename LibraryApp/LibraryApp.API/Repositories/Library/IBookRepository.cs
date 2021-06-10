﻿using LibraryApp.API.Repositories.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.API.Repositories.Library
{
    public interface IBookRepository
    {
        Task<bool> AddBook(BookDto item);
    }
}
