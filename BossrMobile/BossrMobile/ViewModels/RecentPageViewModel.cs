using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Controls;
using BossrMobile.Models;
using BossrMobile.Models.ViewItems;
using Humanizer;
using Xamarin.Forms;

namespace BossrMobile.ViewModels
{
    public class RecentPageViewModel : INotifyPropertyChanged
    {
        private AlertPreset alertPreset;
        public AlertPreset AlertPreset
        {
            get { return alertPreset; }
            set
            {
                alertPreset = value;
                OnPropertyChanged(nameof(AlertPreset));
            }
        }
        
        private IEnumerable<CreatureItem> recent;
        public IEnumerable<CreatureItem> Recent
        {
            get { return recent; }
            set
            {
                recent = value;
                OnPropertyChanged(nameof(Recent));
            }
        }

        private World selectedWorld;
        public World SelectedWorld
        {
            get { return selectedWorld; }
            set
            {
                selectedWorld = value;
                OnPropertyChanged(nameof(SelectedWorld));
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public async Task ReadRecentAsync()
        {
            IsLoading = true;

            await Task.Delay(200);

            try
            {
                if (SelectedWorld != null)
                {
                    var creatures = await App.RestService.GetCreaturesAsync();
                    var categories = await App.RestService.GetCategoriesAsync();
                    var recentspawns = await App.RestService.GetRecentWorldSpawnsAsync(SelectedWorld.Id);

                    List<CreatureItem> statuses = new List<CreatureItem>();
                    foreach (Spawn spawn in recentspawns.OrderByDescending(x => x.TimeMaxUtc))
                    {
                        Creature creature = creatures.Single(x => x.Id == spawn.CreatureId);
                        Category category = null;
                        if (creature.CategoryId.HasValue)
                            category = categories.SingleOrDefault(x => x.Id == creature.CategoryId);

                        Color categoryColor = Color.Transparent;
                        if (category != null)
                            categoryColor = Color.FromRgb(category.ColorR, category.ColorG, category.ColorB);

                        statuses.Add(new CreatureItem
                        {
                            CreatureName = creatures.Single(x => x.Id == spawn.CreatureId).Name,
                            CategoryName = category?.Name,
                            CategoryColorRgb = categoryColor,
                            Detail = $"{(DateTime.UtcNow - spawn.TimeMinUtc).Humanize()} ago"
                        });
                    }
                    Recent = statuses.OrderBy(x => x.CategoryName).ThenBy(x => x.CreatureName);
                    AlertPreset = AlertPreset.None;
                }
            }
            catch (Exception)
            {
                AlertPreset = AlertPreset.NoConnection;
            }
            
            IsLoading = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}