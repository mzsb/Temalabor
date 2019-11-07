using AspNetCore.Http.Extensions;
using Fb.MC.Certificate;
using Fb.MC.Views;
using Flatbuilder.DTO;
using FreshMvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fb.MC.Views
{
    public class LoginPageModel : FreshBasePageModel
    {
        static readonly Uri baseAddress = new Uri("https://10.0.2.2:5001/");

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


                        var response = await httpClient.GetAsync("api/Customer/get/" + UserName);
                        if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            costumer = new Costumer() { Name = UserName };
                            var res = await httpClient.PostAsJsonAsync<Costumer>("api/Customer/create", costumer);
                            if(res.StatusCode != System.Net.HttpStatusCode.Created)
                            {
                                return;
                            }
                        }
                        else
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            costumer = JsonConvert.DeserializeObject<Costumer>(json);
                        }
                    }
                    var navpage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>(costumer));
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
