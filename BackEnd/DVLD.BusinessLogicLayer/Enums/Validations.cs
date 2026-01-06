namespace DVLD.BusinessLogicLayer.Enums
{
    public enum enValidationStatus
    {
        Success = 0,
        Exists=1,
        NotExists=2,

        EmailAlreadyExists = 1,
        PhoneAlreadyExists = 2,
        NationalNumberAlreadyExists = 3,
        AgeUnder18 = 4,
        InvalidEmailFormat = 5,
        InvalidPhoneFormat = 6,
        RequiredFieldMissing = 7,
        PasswordTooWeak = 8,
        InvalidEmail=9,
        InvalidNationalNumber=10,
        InValidPhon=11,
        GeneralFailure = 99
    }
}
