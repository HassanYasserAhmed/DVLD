namespace DVLD.DataAccessLayer.DTOs.User
{
    public class ChangePasswordDTO
    {
        public int UserID { get; set; }
        public string NewPassword { get; set; }
    }
}
