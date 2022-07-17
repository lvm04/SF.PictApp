using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SF.PictApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private string savedPin;
        private string folderPath = @"/storage/emulated/0/DCIM/Camera";
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            savedPin = Preferences.Get("PIN", "");

            if (String.IsNullOrEmpty(savedPin))
                welcomeText.Text = "Установите PIN-код";
            else
                welcomeText.Text = "Введите PIN-код";

            base.OnAppearing();
        }


        private async void Login_Click(object sender, EventArgs e)
        {
            string enteredPin = pinEntry.Text?.Trim();
            if (String.IsNullOrEmpty(enteredPin) || enteredPin.Length < 4)
            {
                infoMessage.Text = "PIN-код должен быть не менее 4-х символов";
                return;
            }

            if (String.IsNullOrEmpty(savedPin))
            {
                Preferences.Set("PIN", enteredPin);
                await Navigation.PushAsync(new PictureListPage(folderPath));
            }    
            else
            {
                if (savedPin == enteredPin)
                {
                    await Navigation.PushAsync(new PictureListPage(folderPath));
                }
                else
                {
                    infoMessage.Text = "Неверный PIN-код";
                    return;
                }
            }
        }
    }
}