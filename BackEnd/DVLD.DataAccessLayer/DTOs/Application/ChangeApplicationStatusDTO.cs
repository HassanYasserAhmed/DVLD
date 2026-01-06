using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.DataAccessLayer.DTOs.Application
{
    public class ChangeApplicationStatusDTO
    {
        public int ApplicationId { get; set; }  
        public int NewStatus { get; set; }
    }
}
