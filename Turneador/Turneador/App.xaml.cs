using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turneador
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static string Route;
        public App(string route)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            Route = route;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
