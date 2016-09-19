using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BossrMobile.Annotations;
using BossrMobile.Models;
using BossrMobile.Utilities;

namespace BossrMobile.ViewModels
{
    public class CreaturesPageViewModel : INotifyPropertyChanged
    {
        private bool isLoading;
        private IEnumerable<Creature> creatures;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        
        public IEnumerable<Creature> Creatures
        {
            get { return creatures; }
            set
            {
                creatures = value;
                OnPropertyChanged(nameof(Creatures));
            }
        }
        
        public async Task ReadCreatures()
        {
            IsLoading = true;
            Creatures = await App.RestService.ReadCreaturesAsync();
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
