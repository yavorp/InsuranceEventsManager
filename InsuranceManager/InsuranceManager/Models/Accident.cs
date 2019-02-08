using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceManager.Models
{
    public enum Status
    {
        None=0,
        ToBeHandled = 1,
        LowPriority = 2,
        Declined = 3,
        Handled=4
    }

    public enum DamageLevel
    {
        Low = 1,
        High = 2,
        Medium = 3,
        None = 0
    }

    public class Accident
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime AccidentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Status Status { get; set; }
        public DamageLevel DamageLevel { get; set; }
        #endregion
    }


}
