namespace DVLD.DataAccessLayer.DTOs.Test
{
    public class TestAppointmentDTO
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsLocked { get; set; }
    }
}
