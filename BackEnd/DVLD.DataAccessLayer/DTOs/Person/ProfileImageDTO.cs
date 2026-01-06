namespace DVLD.DataAccessLayer.DTOs.Person
{
    public class ProfileImageDTO
    {
        public int PersonID { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ImageName { get; set; }
    }
}
