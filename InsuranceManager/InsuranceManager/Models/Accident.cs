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
        private string _damageDescription;

        private Status _status;
        public DamageLevel _level;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public string FullName => $"{_firstName} {_LastName}";
        public int Id => _id;
        public string RegNumber => _regNumber;
        public DateTime Date => _dateOfAccident;
        public Status Status_Prop
        {
            get => _status;
            set
            {
                if(value==Status.ToBeHandled)
                {
                    _status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_status)));
                }
            }
        }
        #endregion

        #region Constructors
        public Accident(int id, string regNumber, string firstName, string lastName, DateTime date, Status status, DamageLevel lvl,string damageDescription)
        {
            _id = id;
            _regNumber = regNumber;
            _firstName = firstName;
            _LastName = lastName;
            _dateOfAccident = date;
            _status = status;
            _level = lvl;
        }

        public Accident() : this(0, default(string), default(string), default(string), default(DateTime), Status.None, DamageLevel.None,default(string))
        { }

        public Accident(Accident accident) : this(accident.Id, accident.RegNumber, accident._firstName, accident._LastName, accident.Date, accident._status, accident._level,accident._damageDescription)
        { }
        #endregion

    }


}
