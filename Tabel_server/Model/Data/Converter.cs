using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Tabel_server.Model.Data
{

   public class ConverterDay : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("dd.MM.yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class ConverterTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("t");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
       
        
    }
    public class ConverterColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string specCheck = (string)value;
            if (specCheck == "ком.")
            { return "#FF80E2F1"; }
            else if (specCheck=="")
            { return "White"; }
            else if (specCheck == "больн.")
            { return "#FFFFBDBD"; }
            else if (specCheck == "отп.б.с.")
            { return "#FFFB8585"; }
            else if (specCheck == "отг.")
            { return "#FFFFFFAB"; }
            else if (specCheck == "отп.")
            { return "#FFFFFF7D"; }
            else return "White";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterTimeSpan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return  (( (TimeSpan)value).TotalHours);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    }
