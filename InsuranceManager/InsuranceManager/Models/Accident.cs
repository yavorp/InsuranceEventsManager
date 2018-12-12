using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public enum Status
    {
        None=0,
        ToBeHandled = 1,
        LowPriority = 2,
        Declined = 3
    }

    public enum DamageLevel
    {
        Low = 1,
        High = 2,
        Medium = 3,
        None = 0
    }

    public class Accident:INotifyPropertyChanged
    {
        #region Data members
        private int _id;
        private string _regNumber;
        private string _firstName;
        private string _LastName;
        private DateTime _dateOfAccident;

        public Status _status;
        public DamageLevel _level; 
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public string FullName => $"{_firstName} {_LastName}";
        public int Id => _id;
        public string RegNumber => _regNumber;
        public DateTime Date => _dateOfAccident;
        #endregion

        #region Constructors
        public Accident(int id, string regNumber, string firstName, string lastName, DateTime date, Status status, DamageLevel lvl)
        {
            _id = id;
            _regNumber = regNumber;
            _firstName = firstName;
            _LastName = lastName;
            _dateOfAccident = date;
            _status = status;
            _level = lvl;
        }

        public Accident() : this(0, default(string), default(string), default(string), default(DateTime), Status.None, DamageLevel.None)
        { }

        public Accident(Accident accident) : this(accident.Id, accident.RegNumber, accident._firstName, accident._LastName, accident.Date, accident._status, accident._level)
        { }
        #endregion

    }


}
