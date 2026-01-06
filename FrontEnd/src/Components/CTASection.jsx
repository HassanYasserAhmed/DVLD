import React from "react";

const CTASection = () => {
  return (
    <section className="py-16 sm:py-20 px-4 sm:px-8 text-center bg-gray-50">
      <h2 className="text-2xl sm:text-3xl md:text-4xl font-bold mb-6">
        جاهز للحصول على رخصتك الآن؟
      </h2>
      <button className="bg-blue-600 text-white text-sm sm:text-base md:text-lg px-6 sm:px-8 md:px-12 py-3 sm:py-4 md:py-5 rounded-lg hover:bg-blue-700 transition duration-300">
        سجل وابدأ الآن
      </button>
    </section>
  );
};
export default CTASection;
