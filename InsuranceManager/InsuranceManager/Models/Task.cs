using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public class Task
    {
        private DateTime _startDate;
        private DateTime _endDate;
        [Key]
        public int Id { get; set; }
        public Mechanic Mechanic { get; set; }
        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value.Date < DateTime.Today ? DateTime.Today : value.Date;

        }

        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value.Date < StartDate ? StartDate : value.Date;

        }

        public bool IsOverlaped(Task task)
        {
            return !(StartDate.Date > task._endDate.Date || EndDate.Date < task.StartDate);
        }

    }
}
