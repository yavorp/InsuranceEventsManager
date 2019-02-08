using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerTlerikUI.Code
{
    class Works
    { 
        public int Id { get; set; }
        public List<MechanicUtil> Mechanics { get; set; }//doesnt need full encapsulation

        public string Name { get; set; }
    }
}
