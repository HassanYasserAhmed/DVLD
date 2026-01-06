import React from 'react'
import { NavLink } from "react-router-dom";
const DesktopNavbar = () => {
  return (
    <>
           {/* Desktop Links */}
<div className={`hidden md:flex
    absolute left-0 top-full w-full bg-blue-600 sm:static sm:w-auto sm:flex
    transform transition-all duration-300 ease-in-out py-2 sm: px-2
       "opacity-100 translate-y-0 visible"`}

>        <div className="flex flex-col sm:flex-row sm:space-x-4 space-y-2 sm:space-y-0 text-end mt-2 sm:mt-0 font-medium">
          <a href="#" className="hover:underline hover:text-gray-300 text-sm sm:text-base">ApplicationsD</a>
          <a href="/People" className="hover:underline hover:text-gray-300 text-sm sm:text-base">People</a>
          <a href="#" className="hover:underline hover:text-gray-300 text-sm sm:text-base">Drivers</a>
          <a href="#" className=" hover:underline hover:text-gray-300 text-sm sm:text-base">Users</a>
          <NavLink to="/Login" href="login" className="block sm:hidden hover:text-gray-300 hover:underline text-sm sm:text-base">Login</NavLink>
        </div>
</div>
     <div className="hidden md:flex mt-2 md:mt-0 md:ml-4">
  <a href="login" className="hover:underline hover:text-gray-300 text-sm sm:text-base">Login</a>
  </div>
      </>

  )
}

export default DesktopNavbar
