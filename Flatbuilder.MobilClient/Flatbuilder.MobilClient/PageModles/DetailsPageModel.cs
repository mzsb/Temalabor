using Fb.MC.Certificate;
using Flatbuilder.DTO;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fb.MC.Views
{
    class DetailsPageModel : FreshBasePageModel
    {
        static readonly Uri baseAddress = new Uri("https://10.0.2.2:5001/");

        private Order order;
        public Order Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                RaisePropertyChanged("Order");
            }
        }
        public ICommand DeleteOrderCommand { get; }

        public override void Init(object initData)
        {
            base.Init(initData);
            Order = (Order)initData;
        }
        public DetailsPageModel()
        {
            DeleteOrderCommand = new Command(
                async () => 
                {
                    await DeleteOrder(Order.Id);
                });
        }

        private async Task DeleteOrder(int id)
        {
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
                var response = await httpClient.DeleteAsync("api/Order/delete/" + id);
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var navpage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>(Order.Costumer));
                    Application.Current.MainPage = navpage;
                }
            }
        }
    }
}
