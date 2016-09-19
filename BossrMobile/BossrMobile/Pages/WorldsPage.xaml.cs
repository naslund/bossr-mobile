using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BossrMobile.Models;
using BossrMobile.ViewModels;
using Xamarin.Forms;

namespace BossrMobile.Pages
{
    public partial class WorldsPage : ContentPage
    {
        private WorldsPageViewModel WorldPageViewModel => (WorldsPageViewModel)BindingContext;

        public WorldsPage()
        {
            InitializeComponent();
            BindingContext = new WorldsPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await WorldPageViewModel.ReadWorlds();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await WorldPageViewModel.ReadWorlds();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new StatusPage((World)e.Item));
        }
    }
}
