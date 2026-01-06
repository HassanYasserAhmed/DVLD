namespace DVLD.BusinessLogicLayer.Enums.Person
{
    public enum enPersonStatus
    {
        Success=0,
        Found=1,
        NotFound=2,
        Authorized=3,
        Unauthorized=4,
        Faild=99
    }
    public enum AuthStatus
    {
        Success = 0,
        InvalidCredentials = 1,
        TokenInvalid = 2,
        TokenExpired = 3,
        UserNotFound = 4
    }
    public enum enAddPersonStatus
    {
        Success = 0,
        EmailAlreadyExists = 1,
        PhoneAlreadyExists = 2,
        NationalNumberAlreadyExists = 3,
        AgeUnder18 = 4,
        Failed = 99
    }
    public enum enUpdatePersonStatus
    {
        Success = 0,
        EmailAlreadyExists = 1,
        PhoneAlreadyExists = 2,
        NationalNumberAlreadyExists = 3,
        AgeUnder18 = 4,
        UserNotFound,
        Failed = 99
    }
    public enum enDeletePersonStatus
    {
        Success = 0,
        PersonNotFound = 1,
        Failed = 99
    }
    public enum enGetRoleByTokenStatus
    {
        Success = 0,
        TokenIsNull=1,
        UnauthorizedAccess=2,
    }
    public enum enAddImageStatus
    {
        Success =0,
        Failed=99
    }

    public enum enUpdateImageStatus
    {
        Success = 0,
        PersonNotFound=1,
        ImageNotFound = 2,
        FailedToDeleteOldImage=3,
        FailedToUploadNewImage=4,
        Failed = 99
    }
    public enum enGetRoleByEmailStatus
    {
        Success = 0,
        InvalidEmail =1,
        NotFound=2,
        Failed=99
    }
    public enum enGetAllPeopleStatus { Found = 0,NotFound=99}
    public enum enGetPersonStatus { Success = 0,Faild=99}
}
