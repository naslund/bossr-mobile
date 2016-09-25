using BossrMobile.Services;
using Xamarin.Forms;

namespace BossrMobile
{
    public partial class App : Application
    {
        public static RestService RestService { get; private set; }

        public App()
        {
            InitializeComponent();

            RestService = new RestService();
            MainPage = new Navigation.MainPage();
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
