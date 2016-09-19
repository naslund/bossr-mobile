using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BossrMobile.Pages;
using Xamarin.Forms;

namespace BossrMobile.Navigation
{
    
    public partial class MasterPage : ContentPage
    {
        public ListView ListView => listView;

        public MasterPage()
        {
            InitializeComponent();

            var masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Status",
                    TargetType = typeof(WorldsPage)
                },
                new MasterPageItem
                {
                    Title = "Creatures",
                    TargetType = typeof(CreaturesPage)
                }
            };

            listView.ItemsSource = masterPageItems;
        }
    }
}
