using System;
using System.ComponentModel.DataAnnotations;

namespace eMieter.ViewModel
{
    public class UserVM
    {
        public Guid OwnerId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public bool StayLogin { get; set; }

    }
}
