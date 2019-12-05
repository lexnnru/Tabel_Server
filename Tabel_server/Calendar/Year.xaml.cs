using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Calendar
{
    public partial class Year : UserControl
    {
        DateTime year;
        Month lastChecked;
        public DateTime SelectedDay { get; set; }
        public DayType DayTypeSelectedDay { get; set; }
        public event Action<DateTime, DayType> Set_DayType;
        public Year(DateTime Year, List<(DateTime, DayType)> SpecialDays = null)
        {
            InitializeComponent();

            year = Year;
            Month month;
            for (int i = 1; i < 13; i++)
            {
                month = new Month(new DateTime(year.Year, i, 1), SpecialDays) { Margin = new Thickness(1) };
                month.Checked += (s, d, t) =>
                {
                    if (lastChecked == null)
                    {
                        lastChecked = s;
                    }
                    else
                    {
                        if (lastChecked.Date != s.Date)
                        {
                            lastChecked.lastChecked.IsChecked = false;
                            lastChecked.lastChecked = null;
                            lastChecked = s;
                        }
                    }
                    if (t == DayType.FullDay)
                    { SelectedDate.Text = $"{d.ToLongDateString()} - Полный рабочий день.";
                        SelectedDay = d;
                        DayTypeSelectedDay = t;

                    }
                    if (t == DayType.FreeDay)
                    { SelectedDate.Text = $"{d.ToLongDateString()} - Выходной день.";
                        SelectedDay = d;
                        DayTypeSelectedDay = t;
                    }
                    if (t == DayType.ShortDay)
                    {
                        SelectedDate.Text = $"{d.ToLongDateString()} - Сокращённый на 1 час рабочий день.";
                        SelectedDay = d;
                        DayTypeSelectedDay = t;
                    }
                    if (t == DayType.VeryShortDay)
                    { SelectedDate.Text = $"{d.ToLongDateString()} - Сокращённый на 2 часа рабочий день.";
                        SelectedDay = d;
                        DayTypeSelectedDay = t;
                    }
                    if (t == DayType.Castom)
                    {
                        SelectedDate.Text = $"{d.ToLongDateString()} - Сокращённый на {Tabel_server.Properties.Settings.Default.Castom} часа рабочий день.";
                    }
                    
                };
                Main.Children.Add(month);
                Grid.SetColumn(month, (i - 1) % 6);
                Grid.SetRow(month, (i - 1) / 6);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bt_SetDayType.SelectedIndex == 0)
            { DayTypeSelectedDay = DayType.FreeDay; }
            else if (bt_SetDayType.SelectedIndex == 1)
            { DayTypeSelectedDay = DayType.FullDay; }
            else if (bt_SetDayType.SelectedIndex == 2)
            { DayTypeSelectedDay = DayType.ShortDay; }
            else if (bt_SetDayType.SelectedIndex == 3)
            { DayTypeSelectedDay = DayType.VeryShortDay; }
            else if (bt_SetDayType.SelectedIndex == 4)
            { DayTypeSelectedDay = DayType.Castom; }
            Set_DayType?.Invoke(SelectedDay, DayTypeSelectedDay);
        }
    }
}