using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public class Mechanic
    {
        private int _id;

        [Key]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _firstName;
        private string _lastName;
        private string _description;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value ?? string.Empty; }
        }


        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value ?? string.Empty; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value ?? string.Empty; }
        }
        private uint days;
        private List<Task> _tasks;//use include in order to get that from the database

        public bool IsFree(Task task) => _tasks.All(t => !t.IsOverlaped(task));

    }
}
