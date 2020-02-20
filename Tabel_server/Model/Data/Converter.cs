using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
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
            { return "LightBlue"; }
            else if (specCheck == DayTypeOnPlan.Worked)
            { return "White"; }
            else if (specCheck == DayTypeOnPlan.WorkedShort)
            { return "LightYellow"; }
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
            { return "#FF80E2F1"; }
            else if (specCheck== DayTypeOnFact.Worked)
            { return "White"; }
            else if (specCheck == DayTypeOnFact.NotWorkedSick)
            { return "#FFFFBDBD"; }
            else if (specCheck == DayTypeOnFact.NotWorkedMatherhoodVacation)
            { return "#FFFB8585"; }
            else if (specCheck == DayTypeOnFact.NotWorkedVacation)
            { return "#FFFFFF7D"; }
            else if (specCheck == DayTypeOnFact.NotWorkedAdministrative)
            { return "#FFFFFF7D"; }
            else if (specCheck == DayTypeOnFact.NotWorked)
            { return "#FFFFFF7D"; }
            else return "Blue";
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
            { return "Выходной"; }
            else if (specCheck == DayTypeOnFact.Worked)
            { return "Рабочий"; }
            else if (specCheck == DayTypeOnFact.WorkedBusinessTrip)
            { return "Командировка"; }
            else if (specCheck == DayTypeOnFact.NotWorkedSick)
            { return "Больничный"; }
            else if (specCheck == DayTypeOnFact.NotWorkedMatherhoodVacation)
            { return "Больничный по уходу за ребенком"; }
            else if (specCheck == DayTypeOnFact.NotWorkedVacation)
            { return "Отпуск"; }
            else if (specCheck == DayTypeOnFact.NotWorkedAdministrative)
            { return "Административный"; }
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
    }
