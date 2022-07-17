using SF.PictApp.Models;
using System;
using System.Collections.Generic;
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
        public List<Picture> Pictures { get; set; }

        public PictureListPage(string path)
        {
            InitializeComponent();

            FolderPath = path;
            titleLabel.Text = path;
        }

       
        protected override void OnAppearing()
        {
            //DisplayAlert(null, Directory.GetFiles(folderPath).Length.ToString(), "OK");
            Pictures = Directory.GetFiles(FolderPath)
                                .Select(f => 
                                    new Picture { 
                                        Name = Path.GetFileName(f), 
                                        Description = File.GetLastWriteTime(f).ToString(),
                                        Image = ImageSource.FromStream(() => new MemoryStream(GetImageBytes(f)))
                                    })
                                .ToList();
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

        private async void OpenPicture_Clicked(object sender, EventArgs e)
        {

        }
        private async void RemovePicture_Clicked(object sender, EventArgs e)
        {

        }

    }
}