using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Calendar
{
    public partial class Day : UserControl
    {
        bool isChecked;
        DayType type;
        public TimeSpan DayLength { get; set; }
        public Day(int Number)
        {
            InitializeComponent();

            this.Number = Number;
            Text.Text = Number.ToString();
        }

        public int Number { get; private set; }
        public event Action<Day> Checked;
        public DayType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                switch(value)
                {
                    case DayType.FreeDay:
                        Bord.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 100, 100));
                        Bord.Background = new SolidColorBrush(Color.FromRgb(255, 119, 119));
                        break;
                    case DayType.FullDay:
                        Bord.BorderBrush = new SolidColorBrush(Color.FromRgb(76, 165, 255));
                        Bord.Background = new SolidColorBrush(Color.FromRgb(119, 187, 255));
                        break;
                    case DayType.ShortDay:
                        Bord.BorderBrush = new SolidColorBrush(Color.FromRgb(76, 180, 170));
                        Bord.Background = new SolidColorBrush(Color.FromRgb(119, 200, 170));
                        break;
                }
            }
        }

        public bool IsChecked
        {
            set
            {
                isChecked = value;
                if (value)
                    CheckAnim();
                else
                    Bord_MouseLeave(this, null);
            }
            get
            {
                return isChecked;
            }
        }

        private void Bord_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsChecked)
            {
                ColorAnimation BackgroundAnim = new ColorAnimation();
                ColorAnimation BorderBrushAnim = new ColorAnimation();
                BackgroundAnim.Duration = TimeSpan.FromMilliseconds(100);
                BorderBrushAnim.Duration = TimeSpan.FromMilliseconds(100);
                if (Type == DayType.FreeDay)
                {
                    BackgroundAnim.To = Color.FromRgb(255, 158, 158);
                    BorderBrushAnim.To = Color.FromRgb(255, 142, 142);
                }
                else if (type == DayType.FullDay)
                {
                    BackgroundAnim.To = Color.FromRgb(178, 216, 255);
                    BorderBrushAnim.To = Color.FromRgb(162, 200, 255);
                }
                else if (type == DayType.ShortDay)
                {
                    BackgroundAnim.To = Color.FromRgb(140, 220, 190);
                    BorderBrushAnim.To = Color.FromRgb(96, 200, 190);
                }
                PropertyPath BackgroundTargetPath = new PropertyPath("(Border.Background).(SolidColorBrush.Color)");
                PropertyPath BorderBrushTargetPath = new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)");
                Storyboard CellBackgroundChangeStory = new Storyboard();
                Storyboard.SetTarget(BackgroundAnim, Bord);
                Storyboard.SetTarget(BorderBrushAnim, Bord);
                Storyboard.SetTargetProperty(BackgroundAnim, BackgroundTargetPath);
                Storyboard.SetTargetProperty(BorderBrushAnim, BorderBrushTargetPath);
                CellBackgroundChangeStory.Children.Add(BackgroundAnim);
                CellBackgroundChangeStory.Children.Add(BorderBrushAnim);
                CellBackgroundChangeStory.Begin();
            }
        }

        private void Bord_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsChecked)
            {
                ColorAnimation BackgroundAnim = new ColorAnimation();
                ColorAnimation BorderBrushAnim = new ColorAnimation();
                BackgroundAnim.Duration = TimeSpan.FromMilliseconds(200);
                BorderBrushAnim.Duration = TimeSpan.FromMilliseconds(200);
                if (Type == DayType.FreeDay)
                {
                    BackgroundAnim.To = Color.FromRgb(255, 119, 119);
                    BorderBrushAnim.To = Color.FromRgb(255, 100, 100);
                }
                else if (Type == DayType.FullDay)
                {
                    BackgroundAnim.To = Color.FromRgb(119, 187, 255);
                    BorderBrushAnim.To = Color.FromRgb(76, 165, 255);
                }
                else if (type == DayType.ShortDay)
                {
                    BackgroundAnim.To = Color.FromRgb(119, 200, 170);
                    BorderBrushAnim.To = Color.FromRgb(76, 180, 170);
                }
                PropertyPath BackgroundTargetPath = new PropertyPath("(Border.Background).(SolidColorBrush.Color)");
                PropertyPath BorderBrushTargetPath = new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)");
                Storyboard CellBackgroundChangeStory = new Storyboard();
                Storyboard.SetTarget(BackgroundAnim, Bord);
                Storyboard.SetTarget(BorderBrushAnim, Bord);
                Storyboard.SetTargetProperty(BackgroundAnim, BackgroundTargetPath);
                Storyboard.SetTargetProperty(BorderBrushAnim, BorderBrushTargetPath);
                CellBackgroundChangeStory.Children.Add(BackgroundAnim);
                CellBackgroundChangeStory.Children.Add(BorderBrushAnim);
                CellBackgroundChangeStory.Begin();
            }
        }

        private void Bord_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsChecked = true;
            Checked?.Invoke(this);
        }

        private void CheckAnim()
        {
            ColorAnimation BackgroundAnim = new ColorAnimation();
            ColorAnimation BorderBrushAnim = new ColorAnimation();
            BackgroundAnim.Duration = TimeSpan.FromMilliseconds(200);
            BorderBrushAnim.Duration = TimeSpan.FromMilliseconds(200);
            if (Type == DayType.FreeDay)
            {
                BackgroundAnim.To = Color.FromRgb(255, 240, 120);
                BorderBrushAnim.To = Color.FromRgb(255, 230, 100);
            }
            else if (Type == DayType.FullDay)
            {
                BackgroundAnim.To = Color.FromRgb(255, 240, 120);
                BorderBrushAnim.To = Color.FromRgb(255, 230, 100);
            }
            else if (Type == DayType.ShortDay)
            {
                BackgroundAnim.To = Color.FromRgb(255, 240, 120);
                BorderBrushAnim.To = Color.FromRgb(255, 230, 100);
            }
            PropertyPath BackgroundTargetPath = new PropertyPath("(Border.Background).(SolidColorBrush.Color)");
            PropertyPath BorderBrushTargetPath = new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)");
            Storyboard CellBackgroundChangeStory = new Storyboard();
            Storyboard.SetTarget(BackgroundAnim, Bord);
            Storyboard.SetTarget(BorderBrushAnim, Bord);
            Storyboard.SetTargetProperty(BackgroundAnim, BackgroundTargetPath);
            Storyboard.SetTargetProperty(BorderBrushAnim, BorderBrushTargetPath);
            CellBackgroundChangeStory.Children.Add(BackgroundAnim);
            CellBackgroundChangeStory.Children.Add(BorderBrushAnim);
            CellBackgroundChangeStory.Begin();
        }
    }
}