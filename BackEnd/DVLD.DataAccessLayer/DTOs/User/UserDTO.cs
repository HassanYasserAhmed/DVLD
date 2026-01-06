using DVLD.DataAccessLayer.DTOs.Person;

namespace DVLD.DataAccessLayer.DTOs.User
{
    public class GetUserDTO 
    {
        public int UserID { get; set; } = -1;
        public int PersonID { get; set; } = -1;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; } = null;
        public GetPersonDTO PersonInfo { get; set; }
    }
}
