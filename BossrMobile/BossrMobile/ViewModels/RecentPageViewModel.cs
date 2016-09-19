using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Models;

namespace BossrMobile.ViewModels
{
    public class RecentPageViewModel : INotifyPropertyChanged
    {
        private World selectedWorld;
        private List<Spawn> recent;
        private bool isLoading;

        public List<Spawn> Recent
        {
            get { return recent; }
            set
            {
                recent = value;
                OnPropertyChanged(nameof(Recent));
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

        public async Task ReadRecent()
        {
            IsLoading = true;
            Recent = null;
            if (SelectedWorld != null)
                //Recent = await App.RestService.ReadRecentAsync(SelectedWorld.Id);
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