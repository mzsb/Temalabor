using Fb.MC.Views;
using Flatbuilder.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Flatbuilder.MobilClient.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { private set; get; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(
                async () =>
                {
                   
                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());
                });
        }
    }
}
