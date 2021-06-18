using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Authors;
using LibraryApp.Mobile.Services.BookService;
using LibraryApp.Mobile.Services.Categories;
using LibraryApp.Mobile.Services.Publishers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    [QueryProperty(nameof(BookId), nameof(BookId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public IBookService _bookService => DependencyService.Get<IBookService>();
        public IAuthorService _authorService => DependencyService.Get<IAuthorService>();
        public IPublisherService _publisherService => DependencyService.Get<IPublisherService>();
        public ICategoryService _categoryService => DependencyService.Get<ICategoryService>();
        private long bookId;
        private string title;
        private string description;
        private string authorName;
        private string category;
        private string publisher;
        private string isbn;
        private string date;
        public long Id { get; set; }

        public string Text
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public string ISBN
        {
            get => isbn;
            set => SetProperty(ref isbn, value);
        }
        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        public string AuthorName
        {
            get => authorName;
            set => SetProperty(ref authorName, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Publisher
        {
            get => publisher;
            set => SetProperty(ref publisher, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public long BookId
        {
            get
            {
                return bookId;
            }
            set
            {
                bookId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(long itemId)
        {
            try
            {
                
                var item = await _bookService.GetBookById(itemId);
                var author = await _authorService.GetAuthor(item.AuthorId);
                var pub = await _publisherService.GetPublisherById(item.PublisherId);
                var cate = await _categoryService.GetCategoryById(item.CategoryId);
                AuthorName = author.FirstName + " " + author.LastName;
                Id = item.BookId;
                Text = item.BookName;
                Description = item.Description;
                Publisher = pub.PublisherName;
                Category = cate.CategoryName;
                ISBN = item.ISBN;
                Date = item.ReleaseDate.ToString();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
