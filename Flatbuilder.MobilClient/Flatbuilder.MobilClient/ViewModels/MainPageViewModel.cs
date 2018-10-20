using Flatbuilder.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flatbuilder.MobilClient.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static Uri baseAddress = new Uri("http://10.0.2.2:51502/");

        public string ordersListViewText;
        public string OrdersListViewText
        {
            get { return ordersListViewText; }
            private set { ordersListViewText = value; PropertyChanged(this, new PropertyChangedEventArgs("OrderListTV")); }
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
