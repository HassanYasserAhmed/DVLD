using System.ComponentModel.DataAnnotations;
namespace DVLD.DataAccessLayer.DTOs
{
    public class UpdatePersonDTO
    {
        [Required(ErrorMessage = "ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ID must be greater than 0")]
        public int PersonID { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name can't exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Second name is required")]
        [MaxLength(50)]
        public string SecoundName { get; set; }

        [MaxLength(50)]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "National Number is required")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National Number must be 14 digits")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National Number must be numeric")]
        public string NationalNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Gender")]
        public int GendorID { get; set; } = -1;

        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Country")]
        public int CountryID { get; set; } = -1;

        [MaxLength(100)]
        public string ImageName { get; set; } = string.Empty;
    }
}
