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
        private List<int>kpicker;
        public List<int> Kpicker
        {
            get
            {
                return kpicker;
            }
            set
            {
                kpicker = value;
                RaisePropertyChanged("Kpicker");
            }
        }
        private int selectedK;
        public int SelectedK
        {
            get
            {
                return selectedK;
            }
            set
            {
                for (int i = 0; i < selectedK; i++)
                {
                    Room room = rooms.Find(r => r.Type.ToString().Equals("Flatbuilder.DTO.Kitchen"));
                    rooms.Remove(room);
                    freeRooms.Add(room);
                }
                selectedK = value;
                for (int i = 0; i < selectedK; i++)
                {
                    Room room = freeRooms.Find(r => r.Type.ToString().Equals("Flatbuilder.DTO.Kitchen"));
                    rooms.Add(room);
                    freeRooms.Remove(room);
                }
                RaisePropertyChanged("SelectedK");
            }
        }


        public List<int> Spicker { get; set; }
        public List<int> Bpicker { get; set; }

        private DateTime startDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                PropertyChangedOwn();
                RaisePropertyChanged("StartDate");
            }
        }
        private DateTime endDate = DateTime.Now.AddDays(5);
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                PropertyChangedOwn();
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
        private List<Room> rooms = new List<Room>();

        public NewOrderPageModel()
        {
            CreateOrderCommand = new Command(
                execute: async () =>
                {
                    if (rooms==null || rooms.Count == 0)
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
                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            var navpage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>(/*todo costumer*/));
                            Application.Current.MainPage = navpage;
                        }
                    }
                }
                );
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            UserId = (int)initData;
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

        protected async void PropertyChangedOwn()
        {
            SelectedK = 0;
            freeRooms = await GetRooms(StartDate, EndDate);
            if(Kpicker==null)
                Kpicker = new List<int>();
            Kpicker.Clear();
            Kpicker.Add(0);
            if(freeRooms != null || freeRooms.Count != 0)
            {
                int k = 0;
                int s = 0;
                int b = 0;

                foreach (Room r in freeRooms)
                {
                    if (r.Type.ToString().Equals("Flatbuilder.DTO.Kitchen"))
                        Kpicker.Add(++k);
                }
                RaisePropertyChanged("Kpicker");
            }
            ReCountPrice();
        }
        public void ReCountPrice()
        {
            if (rooms == null || rooms.Count == 0)
            {
                Price = 0;
                return;
            }
            else
            {
                double pr = 0;
                double days = (EndDate - StartDate).TotalDays;

                foreach (Room r in rooms)
                {
                    pr += r.Price * days;

                }
                Price = pr;
            }
        }
    }
}
