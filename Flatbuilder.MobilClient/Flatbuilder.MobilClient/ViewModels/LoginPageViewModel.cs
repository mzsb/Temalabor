using Fb.MC.Views;
using Flatbuilder.DTO;
using FreshMvvm;
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
    public class LoginPageViewModel : FreshBasePageModel, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { private set; get; }
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {

                if (userName != value)
                {
                    userName = value;
                    RaisePropertyChanged(userName);
                   // PropertyChanged(this, new PropertyChangedEventArgs("username")); 
                }
            }
        }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(
                async () =>
                {
                   // await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(UserName));
                });
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
