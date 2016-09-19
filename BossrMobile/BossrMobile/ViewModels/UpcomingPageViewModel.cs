using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Models;

namespace BossrMobile.ViewModels
{
    public class UpcomingPageViewModel : INotifyPropertyChanged
    {
        private World selectedWorld;
        //private List<Status> upcoming;
        private bool isLoading;

        /*public List<Status> Upcoming
        {
            get { return upcoming; }
            set
            {
                upcoming = value;
                OnPropertyChanged(nameof(Upcoming));
            }
        }*/

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task ReadUpcoming()
        {
            IsLoading = true;
            //Upcoming = null;
            if (SelectedWorld != null)
                //Upcoming = await App.RestService.ReadUpcomingAsync(SelectedWorld.Id);
            IsLoading = false;
        }
    }
}