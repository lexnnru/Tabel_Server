using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Calendar
{
    public partial class Month : UserControl
    {
        List<Day> days;
        public Month(DateTime Month, List<(DateTime, DayType)> SpecialDays = null)
        {
            InitializeComponent();
            this.Date = Month;
            days = new List<Day>();

            InitName();
            InitDays();
            if (SpecialDays != null) InitSpecial(SpecialDays);
            Fill();
        }

        public event Action<Month, DateTime, DayType> Checked;
        public Day lastChecked { get; set; }
        public DateTime Date { get; set; }

        void InitName()
        {
            if (Date.Month == 1) MonthName.Text = $"{Date.Year} г.  Январь";
            if (Date.Month == 2) MonthName.Text = $"{Date.Year} г.  Февраль";
            if (Date.Month == 3) MonthName.Text = $"{Date.Year} г.  Март";
            if (Date.Month == 4) MonthName.Text = $"{Date.Year} г.  Апрель";
            if (Date.Month == 5) MonthName.Text = $"{Date.Year} г.  Май";
            if (Date.Month == 6) MonthName.Text = $"{Date.Year} г.  Июнь";
            if (Date.Month == 7) MonthName.Text = $"{Date.Year} г.  Июль";
            if (Date.Month == 8) MonthName.Text = $"{Date.Year} г.  Август";
            if (Date.Month == 9) MonthName.Text = $"{Date.Year} г.  Сентябрь";
            if (Date.Month == 10) MonthName.Text = $"{Date.Year} г.  Октябрь";
            if (Date.Month == 11) MonthName.Text = $"{Date.Year} г.  Ноябрь";
            if (Date.Month == 12) MonthName.Text = $"{Date.Year} г.  Декабрь";
        }

        void InitDays()
        {
            Day day;
            DateTime today;
            for (int i = 0; i < DateTime.DaysInMonth(Date.Year, Date.Month); i++)
            {
                day = new Day(i + 1);
                today = new DateTime(Date.Year, Date.Month, i + 1);
                if (today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                    day.Type = DayType.FreeDay;
                if (today.DayOfWeek == DayOfWeek.Friday)
                    day.Type = DayType.ShortDay;
                day.Checked += (s) =>
                {
                    if (lastChecked == null)
                    {
                        lastChecked = s;
                        Checked?.Invoke(this, new DateTime(Date.Year, Date.Month, s.Number), s.Type);
                    }
                    else
                    {
                        if (lastChecked.Number != s.Number)
                        {
                            lastChecked.IsChecked = false;
                            lastChecked = s;
                            Checked?.Invoke(this, new DateTime(Date.Year, Date.Month, s.Number), s.Type);
                        }
                    }
                };
                days.Add(day);
            }
        }

        void InitSpecial(List<(DateTime, DayType)> SpecialDays)
        {
            SpecialDays.ForEach(d =>
            {
                if (d.Item1.Month == Date.Month)
                {
                    days.Find(i => { return d.Item1.Day == i.Number; }).Type = d.Item2;
                }
            });
        }

        void Fill()
        {
            int Dow = ((int)Date.DayOfWeek == 0) ? 5 : (int)Date.DayOfWeek - 2;
            days.ForEach(d =>
            {
                DaysGrid.Children.Add(d);
                Grid.SetColumn(d, (d.Number + Dow) % 7);
                Grid.SetRow(d, (d.Number + Dow) / 7);
            });
        }
    }
}