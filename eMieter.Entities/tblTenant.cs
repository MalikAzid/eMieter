using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eMieter.Entities
{
  public class tblTenant
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual tblTenancy tblTenancy { get; set; }
    }
}
