import { useState } from 'react'
import { NavLink } from 'react-router-dom'
const MobileNavbar = () => {
  const [isOpen, setIsOpen] = useState(false);
  return (
    <>
         <button
        className="sm:hidden text-white focus:outline-none"
        onClick={() => setIsOpen(!isOpen)}
      >
        <svg
          className="w-6 h-6"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d={isOpen ? "M6 18L18 6M6 6l12 12" : "M4 6h16M4 12h16M4 18h16"} />
        </svg>
      </button>

      {/* Mobile Links */}
<div className={`block md:hidden
    absolute left-0 top-full w-full bg-blue-600 sm:static sm:w-auto sm:flex
    transform transition-all duration-300 ease-in-out py-2 sm: px-2
    ${isOpen 
      ? "opacity-100 translate-y-0 visible" 
      : "opacity-0 -translate-y-3 invisible"}
  `}
>        <div className="flex flex-col sm:flex-row sm:space-x-4 space-y-2 sm:space-y-0 text-end mt-2 sm:mt-0 font-medium">
          <a href="#" className="text-sm sm:text-base">Applications</a>
          <a href="/People" className="text-sm sm:text-base">People</a>
          <a href="#" className="text-sm sm:text-base">Drivers</a>
          <a href="#" className="text-sm sm:text-base">Users</a>
          <NavLink to="/Login" href="login" className="block sm:hidden text-sm sm:text-base">Login</NavLink>
        </div>
</div>
    </>
  )
}

export default MobileNavbar
