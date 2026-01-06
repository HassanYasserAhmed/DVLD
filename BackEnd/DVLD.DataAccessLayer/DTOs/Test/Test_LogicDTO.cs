namespace DVLD.DataAccessLayer.DTOs.Test
{
    public class Test_LogicDTO
    {
        public int TestID { get; set; }
        public bool TestResult { get; set; }
        public bool IsLocked { get; set; }
        public int LDLApplicationID { get; set; }
        public int RetakeTestApplicationID {  get; set; }
        public int TestTypeID { get; set; }
    }
}
