import { useEffect, useState } from "react";
import { getCountriesApi } from "../Api/GeneralApi";
const useGeneral = () => {
    const [Countries, setCountries] = useState([]);
const HandelGetCountries = async() => {
    const data = await getCountriesApi();
    setCountries(data.map(c => ({ value: c.countryID, label: c.countryName })));
}

useEffect(() => {
HandelGetCountries();
}, []);
    return {
        Countries,
        setCountries
    };
}
export default useGeneral;