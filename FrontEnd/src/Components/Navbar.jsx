import { NavLink } from "react-router-dom";
import DesktopNavbar from "./DesktopNavbar";
import MobileNavbar from "./MobileNavbar";

const Navbar = () => {
  return (
<nav className="relative bg-blue-600 text-white px-4 py-4 flex items-center justify-between">
      <NavLink to="/" className="text-xl sm:text-2xl font-bold">DVLD</NavLink>
<MobileNavbar/>
   <DesktopNavbar/>
    </nav>
  );
};

export default Navbar;
