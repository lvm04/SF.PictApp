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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            welcomeText.Text = "Установите PIN-код";

            // Получим значения ползунков из Preferences.
            // Если значений нет - установим значения по умолчанию (false)
            //gasSwitch.On = Preferences.Get("gasState", false);
            //climateSwitch.On = Preferences.Get("climateState", false);
            //electroSwitch.On = Preferences.Get("electroState", false);

            base.OnAppearing();
        }


        private async void Login_Click(object sender, EventArgs e)
        {
            if (pinEntry.Text == "1234")
            {
                await Navigation.PushAsync(new PictureListPage());
            }
            else
            {
                infoMessage.Text = "Неверный PIN-код";
            }
        }
    }
}