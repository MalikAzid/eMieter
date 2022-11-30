using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eMieter.Entities
{
   public class tblRentalProperty
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string EWID { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal LivingAreaSquareMeters { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal RoomCount { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Floor { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        [Required(ErrorMessage = "Required")]
        public Guid HouseId { get; set; }
        [ForeignKey("HouseId")]
        public virtual tblHouse tblHouse { get; set; }
        public virtual List<tblTenancy> tblTenancy { get; set; }
    }
}
