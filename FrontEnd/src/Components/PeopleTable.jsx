import usePeopleTable from '../CustomHooks/usePeopleTable';
import AddEditPersonForm from './AddEditPersonForm';
const PeopleTable = ({handleContextMenu, handleTouchStart, handleTouchEnd}) => {
  const  {FilteredPeople,
     filterColumn,
      setFilterColumn,
      filterValue,
      setFilterValue,
      filteredData
      }
   = usePeopleTable();
  return (
    <div className="max-w-full mx-auto bg-white shadow-md rounded-lg overflow-x-auto">
      {/* Filter controls */}
      <div className=" flex gap-4 p-4 justify-between items-center">
        <div>
        <select
          className="mb-2 sm:mb-0 border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          value={filterColumn}
          onChange={(e) => {
                setFilterColumn(e.target.value);
                setFilterValue("");
          }}
        >
          <option value="None">None</option>
          <option value="personID">Person ID</option>
          <option value="nationalNumber">National No</option>
          <option value="phone">Phone</option>
          <option value="email">Email</option>
        </select>
{ filterColumn !== "None" && ( 
        <input
          type={`${filterColumn === "id" || filterColumn === "nationalNo" ? "number" : "text"}`}
          placeholder={`Filter by ${filterColumn}`}
          className="ml-2 flex-1 md:max-w-[400px] border border-gray-300 rounded-md p-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          value={filterValue}
          onChange={(e) => setFilterValue(e.target.value)}
        />
)}
        </div>
      </div>

      {/* Table */}
      <table className="min-w-full divide-y divide-gray-200">
        <thead className="bg-gray-50">
          <tr>
           <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Person ID</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">National No</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">First Name</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Secound Name</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Third Name</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Last Name</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Gendor</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Date Of Birth</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Nationality</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Phone</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Email</th>
          </tr>
        </thead>
        <tbody className="bg-white divide-y divide-gray-200">
          {FilteredPeople.map(user => (
            <tr
              key={user.personID}
              className="hover:bg-gray-100 cursor-pointer"
              onContextMenu={(e) => handleContextMenu(e, user)}
              onTouchStart={(e) => handleTouchStart(e, user)}
              onTouchEnd={handleTouchEnd}
            >
              <td className="px-6 py-4 whitespace-nowrap">{user.personID}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.nationalNumber}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.firstName}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.secoundName}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.thirdName}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.lastName}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.gendorName}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.dateOfBirth}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.countryName}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.phone}</td>
            <td className="px-6 py-4 whitespace-nowrap">{user.email}</td>
            </tr>
          ))}
     
        </tbody>
      </table>
    </div>
  );
};

export default PeopleTable;