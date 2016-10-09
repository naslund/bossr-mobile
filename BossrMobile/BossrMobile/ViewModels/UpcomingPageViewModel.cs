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
    public class UpcomingPageViewModel : INotifyPropertyChanged
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

        private IEnumerable<CreatureItem> upcoming;
        public IEnumerable<CreatureItem> Upcoming
        {
            get { return upcoming; }
            set
            {
                upcoming = value;
                OnPropertyChanged(nameof(Upcoming));
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

        public async Task ReadUpcomingAsync()
        {
            IsLoading = true;

            await Task.Delay(200);

            try
            {
                if (SelectedWorld != null)
                {
                    IEnumerable<Creature> creatures = await App.RestService.GetCreaturesAsync();
                    IEnumerable<Spawn> spawns = await App.RestService.GetLatestWorldSpawnsAsync(SelectedWorld.Id);
                    IEnumerable<Category> categories = await App.RestService.GetCategoriesAsync();

                    List<CreatureItem> statuses = new List<CreatureItem>();
                    foreach (Spawn spawn in spawns.OrderByDescending(x => x.TimeMinUtc))
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

                        if (DateTime.UtcNow < spawn.TimeMinUtc)
                        {
                            statuses.Add(new CreatureItem
                            {
                                CreatureName = creature.Name,
                                Detail = $"in {(DateTime.UtcNow - spawn.TimeMinUtc).Humanize(2)}",
                                CategoryName = category?.Name,
                                CategoryColorRgb = Color.FromRgb(category.ColorR, category.ColorG, category.ColorB)
                            });
                        }
                    }
                    Upcoming = statuses.OrderBy(x => x.CategoryName).ThenBy(x => x.CreatureName);
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