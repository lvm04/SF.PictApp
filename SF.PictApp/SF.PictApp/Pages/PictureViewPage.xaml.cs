using SF.PictApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SF.PictApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureViewPage : ContentPage
    {
        public static Picture picture;
        public static string title;
        public PictureViewPage(Picture pict)
        {
            InitializeComponent();

            picture = pict;
        }     

        protected override void OnAppearing()
        {
            titleLabel.Text = picture.Name;
            image.Source = picture.Image;
            desrLabel.Text = $"Дата создания: {picture.Description}";

            base.OnAppearing();
        }
    
    }
}