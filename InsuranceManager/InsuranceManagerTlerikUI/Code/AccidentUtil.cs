using InsuranceManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace InsuranceManagerTlerikUI.Code
{
    public class AccidentUtil : INotifyPropertyChanged
    {
        private int _id;
        private string _fullNameOfPerson;
        private string _description;
        private string _regNumber;
        private string _lastModified;
        private ObservableCollection<StatusUtil> _statusUtils;

        private static readonly IDictionary<Status, string> StatusTexts = new Dictionary<Status, string>
        {
            { Status.None, "Необработено" }
        };


        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value >= 0 ? value : 0;
            }
        }

        public ObservableCollection<StatusUtil> StatusUtils
        {
            get
            {
                if (null == this._statusUtils)
                {
                    _statusUtils = new ObservableCollection<StatusUtil>();
                }
                return _statusUtils;
            }
        }

        public int StatusId
        {
            get
            {
                return statusId;
            }
            set
            {
                if (statusId != value)
                {
                    statusId = value >= 0 ? value : 0;
                    OnPropertyChanged("StatusId");
                    OnPropertyChanged("StatusName");
                }
            }
        }
        public string StatusName
        {
            get
            {
                var selectedStatus = _statusUtils.Where(s => s.Id == StatusId).FirstOrDefault();
                return selectedStatus.Name;
            }
        }

        public string FullNameOfPerson
        {
            get
            {
                return _fullNameOfPerson;
            }
            set
            {
                _fullNameOfPerson = value ?? "";
            }
        }


        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value ?? "";
            }
        }
        public string RegNumber
        {
            get
            {
                return _regNumber;
            }
            set
            {
                _regNumber = value ?? "";
            }
        }

        public DateTime AccidentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModified
        {
            get { return _lastModified; }
            set { _lastModified = value ?? ""; }
        }

        private int statusId;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DamageLevel { get; set; }

        public AccidentUtil(Accident accident)
        {
            ID = accident.Id;
            FullNameOfPerson = $"{accident.FirstName} { accident.LastName}"; // I set name this way because It is required property from the database it cannot be null
            Description = accident.Description;
            RegNumber = accident.RegistrationNumber;
            AccidentDate = accident.AccidentDate;
            StatusId = (int)accident.Status;
            if (accident.LastModified == DateTime.MinValue)
            {
                LastModified = "";
            }
            else
            {
                LastModified = $"{accident.LastModified.Day.ToString()}.{accident.LastModified.Month.ToString()}.{accident.LastModified.Year}";
            }
            CreatedDate = accident.CreatedDate;

            _statusUtils = new ObservableCollection<StatusUtil>();
            _statusUtils.Add(new StatusUtil(0, "Необработено"));
            _statusUtils.Add(new StatusUtil(1, "Готово за обработка"));
            _statusUtils.Add(new StatusUtil(2, "С нисък приоритет"));
            _statusUtils.Add(new StatusUtil(3, "Отказано събитие"));

        }
        public AccidentUtil()
        {
            ID = 0;
            FullNameOfPerson = "";
            Description = "";
            AccidentDate = DateTime.Now;
            RegNumber = "";
            StatusId = 0;
            LastModified = "";
            _statusUtils = new ObservableCollection<StatusUtil>();
            _statusUtils[0] = new StatusUtil(0, "Необработено");
            _statusUtils[1] = new StatusUtil(1, "Готово за обработка");
            _statusUtils[2] = new StatusUtil(2, "С нисък приоритет");
            _statusUtils[3] = new StatusUtil(3, "Отказано събитие");
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }
        public static string DateToString(DateTime date)
        {
            string result = "";
            if (date != DateTime.MinValue)
            {
                result = date.ToString("dd.MM.yyyy");
            }
            return result;
        }
        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
