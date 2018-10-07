using Flatbuilder.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Flatbuilder.MobilClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        static Uri baseAddress = new Uri("http://10.0.2.2:51502/");

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
                catch (Exception e)
                {
                    
                    throw;
                }
            }
        }

        async void listOrdersButton_Click(object sender, System.EventArgs e)
        {
            ordersListView.Text = await ListOrdersString();
        }
    }
}
