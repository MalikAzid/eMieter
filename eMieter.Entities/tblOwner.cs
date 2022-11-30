using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eMieter.Entities
{
   public class tblOwner
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public virtual List<tblUser> tblUser { get; set; }
        public virtual List<tblHouse> tblHouse { get; set; }
    }
}
