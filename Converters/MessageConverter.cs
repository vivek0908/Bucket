using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace WPF_Chat_ver1.Converters
{
    internal class MessageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var tuple = new Tuple<string, ObservableCollection<string>>(
            (string)values[0], (ObservableCollection<string>)values[1]);
            return (object)tuple;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
