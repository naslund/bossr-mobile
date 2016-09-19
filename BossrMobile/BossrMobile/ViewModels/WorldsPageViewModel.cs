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

        public async Task ReadWorlds()
        {
            IsLoading = true;
            Worlds = await App.RestService.ReadWorldsAsync();
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
