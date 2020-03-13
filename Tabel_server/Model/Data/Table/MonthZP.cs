using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table
{
    public class MonthZP : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MonthZP(MonthEmployee MonthEmployeeZP)
        {
            this.MonthEmployeeZP = MonthEmployeeZP;
            MonthBonus = 35;
            Editing = false;
            if (MonthEmployeeZP.DaysNotWorkedVacation > 0 || MonthEmployeeZP.DaysNotWorkedMatherhoodVacation > 0)
            { BonusOverWorkingWhenVocationChk = true; }
            else
            {
                BonusOverWorkingChk = true;
            }
            CalculateBonus();
            Fix = false;
        }
        public MonthZP(int MonthBonus, double BiznessTripBonus, double OverWorkingBonus,
            double OverWorkingBonusIfVocation, int FreeBonus, int ZP,  int ZPwithout13)
        {
            this.MonthBonus = MonthBonus;
            this.BiznessTripBonus = BiznessTripBonus;
            this.OverWorkingBonus = OverWorkingBonus;
            this.OverWorkingBonusIfVocation = OverWorkingBonusIfVocation;
            this.FreeBonus = FreeBonus;
            this.ZP = ZP;
            this.ZPwithout13 = ZPwithout13;
            Fix = true;

        }
        MonthEmployee monthEmployee;
        public MonthEmployee MonthEmployeeZP
        {
            get { return monthEmployee; }
            set
            {
                monthEmployee = value;
            }
        }
        int monthBonus;
        public int MonthBonus
        {
            get { return monthBonus; }
            set { monthBonus = value; 
                CalculateZP();
            }
        }
        public double biznessTripBonus;
        public double BiznessTripBonus { get { return biznessTripBonus; } set { biznessTripBonus = Math.Round(value); CalculateZP(); } }
        double overWorkingBonus { get; set; }
        public double OverWorkingBonus
        {
            get { return overWorkingBonus; }
            set
            {
                overWorkingBonus = Math.Round(value);
                CalculateZP();
            }
        }
        double overWorkingBonusIfVocation { get; set; }
        public double OverWorkingBonusIfVocation
        {
            get { return overWorkingBonusIfVocation; }
            set
            {
                overWorkingBonusIfVocation = Math.Round(value); CalculateZP();
            }
        }
        int freeBonus;
        public int FreeBonus
        {
            get { return freeBonus; }
            set
            {
                freeBonus = value;
                OnPropertyChanged("FreeBonus");
                CalculateZP();
            }
        }
        int zp;
        public int ZP
        {

            get { return zp; }
            set
            {
                zp = value;
                OnPropertyChanged("ZP");
            }
        }
     
        int zPwithout13;
        public int ZPwithout13
        {
            get { return zPwithout13; }
            set
            {
                zPwithout13 = value;
                OnPropertyChanged("ZPwithout13");
            }
        }
        bool editing;
        public bool Editing
        {
            get
            {
                return editing;
            }
            set
            {
                if (value == true)
                { editing = false; }
                else { editing = true; }
                OnPropertyChanged("Editing");
            }
        }
        bool bonusOverWorkingWhenVocationChk;
        public bool BonusOverWorkingWhenVocationChk
        {
            get
            {
                return bonusOverWorkingWhenVocationChk;
            }
            set
            {
                bonusOverWorkingWhenVocationChk = value;
                CalculateZP();
            }
        }
        bool bonusOverWorkingChk;
        public bool BonusOverWorkingChk
        {
            get
            {
                return bonusOverWorkingChk;
            }
            set
            {
                bonusOverWorkingChk = value;
                CalculateZP();
            }
        }
        bool fix;
        public bool Fix
        {
            get
            {
                return fix;
            }
            set
            {
                if (value==true)
                { fix = false; }
                else { fix = true; }
            }
        }

        public void CalculateZP()
        {
            try
            {
                double hourCost = (MonthEmployeeZP.Employee.Salary / MonthEmployeeZP.WorkedHoursInMonthPlaned.TotalHours);
            double MonthCostBase = hourCost * MonthEmployeeZP.Time1X.TotalHours;
            double MonthCostWhithOverworking = MonthCostBase + hourCost * MonthEmployeeZP.Time15X.TotalHours * 1.5
                + hourCost * MonthEmployeeZP.Time20X.TotalHours * 2 + hourCost * MonthEmployeeZP.TimeHoli.TotalHours * 2;
           

                if (BonusOverWorkingWhenVocationChk == true)
                {
                    ZP = Convert.ToInt32(MonthEmployeeZP.Employee.Salary + (MonthEmployeeZP.Employee.Salary / 100) * MonthBonus
                + (MonthEmployeeZP.Employee.Salary / 100) * BiznessTripBonus + (MonthEmployeeZP.Employee.Salary / 100) * OverWorkingBonusIfVocation
                    + (MonthEmployeeZP.Employee.Salary / 100) * FreeBonus);
                    ZPwithout13 = ZP - (ZP / 100) * 13;
                }
                else
                {
                    ZP = Convert.ToInt32(MonthEmployeeZP.Employee.Salary + (MonthEmployeeZP.Employee.Salary / 100) * MonthBonus
               + (MonthEmployeeZP.Employee.Salary / 100) * BiznessTripBonus + (MonthEmployeeZP.Employee.Salary / 100) * OverWorkingBonus
                   + (MonthEmployeeZP.Employee.Salary / 100) * FreeBonus);
                    ZPwithout13 = ZP - (ZP / 100) * 13;
                }
            }
            catch (Exception ex) {
                Loger.SetLog(ex.Message.ToString());
            }
        }
        public void CalculateBonus()
        {
            //Бонус за переработки
            double bonus15 = (100 * MonthEmployeeZP.Time15X.TotalHours / MonthEmployeeZP.WorkedHoursInMonthPlaned.TotalHours) * 1.5;
            double bonus20 = 100 * MonthEmployeeZP.Time20X.TotalHours / MonthEmployeeZP.WorkedHoursInMonthPlaned.TotalHours * 2;
            OverWorkingBonus = bonus15 + bonus20;
            double OverWorkingBonusIfVocation15 = bonus15 * 22 / (MonthEmployeeZP.DaysWorked + MonthEmployeeZP.DaysWorkedBusinessTrip);
            double OverWorkingBonusIfVocation20 = bonus20 * 22 / (MonthEmployeeZP.DaysWorked + MonthEmployeeZP.DaysWorkedBusinessTrip);
            OverWorkingBonusIfVocation = OverWorkingBonusIfVocation15 + OverWorkingBonusIfVocation20;
            if (bonus15 == overWorkingBonus && bonus20 == 0)
            { OverWorkingBonusIfVocation = 0; }
            if (MonthEmployeeZP.DaysWorkedBusinessTrip > 0 && MonthEmployeeZP.DaysWorkedBusinessTrip < 7)
            { BiznessTripBonus = 0.35 * MonthEmployeeZP.DaysWorkedBusinessTrip / MonthEmployeeZP.WorkedDayInMonthPlaned * 100; }
            else
            {
                BiznessTripBonus = 0.35 * MonthEmployeeZP.DaysWorkedBusinessTrip / (MonthEmployeeZP.WorkedDayInMonthPlaned * (MonthEmployeeZP.DaysWorkedBusinessTrip - 7)
                 / MonthEmployeeZP.WorkedDayInMonthPlaned + 1);
            }
        }
    }
}
