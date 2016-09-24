using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using Xamarin.Forms;

namespace BossrMobile.Converters
{
    public class DateTimeOffsetToHoursAgoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset)
                return $"Updated {(DateTime.UtcNow - ((DateTimeOffset) value).UtcDateTime).Humanize(2)} ago";

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
