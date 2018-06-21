using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace Products.Views
{
    public class ImgFileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty((string)value)? string.Empty : $@"{Environment.CurrentDirectory}\img\{(string)value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}