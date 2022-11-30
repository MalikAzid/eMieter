using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eMieter.Entities
{
   public class tblHouse
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public string City { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        [Required(ErrorMessage = "Required")]
        public Guid OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual tblOwner tblOwner { get; set; }
        public virtual List<tblRentalProperty> tblRentalProperty { get; set; }
    }
}
