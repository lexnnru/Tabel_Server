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

        public MonthZP(MonthEmployee MonthEmployee)
        {
            this.MonthEmployee = MonthEmployee;
            MonthBonus = 35;
            if (MonthEmployee.DaysNotWorkedVacation > 0 || MonthEmployee.DaysNotWorkedMatherhoodVacation > 0)
            { BonusOverWorkingWhenVocationChk = true; }
            CalculateBonus();
            Saved = false;
        }
        public MonthZP(int MonthBonus, int FreeBonus, int Salary, DateTime SavedDate, MonthEmployee MonthEmployee)
        {
            this.MonthBonus = MonthBonus;
            this.FreeBonus = FreeBonus;
            this.Salary = Salary;
            this.SavedDate = SavedDate;
            this.MonthEmployee = MonthEmployee;
            if (MonthEmployee.DaysNotWorkedVacation > 0 || MonthEmployee.DaysNotWorkedMatherhoodVacation > 0)
            { BonusOverWorkingWhenVocationChk = true; }
            CalculateBonus();
            Saved = true;
        }
        DateTime savedDate;
        public DateTime SavedDate
        {
            get { return savedDate; }
            set
            {
                savedDate = value;
                OnPropertyChanged("SavedDate");
            }
        }


        MonthEmployee monthEmployee;
        public MonthEmployee MonthEmployee
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
        public double BiznessTripBonus 
        { 
            get
            { return biznessTripBonus; }
            set 
            { 
                biznessTripBonus = Math.Round(value); 
                CalculateZP(); 
            }
        }
        double overWorkingBonus { get; set; }
        public double OverWorkingBonus
        {
            get {
                return overWorkingBonus;
            }
            set
            {
                overWorkingBonus =  Math.Round(value);
                if (BonusOverWorkingWhenVocationChk == false)
                { OverWorkingBonusRezult = OverWorkingBonus; }
            }
        }
        double overWorkingBonusRezult { get; set; }
        public double OverWorkingBonusRezult
        {
            get
            {
                return overWorkingBonusRezult;
            }
            set
            {
                overWorkingBonusRezult = value;
                CalculateZP();
                OnPropertyChanged("OverWorkingBonusRezult");
                BonusSumma = FreeBonus + Convert.ToInt32(Math.Round(value));
            }
        }
        double overWorkingBonusIfVocation { get; set; }
        public double OverWorkingBonusIfVocation
        {
            get { return overWorkingBonusIfVocation; }
            set
            {
                overWorkingBonusIfVocation = Math.Round(value);
                if (BonusOverWorkingWhenVocationChk==true)
                { OverWorkingBonusRezult = overWorkingBonusIfVocation; }
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
                BonusSumma = value + Convert.ToInt32(Math.Round(OverWorkingBonusRezult)); ;
            }
        }
        int bonusSumma;
        public int BonusSumma
        {
            get { return bonusSumma; }
            set
            {
                bonusSumma = value;
                OnPropertyChanged("BonusSumma");
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
        int salary;
        public int Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
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
            if (value==true)
                { OverWorkingBonusRezult = OverWorkingBonusIfVocation; }
            else
                { OverWorkingBonusRezult = OverWorkingBonus; }
            }
        }

        bool saved;
        public bool Saved
        {
            get
            {
                return saved;
            }
            set
            {
                saved = value;
            }
        }

        public void CalculateZP()
        {
            try
            {
                double hourCost = (MonthEmployee.Employee.Salary / MonthEmployee.WorkedHoursInMonthPlaned.TotalHours);
            double MonthCostBase = hourCost * MonthEmployee.Time1X.TotalHours;
            double MonthCostWhithOverworking = MonthCostBase + hourCost * MonthEmployee.Time15X.TotalHours * 1.5
                + hourCost * MonthEmployee.Time20X.TotalHours * 2 + hourCost * MonthEmployee.TimeHoli.TotalHours * 2;
           

                //if (BonusOverWorkingWhenVocationChk == true)
                //{
                //    ZP = Convert.ToInt32(MonthEmployee.Employee.Salary + (MonthEmployee.Employee.Salary / 100) * MonthBonus
                //+ (MonthEmployee.Employee.Salary / 100) * BiznessTripBonus + (MonthEmployee.Employee.Salary / 100) * OverWorkingBonusIfVocation
                //    + (MonthEmployee.Employee.Salary / 100) * FreeBonus);
                //    ZPwithout13 = ZP - (ZP / 100) * 13;
                //}
                //else
                //{
                    ZP = Convert.ToInt32(MonthEmployee.Employee.Salary + (MonthEmployee.Employee.Salary / 100) * MonthBonus
               + (MonthEmployee.Employee.Salary / 100) * BiznessTripBonus + (MonthEmployee.Employee.Salary / 100) * OverWorkingBonusRezult
                   + (MonthEmployee.Employee.Salary / 100) * FreeBonus);
                    ZPwithout13 = ZP - (ZP / 100) * 13;
                //}
            }
            catch (Exception ex) {
                Loger.SetLog(ex.Message.ToString());
            }
        }
        public void CalculateBonus()
        {
            //Бонус за переработки
            double bonus15 = (100 * MonthEmployee.Time15X.TotalHours / MonthEmployee.WorkedHoursInMonthPlaned.TotalHours) * 1.5;
            double bonus20 = 100 * MonthEmployee.Time20X.TotalHours / MonthEmployee.WorkedHoursInMonthPlaned.TotalHours * 2;
            OverWorkingBonus = bonus15 + bonus20;
            double OverWorkingBonusIfVocation15 = bonus15 * 22 / (MonthEmployee.DaysWorked + MonthEmployee.DaysWorkedBusinessTrip);
            double OverWorkingBonusIfVocation20 = bonus20 * 22 / (MonthEmployee.DaysWorked + MonthEmployee.DaysWorkedBusinessTrip);
            OverWorkingBonusIfVocation = OverWorkingBonusIfVocation15 + OverWorkingBonusIfVocation20;
            if (bonus15 == overWorkingBonus && bonus20 == 0)
            { OverWorkingBonusIfVocation = 0; }
            if (MonthEmployee.DaysWorkedBusinessTrip > 0 && MonthEmployee.DaysWorkedBusinessTrip < 7)
            { BiznessTripBonus = 0.35 * MonthEmployee.DaysWorkedBusinessTrip / MonthEmployee.WorkedDayInMonthPlaned * 100; }
            else
            {
                BiznessTripBonus = 0.35 * MonthEmployee.DaysWorkedBusinessTrip / (MonthEmployee.WorkedDayInMonthPlaned * (MonthEmployee.DaysWorkedBusinessTrip - 7)
                 / MonthEmployee.WorkedDayInMonthPlaned + 1);
            }
        }
    }
}
