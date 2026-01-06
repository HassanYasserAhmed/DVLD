using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.DataAccessLayer.DTOs.Application
{
    public class UpdateApplicationTypeDTO
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationName { get; set; }
        public decimal ApplicationTypeFees { get; set; }
    }
}
