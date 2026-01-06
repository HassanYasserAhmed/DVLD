import { validateEmailApi, validateNationalNumberApi,validatePhoneApi } from "../Api/ValidationApi";
import { useState } from "react";
const useValidations = () => {
  const [EmailValidation, setEmailValidation] = useState(true);
  const [ NationalNumberValidation, setNationalNumberValidation] = useState(true);
  const [PhoneValidation, setPhoneValidation] = useState(true);
    const HandelEmailValidation = async (email) => {
        if(email == null || email =="")
            return;
      const {success,message} = await validateEmailApi(email);
      setEmailValidation(success);
      return {success,message};
    }

    const HandelNationalNumberValidation = async (nationalNumber) => {
      if(nationalNumber == null || nationalNumber == "") return;
      const {success,message} = await validateNationalNumberApi(nationalNumber);
      setNationalNumberValidation(success);
      return {success,message};
    }

    const HandelPhoneValidation = async (phone) => {
      if(phone == null || phone == "") return;
      const {success,message} = await validatePhoneApi(phone);
      setPhoneValidation(success);
      return {success,message};
    }

    return ({
    HandelPhoneValidation,
    PhoneValidation,
    setPhoneValidation,
    HandelNationalNumberValidation,
    NationalNumberValidation,
    setNationalNumberValidation,
    HandelEmailValidation,
    EmailValidation,
    setEmailValidation
  });
};
export default useValidations;