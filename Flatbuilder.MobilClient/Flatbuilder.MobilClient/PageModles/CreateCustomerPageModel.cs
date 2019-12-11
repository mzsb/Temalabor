using AspNetCore.Http.Extensions;
using Fb.MC.Certificate;
using Flatbuilder.DTO;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fb.MC.Views
{
    class CreateCustomerPageModel : FreshBasePageModel
    {
        static readonly Uri baseAddress = new Uri("https://10.0.2.2:5001/");

        public ICommand CreateCommand { private set; get; }
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
                ((Command)CreateCommand).ChangeCanExecute();
                // PropertyChanged(this, new PropertyChangedEventArgs("username")); 
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("password");
                ((Command)CreateCommand).ChangeCanExecute();
                // PropertyChanged(this, new PropertyChangedEventArgs("username")); 
            }
        }
        private string repeatPassword;
        public string RepeatPassword
        {
            get
            {
                return repeatPassword;
            }
            set
            {
                repeatPassword = value;
                RaisePropertyChanged("repeatPassword");
                ((Command)CreateCommand).ChangeCanExecute();
                // PropertyChanged(this, new PropertyChangedEventArgs("username")); 
            }
        }
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged("email");
                ((Command)CreateCommand).ChangeCanExecute();
                // PropertyChanged(this, new PropertyChangedEventArgs("username")); 
            }
        }
        public CreateCustomerPageModel()
        {
            LoginCommand = new Command(
                execute: async () =>
                {
                    var navpage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<LoginPageModel>());
                    Application.Current.MainPage = navpage;
                });
            CreateCommand = new Command(

                execute: async () =>
                {
                    Costumer costumer;
                    HttpClient httpClient;
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            httpClient = new HttpClient(DependencyService.Get<IHTTPClientHandlerCreationService>().GetInsecureHandler());
                            break;
                        default:
                            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                            httpClient = new HttpClient(new HttpClientHandler());
                            break;
                    }
                    using (httpClient)
                    {
                        httpClient.BaseAddress = baseAddress;

                        costumer = new Costumer() { Name = UserName, Password = Password, Email = Email };
                        var res = await httpClient.PostAsJsonAsync<Costumer>("api/Customer/create", costumer);
                        if (res.StatusCode != System.Net.HttpStatusCode.Created)
                        {
                            return;
                        }
                    }
                    var navpage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>(costumer));
                    Application.Current.MainPage = navpage;
                },
                canExecute: () =>
                {
                    if (UserName == "" ||
                        UserName == null ||
                        Password == "" ||
                        Password == null ||
                        Email == "" ||
                        Email == null ||
                        Email.Contains("@") ||
                        RepeatPassword == "" ||
                        RepeatPassword == null ||
                        Password != RepeatPassword)
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
