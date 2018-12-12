using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public class Mechanic
    {
        private List<Task> _tasks;//use include in order to get that from the database

        public bool IsFree(Task task) => _tasks.All(t => !t.IsOverlaped(task));
        
    }
}
