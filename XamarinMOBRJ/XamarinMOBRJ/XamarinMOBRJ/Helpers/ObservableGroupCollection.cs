using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace XamarinMOBRJ.ViewModels
{
    public class ObservableCollection<S, T> : ObservableCollection<T>
    {
        private readonly S _key;
        public ObservableCollection(IGrouping<S, T> group)
            : base(group)
        {
            _key = group.Key;
        }
        public S Key
        {
            get { return _key; }
        }
    }
}
