import { useState, useEffect } from "react";
import { fetchPeopleApi} from "../Api/PeopleApi";
const usePeopleTable = () => {
  const [filterColumn, setFilterColumn] = useState("None");
  const [filterValue, setFilterValue] = useState("");
  const [IsOpened, setIsOpened] = useState(false);
  const [People, setPeople] = useState([]);
  const [FilteredPeople, setFilteredPeople] = useState([]);
  
  const HandelGetAllPeople = async () => {
     const {success,message,data} = await fetchPeopleApi(localStorage.getItem("token"));
    if (success) {
      setPeople(data);
      setFilteredPeople(data);
    }
  }

   useEffect(() => {
      HandelGetAllPeople();
    },[]);

  useEffect(() => {
    if(filterColumn != "None" || filterValue != null && filterValue != "") {
     const filteredData = People.filter(user => {
    const value = user[filterColumn];
    if (!value) return false;
    return value.toString().toLowerCase().includes(filterValue.toLowerCase());
  });

    setFilteredPeople(filteredData);
    }
  }, [filterColumn, filterValue]);

  return {
    FilteredPeople,
    filterColumn,
    setFilterColumn,
    filterValue,
    setFilterValue,
    IsOpened,
    setIsOpened
  };
};
export default usePeopleTable;