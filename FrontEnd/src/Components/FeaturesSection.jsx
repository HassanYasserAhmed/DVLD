import React from "react";

const FeaturesSection = () => {
  return (
    <section className="py-12 sm:py-16 md:py-20 px-4 sm:px-6 md:px-12 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6 sm:gap-8 md:gap-12 text-center">
      <div className="p-4 sm:p-6 md:p-8 bg-white shadow-md hover:shadow-lg rounded-lg transition duration-300">
        <h3 className="text-lg sm:text-xl md:text-2xl font-semibold mb-2">تقديم رخصة جديدة</h3>
        <p className="text-sm sm:text-base md:text-lg">قدّم طلب رخصتك الجديدة بكل سهولة وسرعة.</p>
      </div>
      <div className="p-4 sm:p-6 md:p-8 bg-white shadow-md hover:shadow-lg rounded-lg transition duration-300">
        <h3 className="text-lg sm:text-xl md:text-2xl font-semibold mb-2">تجديد رخصتك</h3>
        <p className="text-sm sm:text-base md:text-lg">جدد رخصتك بدون الذهاب لإدارة المرور.</p>
      </div>
      <div className="p-4 sm:p-6 md:p-8 bg-white shadow-md hover:shadow-lg rounded-lg transition duration-300">
        <h3 className="text-lg sm:text-xl md:text-2xl font-semibold mb-2">دفع الرسوم إلكترونيًا</h3>
        <p className="text-sm sm:text-base md:text-lg">ادفع رسوم الرخصة أونلاين بأمان وسهولة.</p>
      </div>
    </section>
  );
};

export default FeaturesSection;
