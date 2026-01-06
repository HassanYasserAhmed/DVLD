import React from "react";

const Footer = () => {
  return (
    <footer className="bg-blue-600 text-white py-4 sm:py-6 px-4 sm:px-8 text-center flex flex-col sm:flex-row justify-between items-center">
      <p className="text-sm sm:text-base mb-2 sm:mb-0">
        © 2025 CarLicense. جميع الحقوق محفوظة.
      </p>

      {/* مثال لو حبيت تضيف روابط */}
      <div className="flex space-x-4">
        <a href="#" className="hover:underline text-sm sm:text-base">Privacy</a>
        <a href="#" className="hover:underline text-sm sm:text-base">Terms</a>
        <a href="#" className="hover:underline text-sm sm:text-base">Contact</a>
      </div>
    </footer>
  );
};

export default Footer;
