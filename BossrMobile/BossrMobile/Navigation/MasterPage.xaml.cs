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
                    IconSource = "globe24.png",
                    TargetType = typeof(WorldsPage)
                },
                new MasterPageItem
                {
                    Title = "Creatures",
                    IconSource = "tentacles24.png",
                    TargetType = typeof(CreaturesPage)
                }
            };

            listView.ItemsSource = masterPageItems;
        }
    }
}
