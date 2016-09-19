using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Models;

namespace BossrMobile.ViewModels
{
    public class SpawnablePageViewModel : INotifyPropertyChanged
    {
        private World selectedWorld;
        //private List<Status> spawnable;
        private bool isLoading;

        /*public List<Status> Spawnable
        {
            get { return spawnable; }
            set
            {
                spawnable = value;
                OnPropertyChanged(nameof(Spawnable));
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

        public async Task ReadSpawnable()
        {
            IsLoading = true;
            //Spawnable = null;
            if (SelectedWorld != null)
                //Spawnable = await App.RestService.ReadSpawnableAsync(SelectedWorld.Id);
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
