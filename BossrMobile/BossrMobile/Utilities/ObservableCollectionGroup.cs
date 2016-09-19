using System.Collections.ObjectModel;
using System.Linq;

namespace BossrMobile.Utilities
{
    public class ObservableGroupCollection<TKey, T> : ObservableCollection<T>
    {
        public TKey Key { get; }

        public ObservableGroupCollection(IGrouping<TKey, T> group)
            : base(group)
        {
            Key = group.Key;
        }
    }
}
