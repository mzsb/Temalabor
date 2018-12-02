using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AspNetCore.Http.Extensions;
using Flatbuilder.DTO;
using FreshMvvm;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Fb.MC.Views
{
    class NewOrderPageModel : FreshBasePageModel
    {
        public int UserId{ get; set; }
        static readonly Uri baseAddress = new Uri("http://10.0.2.2:51502/");

        public ICommand CreateOrderCommand { get; }

        public List<int> Kpicker { get; set; }
        

        public List<int> Spicker { get; set; }
        public List<int> Bpicker { get; set; }

        private DateTime startDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged();
                RaisePropertyChanged("StartDate");
            }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged();
                RaisePropertyChanged("EndDate");
            }
        }
        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                RaisePropertyChanged("Price");
            }
        }
        private List<Room> freeRooms;
        private List<Room> rooms;

        public NewOrderPageModel()
        {
            CreateOrderCommand = new Command(
                execute: async () =>
                {
                    if(rooms==null || rooms.Count == 0)
                    {
                        return;
                    }
                    var order = new Order()
                    {
                        CostumerId = UserId,
                        StartDate = this.StartDate,
                        EndDate = this.EndDate,
                        Rooms = rooms
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = baseAddress;

                        var orderJson = JsonConvert.SerializeObject(order);
                        StringContent cont = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

                        var response = await client.PostAsJsonAsync<Order>("api/Order/create",order);
                    }
                }
                );
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            UserId = (int)initData;
            StartDate = DateTime.Now;
            EndDate = StartDate.AddDays(5);
            freeRooms = await GetRooms(StartDate, EndDate);
        }

        private async Task<List<Room>> GetRooms(DateTime start, DateTime end)
        {
            var sd = start.ToString("MM-dd-yyyy");
            var ed = end.ToString("MM-dd-yyyy");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = baseAddress;

                var response = await client.GetAsync("api/Order/" + sd + "/" + ed);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Room>>(json);
            }
        }

        protected async void OnPropertyChanged()
        {
            freeRooms = await GetRooms(StartDate, EndDate);
            if (rooms==null || rooms.Count==0){
                Price = 0;
                return;
            }
           double pr=0;
           double days = (EndDate - StartDate).TotalDays;

            foreach (Room r in rooms)
            {
                pr += r.Price * days;
            }
            Price = pr;
        }
    }
}
