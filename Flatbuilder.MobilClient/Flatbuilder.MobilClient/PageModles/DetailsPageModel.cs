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
        static readonly Uri baseAddress = new Uri("http://10.0.2.2:51502/");

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
        ICommand DeleteOrderCommand { get; }

        public override void Init(object initData)
        {
            base.Init(initData);
            Order = (Order)initData;
            foreach(Room r in Order.Rooms)
            {
                r.Type = r.GetType();
            }
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
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                var response = await client.GetAsync("api/Order/delete/" + id);
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    await CoreMethods.PopToRoot(false);
                }
            }
        }
    }
}
