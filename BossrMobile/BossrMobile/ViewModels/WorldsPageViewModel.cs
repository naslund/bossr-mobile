using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Controls;
using BossrMobile.Models;

namespace BossrMobile.ViewModels
{
    public class WorldsPageViewModel : INotifyPropertyChanged
    {
        public WorldsPageViewModel()
        {
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Factory.StartNew(SetRandomPlaceholderAsync);
                return true;
            });
        }
        
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
        
        private IEnumerable<World> worlds;
        public IEnumerable<World> Worlds
        {
            get { return worlds; }
            set
            {
                worlds = value;
                OnPropertyChanged(nameof(Worlds));
            }
        }

        private IEnumerable<World> filteredWorlds;
        public IEnumerable<World> FilteredWorlds
        {
            get { return filteredWorlds; }
            set
            {
                filteredWorlds = value;
                OnPropertyChanged(nameof(FilteredWorlds));
            }
        }

        private string searchCriteria;
        public string SearchCriteria
        {
            get { return searchCriteria; }
            set
            {
                searchCriteria = value;
                OnPropertyChanged(nameof(SearchCriteria));
            }
        }

        private string searchPlaceholder;
        public string SearchPlaceholder
        {
            get { return searchPlaceholder; }
            set
            {
                searchPlaceholder = value;
                OnPropertyChanged(nameof(SearchPlaceholder));
            }
        }

        public async Task ReadWorldsAsync()
        {
            IsLoading = true;

            await Task.Delay(200);

            try
            {
                Worlds = await App.RestService.GetWorldsAsync();
                AlertPreset = AlertPreset.None;
            }
            catch (Exception)
            {
                AlertPreset = AlertPreset.NoConnection;
            }
            
            IsLoading = false;

            await FilterWorlds();
        }

        public async Task SetRandomPlaceholderAsync()
        {
            string name = Worlds?.OrderBy(x => Guid.NewGuid()).FirstOrDefault()?.Name;
            if (name == null)
                return;

            SearchPlaceholder = "";
            foreach (char c in name)
            {
                SearchPlaceholder += c;
                await Task.Delay(100);
            }
        }

        public async Task FilterWorlds()
        {
            await Task.Run(() =>
            {
                FilteredWorlds = string.IsNullOrEmpty(SearchCriteria) || Worlds == null ? Worlds : Worlds.Where(x => x.Name.ToLower().Contains(SearchCriteria.ToLower()));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
