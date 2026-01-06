using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.DataAccessLayer.DTOs.License
{
    public class GetInternationalDrivingLicensesDTO
    {
      public int IntDLicenseID { get; set; }
      public int LLicenseID { get; set; }
      public int ApplicationID { get; set; }
      public int DriverID { get; set; }
      public DateTime IssueDate { get; set; }
      public DateTime ExpirateionDate { get; set; }
      public bool IsActive { get; set; }
      public string FullName { get; set; }
      public string Gendor {  get; set; }
      public string NationalNumber { get; set; }
      public DateOnly DateOfBirth { get; set; }

    }
}
