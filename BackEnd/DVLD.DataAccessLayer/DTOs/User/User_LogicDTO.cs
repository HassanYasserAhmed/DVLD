using DVLD.DataAccessLayer.DTOs.Person;

namespace DVLD.DataAccessLayer.DTOs.User
{
    public class User_LogicDTO : Person_LogicDTO
    {
        public int UserID { get; set; } = -1;
        public bool IsActive { get; set;} = false;
        public int RoleID { get; set; } = -1;
        public string Password { get; set; } = string.Empty;
    }
}