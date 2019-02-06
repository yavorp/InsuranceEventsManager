using InsuranceManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace InsuranceManagerTlerikUI.Code
{
    public class AccidentUtil : INotifyPropertyChanged
    {
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
        private int _id;
        private string _fullNameOfPerson;
        private string _description;
        private string _regNumber;
        private string _accidentDate;
        private DateTime _createdDate;
        private string _lastModified;
        public string FullNameOfPerson { get; set; }
        public string Description { get; set; }
        public string RegNumber { get; set; }
        public string AccidentDate { get; set; }
        public string CreatedDate;
        public string LastModified { get; set; }
        public string Status;
        public int statusId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string DamageLevel { get; set; }

        public AccidentUtil(Accident accident)
        {
            ID = accident.Id;
            FullNameOfPerson = $"{accident.FirstName} { accident.LastName}";
            Description = accident.Description;
            RegNumber = accident.RegistrationNumber;
            AccidentDate = accident.AccidentDate.Day.ToString() + ". " + accident.AccidentDate.Month.ToString()
                + ". " + accident.AccidentDate.Year;
            if (accident.CreatedDate == DateTime.MinValue)
                CreatedDate = "";
            statusId = (int)accident.Status;

        }
        public AccidentUtil()
        {
            ID = 0;
            FullNameOfPerson = "";
            Description = "";
            AccidentDate = "";
            RegNumber = "";
            statusId = 0;
            Status = "";
            LastModified = "";
        }
    }
}
