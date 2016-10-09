﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Controls;
using BossrMobile.Models;
using BossrMobile.Models.ViewItems;
using BossrMobile.Utilities;
using Humanizer;
using Humanizer.Localisation;
using Xamarin.Forms;

namespace BossrMobile.ViewModels
{
    public class CreaturesPageViewModel : INotifyPropertyChanged
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

        private IEnumerable<CreatureItem> creatureItems;
        public IEnumerable<CreatureItem> CreatureItems
        {
            get { return creatureItems; }
            set
            {
                creatureItems = value;
                OnPropertyChanged(nameof(CreatureItems));
            }
        }

        public async Task ReadCreaturesAsync()
        {
            IsLoading = true;

            await Task.Delay(200);

            try
            {
                var creatures = await App.RestService.GetCreaturesAsync();
                var categories = await App.RestService.GetCategoriesAsync();

                var items = new List<CreatureItem>();

                foreach (Creature creature in creatures)
                {
                    Category category = categories.SingleOrDefault(x => creature.CategoryId != null && x.Id == creature.CategoryId.Value);

                    items.Add(new CreatureItem
                    {
                        CreatureName = creature.Name,
                        Detail = $"{TimeSpan.FromHours(creature.HoursBetweenEachSpawnMin).Humanize(maxUnit: TimeUnit.Day)} - {TimeSpan.FromHours(creature.HoursBetweenEachSpawnMax).Humanize(maxUnit: TimeUnit.Day)}",
                        CategoryName = category == null ? "Uncategorized" : category.Name,
                        CategoryColorRgb = category == null ? Color.Gray : Color.FromRgb(category.ColorR, category.ColorG, category.ColorB)
                    });
                }

                CreatureItems = items.OrderBy(x => x.CategoryName).ThenBy(x => x.CreatureName);
                AlertPreset = AlertPreset.None;
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
