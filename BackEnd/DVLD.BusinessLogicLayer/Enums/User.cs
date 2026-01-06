namespace DVLD.BusinessLogicLayer.Enums
{
   public enum enUser
    {
        success = 0,
        NotFound =1,
        OldPasswordIncorrect=2,
        UsedBefore=3,
        HasData=4,
        InvalidEmailOrPassword=5,
        NotActive=6,
        Failed=99
    }
}
