using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public class Mechanic
    {

        [Key]
        public int Id { get; set; }
        public string LastName
        {
            get;set;
        }


        public string FirstName
        {
            get;set;
        }
        
        public string Description
        {
            get;set;
        }

        public int WorkshopId { get; set; } 
        [ForeignKey(nameof(WorkshopId))]
        public Workshop Workshop { get; set; }

        public List<Task> Tasks { get; set; }

    }
}
