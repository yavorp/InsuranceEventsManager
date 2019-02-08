using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManagerTlerikUI.Code
{
    public class MechanicUtil
    {
        private int _id;
        public int Id { get; set; }
        public string LastName
        {
            get; set;
        }


        public string FirstName
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public List<Task> Tasks { get; set; }
    }
}
