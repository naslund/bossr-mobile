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
    public partial class SpawnablePage : ContentPage
    {
        private SpawnablePageViewModel SpawnablePageViewModel => (SpawnablePageViewModel)BindingContext;

        public SpawnablePage(World selectedWorld)
        {
            InitializeComponent();
            BindingContext = new SpawnablePageViewModel();
            SpawnablePageViewModel.SelectedWorld = selectedWorld;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SpawnablePageViewModel.ReadSpawnable();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await SpawnablePageViewModel.ReadSpawnable();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
