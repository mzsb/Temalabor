using Flatbuilder.DTO;
using FreshMvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fb.MC.Views
{
    class MainPageModel : FreshBasePageModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        static Uri baseAddress = new Uri("http://10.0.2.2:51502/");

        public string ordersListViewText;
        public string OrdersListViewText
        {
            get
            {
                return ordersListViewText;
            }
            private set
            {
                ordersListViewText = value;
                RaisePropertyChanged("ordersListViewText");
                //PropertyChanged(this, new PropertyChangedEventArgs("OrderListTV"));
            }
        }
        private string userName;
        public string UserName { get { return userName; } private set {
                userName = value;
                RaisePropertyChanged("UserName");
            } }
        private string orders;
        public string Orders
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

        private List<Order> ordersl;
        public List<Order> Ordersl
        {
            get
            {
                return ordersl;
            }
            set
            {
                ordersl = value;
                RaisePropertyChanged("Ordersl");
            }
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            UserName = initData.ToString();
            Orders = await ListOrdersByNameString(UserName);
            Ordersl = await ListOrdersByName(UserName);
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

        public static async Task<String> ListOrdersString()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                try
                {
                    var response = await client.GetAsync("api/Order/list");
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static async Task<String> ListOrdersByNameString(String name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                try
                {
                    var response = await client.GetAsync("api/Order/list/" + name);
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
