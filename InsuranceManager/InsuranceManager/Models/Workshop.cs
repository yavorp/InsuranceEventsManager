using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public class Workshop
    {
        private string _name;
        private List<Mechanic> _mechanics;//doesnt need full encapsulation

        public string Name
        {
            get => _name;
            set
            {
                _name = value != null ? value : "";
            }
        }
    }
}
