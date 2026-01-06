using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.DataAccessLayer.DTOs.User
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public DateTime TokenExpiredAt { get; set; }
    }
}