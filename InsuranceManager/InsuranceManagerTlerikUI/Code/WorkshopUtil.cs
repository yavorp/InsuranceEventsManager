using InsuranceManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerTlerikUI.Code
{
    public class WorkshopUtil:INotifyPropertyChanged
    {
        private string _name;
        private int _id;
        private int _mechanicId;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value > 0 ? value : 0;
            }
        }
        public ObservableCollection<MechanicUtil> Mechanics=> _mechanics.GetObservable(a =>
        {
            var item = new MechanicUtil(a);
            return item;
        });
        private List<Mechanic> _mechanics;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value ?? "";
            }
        }

        public string MechanicName
        {
            get;set;
        }

        public int MechanicId
        {
            get
            {
                return _mechanicId;
            }
            set
            {
                if (_mechanicId != value)
                {
                    _mechanicId = value >= 0 ? value : 0;
                    OnPropertyChanged("MechanicId");
                    OnPropertyChanged("MechanicName");
                }
            }
        }

        public WorkshopUtil(Workshop workshop)
        {
            Id = workshop.Id;
            Name = workshop.Name;
            _mechanics = workshop.Mechanics;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
