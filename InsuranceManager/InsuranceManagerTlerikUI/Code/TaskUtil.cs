using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerTlerikUI.Code
{
    public class TaskUtil
    {
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;
        public int Id { get; set; }

        public int MechanicId { get; set; }

       // public Mechanic Mechanic { get; set; }

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value >= DateTime.Now ? value : DateTime.Now;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value >= StartDate ? value : StartDate;
            }
        }

        public TaskUtil(InsuranceManager.Models.Task task)
        {
            Id = task.Id;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
        }

        public TaskUtil()
        {
        }

        public bool IsOverlaped(TaskUtil task)
        {
            return ((task.StartDate<StartDate && task.EndDate<StartDate)||(task.StartDate>StartDate && task.StartDate>EndDate));
        }
    }
}
