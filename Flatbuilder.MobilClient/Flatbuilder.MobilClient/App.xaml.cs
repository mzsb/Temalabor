using Fb.MC.ViewModels;
using Fb.MC.Views;
using FreshMvvm;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fb.MC
{
    public partial class App : Application
    {
        public App()
        {

            MainPage = FreshPageModelResolver.ResolvePageModel<LoginPageViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
