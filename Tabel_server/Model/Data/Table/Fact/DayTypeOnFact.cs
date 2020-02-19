using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabel_server.Model.Data.Table.EmployeeDay
{
    public enum DayTypeOnFact
    {
        /// <summary>Рабочий день
        /// </summary>
        /// 
        Holiday,
        /// <summary>Рабочий день
        /// </summary>
        Worked,
        /// <summary>Командировка
        /// </summary>
        WorkedBusinessTrip,
        /// <summary>Больничный
        /// </summary>
        NotWorkedSick,
        /// <summary>Отпуск по беременности
        /// </summary>
        NotWorkedMatherhoodVacation,
        /// <summary>Отпуск
        /// </summary>
        NotWorkedVacation,
        /// <summary>Административный отгул
        /// </summary>
        NotWorkedAdministrative
    }


}
