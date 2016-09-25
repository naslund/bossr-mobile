using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Models;
using BossrMobile.Models.ViewItems;
using Humanizer;
using Xamarin.Forms;

namespace BossrMobile.ViewModels
{
    public class SpawnablePageViewModel : INotifyPropertyChanged
    {
        private World selectedWorld;
        private IEnumerable<CreatureItem> spawnable;
        private bool isLoading;

        public IEnumerable<CreatureItem> Spawnable
        {
            get { return spawnable; }
            set
            {
                spawnable = value;
                OnPropertyChanged(nameof(Spawnable));
            }
        }

        public World SelectedWorld
        {
            get { return selectedWorld; }
            set
            {
                selectedWorld = value;
                OnPropertyChanged(nameof(SelectedWorld));
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public async Task ReadSpawnableAsync()
        {
            IsLoading = true;
            Spawnable = null;
            if (SelectedWorld != null)
            {
                IEnumerable<Creature> creatures = await App.RestService.GetCreaturesAsync();
                IEnumerable<Spawn> spawns = await App.RestService.GetLatestWorldSpawnsAsync(SelectedWorld.Id);
                IEnumerable<Category> categories = await App.RestService.GetCategoriesAsync();

                List<CreatureItem> statuses = new List<CreatureItem>();
                foreach (Spawn spawn in spawns)
                {
                    Creature creature = creatures.Single(x => x.Id == spawn.CreatureId);
                    
                    Category category = null;
                    if (creature.CategoryId.HasValue)
                        category = categories.SingleOrDefault(x => x.Id == creature.CategoryId);
                    
                    int missedSpawns = 0;
                    while (DateTime.UtcNow > spawn.TimeMaxUtc)
                    {
                        missedSpawns++;
                        spawn.TimeMinUtc = spawn.TimeMinUtc.AddHours(creature.HoursBetweenEachSpawnMin);
                        spawn.TimeMaxUtc = spawn.TimeMaxUtc.AddHours(creature.HoursBetweenEachSpawnMax);
                    }

                    if (DateTime.UtcNow > spawn.TimeMinUtc && DateTime.UtcNow < spawn.TimeMaxUtc)
                    {
                        statuses.Add(new CreatureItem
                        {
                            // Missed: {missedSpawns - 1}
                            CreatureName = creature.Name,
                            Detail = $"{(spawn.TimeMaxUtc - DateTime.UtcNow).Humanize(2)} left", 
                            CategoryName = category?.Name,
                            CategoryColorRgb = Color.FromRgb(category.ColorR, category.ColorG, category.ColorB)
                        });
                    }
                }
                Spawnable = statuses.OrderBy(x => x.CategoryName).ThenBy(x => x.CreatureName);
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