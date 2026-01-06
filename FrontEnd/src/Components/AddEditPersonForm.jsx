import {useState } from "react";
import { FaUser, FaPhone, FaEnvelope, FaLock } from "react-icons/fa";
import { IoMdClose, IoMdSave } from "react-icons/io";
import useGeneral from "../CustomHooks/useGeneral";
import Select from 'react-select';
const AddEditPersonForm = ({HandelAddPerson, IsAdded, setIsAdded, HandelPhoneValidation, PhoneValidation,setPhoneValidation
  ,NationalNumberValidation,setNationalNumberValidation,HandelNationalNumberValidation
  ,IsOpened,setIsOpened,EmailValidation,setEmailValidation,HandelEmailValidation}) => {
    const {handleCountryChange,Countries, setCountries } = useGeneral();
  const handleChange = (e) => {
    const { name, value } = e.target;
    setPerson({ ...person, [name]: value });
  };
  const handleImageChange = (e) => {
    const file = e.target.files[0];
    if (file) setPerson({ ...person, image: URL.createObjectURL(file) });
  };

  const handleSave = (e) => {
    e.preventDefault();
   HandelAddPerson(person);
  };

  const handleClose = () => {
    setIsOpened(false);
  };
    const [person, setPerson] = useState({
    firstName: "",
    secondName: "",
    thirdName: "",
    lastName: "",
    nationalNo: "",
    dateOfBirth: "",
    gender: 1,
    phone: "",
    email: "",
    country: 1,
    address: "",
    image: null,
  });


  return (
    <div
  className="fixed inset-0 flex items-center justify-center"
>
  <div className="max-w-4xl p-6 bg-white border border-gray-300 rounded-lg shadow-lg">
    <h2 className="text-3xl font-bold text-red-600 text-center mb-6">
      Add New Person
    </h2>

          <form className="grid grid-cols-12 gap-4" onSubmit={handleSave}>
        
        {/* Person ID */}
        <div className="col-span-12 flex items-center mb-4">
          <span className="font-semibold mr-2">Person ID:</span>
          <span>N/A</span>
        </div>
        {/* Name Fields */}
        <label className="col-span-12 sm:col-span-2 font-semibold flex items-center">
          <FaUser className="mr-1" /> Name:
        </label>
        <div className="col-span-12 sm:col-span-10 grid grid-cols-1 sm:grid-cols-4 gap-2">
          <input
            type="text"
            name="firstName"
            placeholder="First"
            value={person.firstName}
            onChange={handleChange}
            className="border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
          />
          <input
            type="text"
            name="secondName"
            placeholder="Second"
            value={person.secondName}
            onChange={handleChange}
            className="border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
          />
          <input
            type="text"
            name="thirdName"
            placeholder="Third"
            value={person.thirdName}
            onChange={handleChange}
            className="border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
          />
          <input
            type="text"
            name="lastName"
            placeholder="Last"
            value={person.lastName}
            onChange={handleChange}
            className="border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
          />
        </div>

        {/* National No */}
        <label className="col-span-12 sm:col-span-2 font-semibold flex items-center">
          <FaLock className="mr-1" /> National No:
        </label>
        <input
          type="text"
          name="nationalNo"
          value={person.nationalNo}
          onChange={handleChange}
          onBlur ={()=>{HandelNationalNumberValidation(person.nationalNo)}}
          className="col-span-12 sm:col-span-4 border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
        />
                {!NationalNumberValidation  && person.nationalNo !="" && <span className="text-red-500 text-sm">National Number is Not valid</span>}

        {/* Date of Birth */}
        <label className="col-span-12 sm:col-span-2 font-semibold">Date Of Birth:</label>
        <input
          type="date"
          name="dateOfBirth"
          value={person.dateOfBirth}
          onChange={handleChange}
          max="2005-12-31"
          className="col-span-12 sm:col-span-4 border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
        />

        {/* Gender */}
        <label className="col-span-12 sm:col-span-2 font-semibold">Gender:</label>
        <div className="col-span-12 sm:col-span-4 flex flex-wrap items-center gap-4">
          <label className="flex items-center gap-1">
            <input
              type="radio"
              name="gender"
              value={2}
              checked={person.gender === 2}
              onChange={(e) => { setPerson({ ...person, gender:2}); }}
            />{" "}
            Male
          </label>
          <label className="flex items-center gap-1">
            <input
              type="radio"
              name="gender"
              value={1}
              checked={person.gender === 1}
              onChange={(e) => { setPerson({ ...person, gender: 1 }); }}
            />{" "}
            Female
          </label>
        </div>

        {/* Phone */}
        <label className="col-span-12 sm:col-span-2 font-semibold flex items-center">
          <FaPhone className="mr-1" /> Phone:
        </label>
        <input
          type="text"
          name="phone"
          value={person.phone}
          onChange={handleChange}
          onBlur = {()=>{HandelPhoneValidation(person.phone)}}
          className="col-span-12 sm:col-span-4 border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
        />
        {!PhoneValidation  && person.phone !="" && <span className="text-red-500 text-sm">Phone is Not valid</span>}

        {/* Email */}
        <label className="col-span-12 sm:col-span-2 font-semibold flex items-center">
          <FaEnvelope className="mr-1" /> Email:
        </label>
        <input
          type="email"
          name="email"
          value={person.email}
          onChange={handleChange}
          onBlur={()=>{HandelEmailValidation(person.email)}}
          className="col-span-12 sm:col-span-4 border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 w-full"
        />
        {!EmailValidation  && person.email !="" && <span className="text-red-500 text-sm">Email is Not valid</span>}
        {/* Country */}
        <label className="col-span-12 sm:col-span-2 font-semibold">Country:</label>


<Select
  options={Countries}
  placeholder="Select Country"
  className="col-span-12 sm:col-span-4  px-2 py-1 w-full"
  value={Countries.find(option=> option.value== person.country)}
  onChange={(selectedOption) => {setPerson({...person,country:selectedOption.value})}}
/>
{/* Address + Image on same row */}
<div className="col-span-12 grid grid-cols-12 gap-4">
  {/* Address */}
  <div className="col-span-12 sm:col-span-8">
    <label className="font-semibold flex items-start mb-1">
      <FaLock className="mr-1 mt-1" /> Address:
    </label>
    <textarea
      name="address"
      value={person.address}
      onChange={handleChange}
      className="w-full border px-2 py-1 rounded focus:outline-none focus:ring-2 focus:ring-blue-400 min-h-[60px] resize-none"
    />
  </div>

  {/* Image */}
  <div className="col-span-12 sm:col-span-4 flex flex-col items-center">
    <label className="font-semibold mb-1">Image:</label>
    {person.image ? (
      <img
        src={person.image}
        alt="Person"
        className="w-40 h-40 object-cover rounded border"
      />
    ) : (
      <img
        src={person.gender == "Male" ? "./Male 512.png" : "./Female 512.png"}
        alt="Person"
        className="w-40 h-40 object-cover rounded border"
      />
    )}
    <input
      type="file"
      accept="image/*"
      onChange={handleImageChange}
      className="mt-1 text-sm underline text-blue-600 cursor-pointer"
    />
  </div>
</div>
        {/* Buttons */}
        <div className="col-span-12 flex flex-col sm:flex-row justify-center sm:space-x-4 gap-2 mt-6 mb-2">
          <button
            onClick={handleClose}
            className="flex items-center justify-center px-4 py-2 border border-gray-400 rounded hover:bg-gray-100 w-full sm:w-auto"
          >
            <IoMdClose className="mr-1" /> Close
          </button>

          <button
            type="submit"
            className="flex items-center justify-center px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 w-full sm:w-auto"
          >
            <IoMdSave className="mr-1" /> Save
          </button>
        </div>
      </form>
          <div className={`${IsAdded ? "block ": "hidden"} text-center`}>Person Added Successfully</div>
    </div>
</div>
  );
};

export default AddEditPersonForm;
