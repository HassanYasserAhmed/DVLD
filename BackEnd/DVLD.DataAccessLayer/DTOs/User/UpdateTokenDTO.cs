namespace DVLD.DataAccessLayer.DTOs.User
{
    public class UpdateTokenDTO
    {
        public int UserID { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
