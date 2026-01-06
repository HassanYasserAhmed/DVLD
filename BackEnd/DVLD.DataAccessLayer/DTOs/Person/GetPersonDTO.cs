namespace DVLD.DataAccessLayer.DTOs.Person
{
    public class GetPersonDTO
    {
        public int PersonID { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? SecoundName { get; set; } = string.Empty;
        public string? ThirdName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;
        public string? NationalNumber { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? Phone { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? ImageName { get; set; } = string.Empty;
        public string? GendorName { get; set; } = string.Empty;

        public string? CountryName { get; set; } = string.Empty;

    }
}
