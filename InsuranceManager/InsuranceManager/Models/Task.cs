using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        
        public int MechanicId { get; set; }

        [ForeignKey(nameof(MechanicId))]
        public Mechanic Mechanic { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsOverlaped(Task task)
        {
            return !(StartDate.Date > task.EndDate.Date || EndDate.Date < task.StartDate);
        }

    }
}
