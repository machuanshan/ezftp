using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EzFtp
{
  public class BoolConverter : IValueConverter
  {
    public bool IsInversed { get; set; } = false;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null) return false;
      bool bv = value is bool? ? ((bool?)value).Value : (bool)value;
      return IsInversed ? !bv : bv;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
