import { useState } from "react";
import { AddPersonApi } from "../Api/PeopleApi";
const usePeople = () =>{
    const [IsAdded, setIsAdded] = useState(false);
    const token = localStorage.getItem("token");
    const HandelAddPerson = async(person) => {
        const {success,message,data} = await AddPersonApi(token,person);

        if(success)
            IsAdded(true);
        else
            IsAdded(false);
    }
    return (
        HandelAddPerson,
        IsAdded,
        setIsAdded
    );
}
export default usePeople;