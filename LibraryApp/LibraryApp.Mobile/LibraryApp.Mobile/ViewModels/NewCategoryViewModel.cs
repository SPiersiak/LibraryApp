using LibraryApp.Mobile.Models;
using LibraryApp.Mobile.Services.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LibraryApp.Mobile.ViewModels
{
    public class NewCategoryViewModel: BaseViewModel
    {
        public ICategoryService _categoryService => DependencyService.Get<ICategoryService>();
        public Command SaveCategoryCommand { get; }
        public Command CancelCommand { get; }
        private string categoryName;
        private string categoryDescritpion;
        public NewCategoryViewModel()
        {
            SaveCategoryCommand = new Command(OnSaveCategory, CategoryValidateSave);
            CancelCommand = new Command(Cancel);
            this.PropertyChanged +=
                (_, __) => SaveCategoryCommand.ChangeCanExecute();
        }
        public string CategoryName
        {
            get => categoryName;
            set => SetProperty(ref categoryName, value);
        }
        public string CategoryDescription
        {
            get => categoryDescritpion;
            set => SetProperty(ref categoryDescritpion, value);
        }
        private void ClearCategory()
        {
            CategoryName = "";
            CategoryDescription = "";
        }
        private bool CategoryValidateSave()
        {
            return !String.IsNullOrWhiteSpace(categoryName)
                && !String.IsNullOrWhiteSpace(categoryDescritpion);
        }
        private async void OnSaveCategory()
        {
            Category category = new Category()
            {
                CategoryName = categoryName,
                CategoryDescription = categoryDescritpion
            };

            var result = await _categoryService.AddNewCategory(category);
            await Application.Current.MainPage.DisplayAlert("Save", "New Category was added", "OK");
            ClearCategory();
            await Task.Delay(400);
            // This will pop the current page off the navigation stack
            //await Shell.Current.GoToAsync("..");
        }
        private async void Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
