using SF.PictApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SF.PictApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureListPage : ContentPage
    {
        public static string FolderPath { get; set; }
        public ObservableCollection<Picture> Pictures { get; set; }

        Picture SelectedPicture;

        public PictureListPage(string path)
        {
            InitializeComponent();

            FolderPath = path;
            titleLabel.Text = path;
        }

       
        protected override void OnAppearing()
        {
            // Создаем коллекцию картинок
            var pictures = Directory.GetFiles(FolderPath)
                                .Select(f => 
                                    new Picture { 
                                        Name = Path.GetFileName(f), 
                                        Description = File.GetLastWriteTime(f).ToString(),
                                        Image = ImageSource.FromStream(() => new MemoryStream(GetImageBytes(f)))
                                    })
                                .ToList();

            Pictures = new ObservableCollection<Picture>(pictures);
            pictureList.ItemsSource = Pictures;
            pictureList.SelectedItem = null;        // снимаем выделение

            base.OnAppearing();
        }

        private byte[] GetImageBytes(string path)
        {
            if (!File.Exists(path))
                return null;

            return File.ReadAllBytes(path);
            //byte[] fileBytes = await ImageExtensions.Resize(path, 1600);        // Уменьшаем размер фото чтобы не нагружать сеть
        }

        private void pictureList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedPicture = (Picture)e.SelectedItem;
        }

        private async void OpenPicture_Clicked(object sender, EventArgs e)
        {
            if (SelectedPicture == null)
            {
                await DisplayAlert(null, $"Пожалуйста, выберите картинку!", "OK");
                return;
            }

            await Navigation.PushAsync(new PictureViewPage(SelectedPicture));
        }
        private void RemovePicture_Clicked(object sender, EventArgs e)
        {
            File.Delete(Path.Combine(FolderPath, SelectedPicture.Name));
            Pictures.Remove(SelectedPicture);
        }

    }
}