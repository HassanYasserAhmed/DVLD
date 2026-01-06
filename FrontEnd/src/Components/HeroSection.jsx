import React from "react";

const HeroSection = () => {
  return (
    <section className="bg-gray-100 py-12 sm:py-16 md:py-20 px-4 sm:px-6 md:px-12 text-center">
      <h2 className="text-2xl sm:text-3xl md:text-4xl font-bold mb-4">
        احصل على رخصتك بسهولة وسرعة
      </h2>
      <p className="text-sm sm:text-base md:text-lg mb-6">
        خدمة إلكترونية لتقديم، تجديد، ودفع رسوم رخص السيارات.
      </p>
      <button className="bg-blue-600 text-white text-sm sm:text-base md:text-lg px-6 sm:px-8 md:px-12 py-3 sm:py-4 md:py-5 rounded-lg hover:bg-blue-700 transition duration-300">
        ابدأ الآن
      </button>
    </section>
  );
};

export default HeroSection;
