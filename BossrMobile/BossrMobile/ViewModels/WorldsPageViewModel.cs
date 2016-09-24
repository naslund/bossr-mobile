using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Models;

namespace BossrMobile.ViewModels
{
    public class WorldsPageViewModel : INotifyPropertyChanged
    {
        private bool isLoading;
        private IEnumerable<World> worlds;
        private IEnumerable<World> filteredWorlds;
        private string searchCriteria;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public IEnumerable<World> Worlds
        {
            get { return worlds; }
            set
            {
                worlds = value;
                OnPropertyChanged(nameof(Worlds));
            }
        }

        public IEnumerable<World> FilteredWorlds
        {
            get { return filteredWorlds; }
            set
            {
                filteredWorlds = value;
                OnPropertyChanged(nameof(FilteredWorlds));
            }
        }

        public string SearchCriteria
        {
            get { return searchCriteria; }
            set
            {
                searchCriteria = value;
                OnPropertyChanged(nameof(SearchCriteria));
            }
        }

        public async Task ReadWorldsAsync()
        {
            IsLoading = true;
            Worlds = await App.RestService.GetWorldsAsync();
            IsLoading = false;
        }

        public void FilterWorlds()
        {
            FilteredWorlds = string.IsNullOrEmpty(SearchCriteria) ? Worlds : worlds.Where(x => x.Name.ToLower().Contains(SearchCriteria.ToLower()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
