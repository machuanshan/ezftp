using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzFtp.Common
{
  public static class Locator
  {
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T instance)
    {
      if(instance == null)
      {
        throw new ArgumentNullException(nameof(instance));
      }

      _services[typeof(T)] = instance;
    }

    public static void Register<T>()
    {
      _services[typeof(T)] = null;
    }

    public static T Resolve<T>()
    {
      if (!_services.ContainsKey(typeof(T)))
      {
        throw new InvalidOperationException("No instance registered for type T");
      }

      var svc = (T)_services[typeof(T)];
      if (svc == null)
      {
        svc = Activator.CreateInstance<T>();
        _services[typeof(T)] = svc;
      }
      return svc;
    }
  }
}
