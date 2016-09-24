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
    public partial class RecentPage : ContentPage
    {
        private RecentPageViewModel RecentPageViewModel => (RecentPageViewModel)BindingContext;

        public RecentPage(World selectedWorld)
        {
            InitializeComponent();

            Icon = Device.OnPlatform("today.png", "", ""); // Todo: -> XAML

            BindingContext = new RecentPageViewModel();
            RecentPageViewModel.SelectedWorld = selectedWorld;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await RecentPageViewModel.ReadRecentAsync();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await RecentPageViewModel.ReadRecentAsync();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
