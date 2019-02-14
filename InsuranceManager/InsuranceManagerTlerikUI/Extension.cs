using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerTlerikUI
{
    public static class Extension
    {
        public static ObservableCollection<T> GetObservable<T, U>(this IEnumerable<U> items, Func<U, T> map) where T : class
        {
            return items.Aggregate(new ObservableCollection<T>(), (acc, i) =>
            {
                acc.Add(map(i));
                return acc;
            });
        }
    }
}
