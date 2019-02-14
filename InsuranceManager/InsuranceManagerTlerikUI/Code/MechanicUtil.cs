using InsuranceManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerTlerikUI.Code
{
    public class MechanicUtil
    {
        private int _id;
        private string _description;
        private string _firstName;
        private string _lastName;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value ?? "";
            }
        }




        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value ?? "";
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

        private IList<InsuranceManager.Models.Task> _tasks;

        public MechanicUtil(Mechanic mechanic)
        {
            Id = mechanic.Id;
            FirstName = mechanic.FirstName;
            LastName = mechanic.LastName;
            Description = mechanic.Description;
            _tasks = mechanic.Tasks;
        }

        public bool CanBeAdded(TaskUtil task)
        {
            bool flag = true;
            foreach (var item in this.Tasks)
            {
                if (!item.IsOverlaped(task))
                {
                    flag = false;
                    break;
                }

            }
            return flag;
        }

        public ObservableCollection<TaskUtil> Tasks => _tasks.GetObservable(a =>
        {
            var item = new TaskUtil(a);
            return item;
        });

        public override string ToString()
        {
            return $"{FirstName} {LastName} - {Description}";
        }
    }
}
