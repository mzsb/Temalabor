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
    class MainPageModel : FreshBasePageModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        static readonly Uri baseAddress = new Uri("http://10.0.2.2:51502/");

        public Order Selected { get; set; }

        private string userName;
        public string UserName { get { return userName; } private set {
                userName = value;
                RaisePropertyChanged("UserName");
            } }

        private List<Order> orders;
        public List<Order> Orders
        {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                RaisePropertyChanged("Orders");
            }
        }

        public ICommand CreateOrderCommand { get; }
        public ICommand DetailsCommand { get; }

        public MainPageModel()
        {
            CreateOrderCommand = new Command(
            execute: async () =>
            {
                await CoreMethods.PushPageModel<NewOrderPageModel>();
            }
            );
            DetailsCommand = new Command(
            execute: async (object param) =>
            {
                try
                {
                    if (Selected == null)
                        return;
                    await CoreMethods.PushPageModel<DetailsPageModel>(Selected);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            );
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            UserName = initData.ToString();
            Orders = await ListOrdersByName(UserName);
        }

        public static async Task<List<Order>> ListOrders()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                var response = await client.GetAsync("api/Order/list");
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Order>>(json);
            }
        }


        public static async Task<List<Order>> ListOrdersByName(String name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

           
                var response = await client.GetAsync("api/Order/list/" + name);
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Order>>(json);
            }
        }

        //public static async Task<String> ListOrdersString()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;

        //        try
        //        {
        //            var response = await client.GetAsync("api/Order/list");
        //            return await response.Content.ReadAsStringAsync();
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //    }
        //}

        //public static async Task<String> ListOrdersByNameString(String name)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = baseAddress;

        //        try
        //        {
        //            var response = await client.GetAsync("api/Order/list/" + name);
        //            return await response.Content.ReadAsStringAsync();
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //}
    }
}
