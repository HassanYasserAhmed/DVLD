using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.DataAccessLayer.DTOs.Application
{
    public class AddNewLDLApplicationDTO
    {
        public int PersonID { get; set; }
        public int LicenseClassID { get; set; }
        public decimal ApplicationFees { get; set; }
        public string ApplicationType { get; set; }
        public int CreatedBy { get; set; }
    }
}
