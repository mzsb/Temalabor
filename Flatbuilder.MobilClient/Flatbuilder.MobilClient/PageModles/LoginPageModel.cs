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
                    RaisePropertyChanged("userName");
                    ((Command)LoginCommand).ChangeCanExecute();
                // PropertyChanged(this, new PropertyChangedEventArgs("username")); 
            }
        }
        public LoginPageModel()
        {
            LoginCommand = new Command(
               execute: () =>
                {
                    var navpage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>(UserName));
                    Application.Current.MainPage = navpage;
                },
               canExecute:() => 
               {
                   if (UserName == "" || UserName == null)
                   {
                       return false;
                   }
                   else return true;
               });
        }
       
        public override void Init(object initData)
        {
            base.Init(initData);
           
        }
    }
}
