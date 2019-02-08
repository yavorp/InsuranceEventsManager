﻿using InsuranceManager.Models;
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
        private string _accidentDate;
        private DateTime _createdDate;
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
                if(null==this._statusUtils)
                {
                    _statusUtils = new ObservableCollection<StatusUtil>();
                }
                return _statusUtils;
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

        public string StatusName
        {
            get
            {
                var selectedStatus = _statusUtils.Where(s => s.Id == StatusId).FirstOrDefault();
                return selectedStatus.Name;
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

        public string AccidentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModified { get; set; }
        public int StatusId
        {
            get
            {
                return statusId;
            }
            set
            {
                statusId = value >= 0 ? value : 0;
                OnPropertyChanged(nameof(StatusId));
                OnPropertyChanged(nameof(StatusName));
            }
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
            if(accident.AccidentDate==DateTime.MinValue)
            {
                AccidentDate = "";
            }
            else
            AccidentDate = $"{accident.AccidentDate.Day.ToString()}.{accident.AccidentDate.Month.ToString()}.{accident.AccidentDate.Year}";

            AccidentDate = accident.AccidentDate.ToString("dd.MM.yyyy");
           
            StatusId = (int)accident.Status;

            if(accident.LastModified==DateTime.MinValue)
            {
                LastModified = "";
            }
            else
            {
                LastModified= $"{accident.LastModified.Day.ToString()}.{accident.LastModified.Month.ToString()}.{accident.LastModified.Year}";
            }
            CreatedDate = accident.CreatedDate;

            _statusUtils = Enum.GetValues(typeof(Status)).Cast<Status>()
                .Aggregate(new ObservableCollection<StatusUtil>(), (accummulate, item) =>
                {
                    accummulate.Add(new StatusUtil((int)item, StatusTexts[item]));
                    return accummulate;
                });

            _statusUtils = new ObservableCollection<StatusUtil>();
            _statusUtils.Add(new StatusUtil(0, "Необработено"));
            _statusUtils.Add(new StatusUtil(1, "Готово за обработка"));
            _statusUtils.Add(new StatusUtil(2, "С нисък приоритет"));
            _statusUtils.Add(new StatusUtil(3, "Необработено"));

        }
        public AccidentUtil()
        {
            ID = 0;
            FullNameOfPerson = "";
            Description = "";
            AccidentDate = "";
            RegNumber = "";
            StatusId = 0;
            LastModified = "";
            _statusUtils = new ObservableCollection<StatusUtil>();
            _statusUtils[0] = new StatusUtil(0, "Необработено");
            _statusUtils[1] = new StatusUtil(1, "Готово за обработка");
            _statusUtils[2] = new StatusUtil(2, "С нисък приоритет");
            _statusUtils[3] = new StatusUtil(3, "Отказано събитие");
        }

        protected virtual void OnPropertyChanged(string args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
                handler?.Invoke(this, new PropertyChangedEventArgs(args));
        }
    }
}
