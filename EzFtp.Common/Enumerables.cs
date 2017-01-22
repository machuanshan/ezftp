using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EzFtp.Common
{
  public static class Enumerables
  {
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
    {
      if(source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      ObservableCollection<T> coll = new ObservableCollection<T>();

      foreach (var item in source)
      {
        coll.Add(item);
      }

      return coll;
    }

    public static void AddRange<T>(this ObservableCollection<T> coll, IEnumerable<T> newItems)
    {
      if (coll == null)
      {
        throw new ArgumentNullException(nameof(coll));
      }
      if (newItems == null)
      {
        throw new ArgumentNullException(nameof(newItems));
      }

      foreach (var item in newItems)
      {
        coll.Add(item);
      }
    }
	}
}
