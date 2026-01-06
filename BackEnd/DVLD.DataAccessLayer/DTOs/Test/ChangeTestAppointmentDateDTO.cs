using System;
using System.Collections.Generic;
using System.Text;

namespace DVLD.DataAccessLayer.DTOs.Test
{
    public class ChangeTestAppointmentDateDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime NewTestAppointmentDate {  get; set; }
    }
}
