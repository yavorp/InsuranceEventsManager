using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }
        public List<Mechanic> Mechanics { get; set; }//doesnt need full encapsulation

        public string Name { get; set; }
    }
}
