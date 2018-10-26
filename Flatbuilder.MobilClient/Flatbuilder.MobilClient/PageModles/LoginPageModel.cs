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

namespace Fb.MC.Views
{
    public class LoginPageModel : FreshBasePageModel
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
                    userName = value;
                    RaisePropertyChanged(userName);
                   // PropertyChanged(this, new PropertyChangedEventArgs("username")); 
            }
        }
        public LoginPageModel()
        {
            LoginCommand = new Command(
               () =>
               {
                   Application.Current.MainPage = FreshPageModelResolver.ResolvePageModel<MainPageModel>(UserName);
               });
        }
       
        public override void Init(object initData)
        {
            base.Init(initData);
           
        }
    }
}
