namespace DVLD.DataAccessLayer.DTOs.Person
{
    public class PersonDTO
    {
        public int PersonID { get; set; } = -1;
        public string FirstName { get; set; } = string.Empty;
        public string SecountName { get; set; } = string.Empty;
        public string ThirdName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalNumber { get; set; }= string.Empty;
        public DateTime? DateOfBirth { get; set; } = null;
        public string Phone {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string GendorName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
    }
}
