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
        public Year(DateTime Year, List<(DateTime, DayType)> SpecialDays = null)
        {
            InitializeComponent();

            year = Year;

            Month month;
            for (int i = 1; i < 13; i++)
            {
                month = new Month(new DateTime(year.Year, i, 1), SpecialDays) { Margin = new Thickness(8) };
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
                    if(t == DayType.FullDay) SelectedDate.Text = $"{d.ToLongDateString()} - Полный рабочий день.";
                    if (t == DayType.FreeDay) SelectedDate.Text = $"{d.ToLongDateString()} - Выходной день.";
                    if (t == DayType.ShortDay) SelectedDate.Text = $"{d.ToLongDateString()} - Сокращённый на 1 час рабочий день.";
                    if (t == DayType.VeryShortDay) SelectedDate.Text = $"{d.ToLongDateString()} - Сокращённый на 2 часа рабочий день.";
                };
                Main.Children.Add(month);
                Grid.SetColumn(month, (i - 1) % 6);
                Grid.SetRow(month, (i - 1) / 6);
            }

        }
    }
}