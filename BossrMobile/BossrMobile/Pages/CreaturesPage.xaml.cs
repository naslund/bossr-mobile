using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BossrMobile.ViewModels;
using Xamarin.Forms;

namespace BossrMobile.Pages
{
    public partial class CreaturesPage : ContentPage
    {
        private CreaturesPageViewModel CreaturesPageViewModel => (CreaturesPageViewModel)BindingContext;

        public CreaturesPage()
        {
            InitializeComponent();
            BindingContext = new CreaturesPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await CreaturesPageViewModel.ReadCreatures();
        }

        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            await CreaturesPageViewModel.ReadCreatures();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
