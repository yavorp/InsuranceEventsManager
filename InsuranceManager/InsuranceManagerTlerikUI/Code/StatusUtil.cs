using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerTlerikUI.Code
{
    public class StatusUtil:INotifyPropertyChanged
    {
        private int _id;
        private string _name;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id
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

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value ?? "Няма наличен статус";
                this.OnPropertyChanged("Name");
            }
        }

        public StatusUtil(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public StatusUtil() : this(0, null) { }

        public StatusUtil(StatusUtil status):this(status.Id, status.Name) { }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
