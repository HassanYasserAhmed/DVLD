using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.DataAccessLayer.DTOs.Test
{
    public class TakeTestDTO
    {
        public int TestAppointmentID { get; set; }
        public bool IsPassed { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
