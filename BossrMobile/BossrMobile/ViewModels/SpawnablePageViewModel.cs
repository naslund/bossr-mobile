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
    public class SpawnablePageViewModel : INotifyPropertyChanged
    {
        private World selectedWorld;
        private List<SpawnableStatus> spawnable;
        private bool isLoading;

        public List<SpawnableStatus> Spawnable
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

        public async Task ReadSpawnable()
        {
            IsLoading = true;
            Spawnable = null;
            if (SelectedWorld != null)
            {
                IEnumerable<Creature> creatures = await App.RestService.GetCreaturesAsync();
                IEnumerable<Spawn> spawns = await App.RestService.GetLatestWorldSpawnsAsync(SelectedWorld.Id);

                List<SpawnableStatus> statuses = new List<SpawnableStatus>();
                foreach (Spawn spawn in spawns)
                {
                    Creature creature = creatures.Single(x => x.Id == spawn.CreatureId);

                    int missedSpawns = 0;
                    while (DateTime.UtcNow > spawn.TimeMaxUtc)
                    {
                        missedSpawns++;
                        spawn.TimeMinUtc = spawn.TimeMinUtc.AddHours(missedSpawns*creature.HoursBetweenEachSpawnMin);
                        spawn.TimeMaxUtc = spawn.TimeMaxUtc.AddHours(missedSpawns*creature.HoursBetweenEachSpawnMax);
                    }

                    if (DateTime.UtcNow > spawn.TimeMinUtc && DateTime.UtcNow < spawn.TimeMaxUtc) // If spawnable
                    {
                        statuses.Add(new SpawnableStatus
                        {
                            CreatureName = creature.Name,
                            TimeLeft = $"{(int)(spawn.TimeMaxUtc - DateTime.UtcNow).TotalHours} hours left"
                        });
                    }
                }
                Spawnable = statuses.OrderBy(x => x.CreatureName).ToList();
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