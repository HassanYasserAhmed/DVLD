import Navbar from "../Components/Navbar";
import useContextMenu from "../CustomHooks/useContextMenu";
import Footer from "../Components/Footer";
import PeopleContextMenue from "../Components/PeopleContextMenue";
import usePeopleTable from "../CustomHooks/usePeopleTable";
import AddEditPersonForm from "../Components/AddEditPersonForm";
import PeopleTable from "../Components/PeopleTable";
import useValidations from "../CustomHooks/useValidations";
import usePeople from "../CustomHooks/usePeople";
const PeoplePage = () => {
    const {
  showMenu,
  menuPosition,
  selectedRow,
  handleContextMenu,
  handleTouchStart,
  handleTouchEnd,
  handleOptionClick,handleClick
} = useContextMenu();

const {IsOpened, setIsOpened} = usePeopleTable();

const {
  HandelPhoneValidation,
  PhoneValidation,
  setPhoneValidation,
  HandelNationalNumberValidation,
    NationalNumberValidation,
    setNationalNumberValidation,HandelEmailValidation,
  setEmailValidation,
  EmailValidation
    } = useValidations();

    const {HandelAddPerson, IsAdded, setIsAdded} = usePeople();
  return (
    <div className="flex flex-col min-h-screen">
  <Navbar />

  <main className="flex-1 bg-gray-100 p-6" onClick={handleClick}>
    <img src="./People 400.png" alt="PeoplePage" className="w-32 h-32 mx-auto mt-4" />
    <h1 className="text-4xl text-center m-4">Manage PeoplePage</h1>
    <div className="mb-4 flex justify-end">
    <button onClick={()=>{setIsOpened(true)}} className="p-2 rounded border focus:ring-1 focus:ring-black">
        <img src="Add Person 72.png" alt="Add Person" />
        </button>
      </div>
  
    <PeopleTable 
      handleContextMenu={handleContextMenu} 
      handleTouchStart={handleTouchStart} 
      handleTouchEnd={handleTouchEnd}

    />

    {showMenu && selectedRow && (
      <PeopleContextMenue
        menuPosition={menuPosition} 
        onOptionClick={handleOptionClick}
        options={["Edit", "Delete", "View"]}
      />
    )}

      {IsOpened && <AddEditPersonForm
        IsOpened={IsOpened} 
        setIsOpened={setIsOpened}
        EmailValidation={EmailValidation}
        setEmailValidation={setEmailValidation}
        HandelEmailValidation={HandelEmailValidation}
        NationalNumberValidation={NationalNumberValidation}
        setNationalNumberValidation={setNationalNumberValidation}
        HandelNationalNumberValidation={HandelNationalNumberValidation}
        HandelPhoneValidation={HandelPhoneValidation}
        PhoneValidation={PhoneValidation}
        setPhoneValidation={setPhoneValidation}
        HandelAddPerson={HandelAddPerson}
        IsAdded={IsAdded}
        setIsAdded={setIsAdded}
        />}
  </main>
  <Footer />
</div>

  );
};
export default PeoplePage;
