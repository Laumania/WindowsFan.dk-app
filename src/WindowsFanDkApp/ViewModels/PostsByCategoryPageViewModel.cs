using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AgFx;
using GalaSoft.MvvmLight;
using WindowsFanDkApp.Api.Models;

namespace WindowsFanDkApp.ViewModels
{
    public class PostsByCategoryPageViewModel : ViewModelBase
    {
        public PostsByCategoryPageViewModel()
        {
        }


        public void Setup(Category category)
        {
            this.Category = category;
            this.CategoryPosts = DataManager.Current.Load<CategoryPosts>(category.Id);
        }

        public Category Category { get; set; }
        public CategoryPosts CategoryPosts { get; set; }
    }
}
