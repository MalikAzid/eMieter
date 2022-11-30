using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eMieter.Entities
{
    public class tblTenancy
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime RentalStartDate { get; set; }
        public Nullable<DateTime> RentalEndDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal RentalCostNetMonthly { get; set; }
        [Required(ErrorMessage = "Required")]
        public decimal RentalAdditionalCostsAkonto { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        [Required(ErrorMessage = "Required")]
        public Guid RentalPropertyId { get; set; }
        [Required(ErrorMessage = "Required")]
        public Guid TenantId { get; set; }
        [ForeignKey("RentalPropertyId")]
        public virtual tblRentalProperty tblRentalProperty { get; set; }
        [ForeignKey("TenantId")]
        public virtual tblTenant tblTenant { get; set; }
    }
}
