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
                RaisePropertyChanged(ordersListViewText);
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

        public override async void Init(object initData)
        {
            base.Init(initData);
            UserName = initData.ToString();
            Orders = await ListOrdersString();
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

        async void listOrdersButton_Click(object sender, System.EventArgs e)
        {
            ordersListViewText = await ListOrdersString();
        }
    }
}
