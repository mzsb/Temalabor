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

namespace Fb.MC.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { private set; get; }
        string Username
        {
            get
            {
                return Username;
            }
            set
            {
                    Username = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(
                async () =>
                {
                   
                    await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(Username));
                });
        }
    }
}
