using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Tabel_server.Model.Data.Table;
using Tabel_server.Model.Data.Table.EmployeeDay;

namespace Tabel_server.Model.Data
{
    public class ConverterDayTypePlan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DayTypeOnPlan specCheck = (DayTypeOnPlan)value;
            if (specCheck == DayTypeOnPlan.Holiday)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorHoliDay.Split(':');

                    return Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2])).ToString();
                }
                catch { return "White"; }


            }
            else if (specCheck == DayTypeOnPlan.Worked)
            { return "White"; }
            else if (specCheck == DayTypeOnPlan.WorkedShort)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorWorkedShortDay.Split(':');

                    return Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2])).ToString();
                }
                catch { return "White"; }
            }
            else return "White";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
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
            DayTypeOnFact specCheck = (DayTypeOnFact)value;
            if (specCheck == DayTypeOnFact.WorkedBusinessTrip)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorBiznesTrip.Split(':');
                    Color color = Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2]));
                    return  color.ToString();
                    
                }
                catch { return "White"; }
            }
            else if (specCheck== DayTypeOnFact.Worked)
            {
                return "White";
            }
            else if (specCheck == DayTypeOnFact.NotWorkedSick)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorSick.Split(':');
                    return Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2])).ToString();
                }
                catch { return "White"; }
            }
            else if (specCheck == DayTypeOnFact.NotWorkedMatherhoodVacation)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorVocation.Split(':');

                return Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2])).ToString();
                }
                catch { return "White"; }
            }
            else if (specCheck == DayTypeOnFact.NotWorkedVacation)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorVocation.Split(':');

                    return Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2])).ToString();
                }
                catch { return "White"; }
            }
            else if (specCheck == DayTypeOnFact.NotWorkedAdministrative)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorAdministrativ.Split(':');

                    return Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2])).ToString();
                }
                catch { return "White"; }
            }
            else if (specCheck == DayTypeOnFact.NotWorked)
            {
                try
                {
                    string[] ColorHoli = Properties.Settings.Default.ColorHoliDay.Split(':');
                    Color color = Color.FromRgb(System.Convert.ToByte(ColorHoli[0]), System.Convert.ToByte(ColorHoli[1]), System.Convert.ToByte(ColorHoli[2]));
                    return color.ToString();
                }
                catch { return "White"; }
            }
            else return "White";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterDayType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DayTypeOnFact specCheck = (DayTypeOnFact)value;
            if (specCheck == DayTypeOnFact.NotWorked)
            { return ""; }
            else if (specCheck == DayTypeOnFact.Worked)
            { return ""; }
            else if (specCheck == DayTypeOnFact.WorkedBusinessTrip)
            { return "Ком."; }
            else if (specCheck == DayTypeOnFact.NotWorkedSick)
            { return "Больн."; }
            else if (specCheck == DayTypeOnFact.NotWorkedMatherhoodVacation)
            { return "Больн."; }
            else if (specCheck == DayTypeOnFact.NotWorkedVacation)
            { return "Отп."; }
            else if (specCheck == DayTypeOnFact.NotWorkedAdministrative)
            { return "Адм."; }
            else if (specCheck == DayTypeOnFact.NoData)
            { return "-"; }
            else return "-";
            //return "-";
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
            return string.Format("{0:f1}", ((TimeSpan)value).TotalHours);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class ConverterMoney : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = (Int32)value;
            return i.ToString("N0", CultureInfo.InvariantCulture).Replace(',', ' ');

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
