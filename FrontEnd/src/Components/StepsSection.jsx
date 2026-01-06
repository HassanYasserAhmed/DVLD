import React from "react";

const StepsSection = () => {
  return (
    <section className="bg-gray-50 py-12 sm:py-16 md:py-20 px-4 sm:px-6 md:px-12 text-center">
      <h2 className="text-2xl sm:text-3xl md:text-4xl font-bold mb-8 sm:mb-12">
        خطوات الحصول على رخصتك
      </h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6 sm:gap-8 md:gap-12">
        <div className="bg-white p-4 sm:p-6 md:p-8 rounded-lg shadow-md hover:shadow-lg transition duration-300">
          <h3 className="text-lg sm:text-xl md:text-2xl font-semibold mb-2">1. التسجيل</h3>
          <p className="text-sm sm:text-base md:text-lg">أنشئ حسابك وادخل بياناتك الأساسية.</p>
        </div>
        <div className="bg-white p-4 sm:p-6 md:p-8 rounded-lg shadow-md hover:shadow-lg transition duration-300">
          <h3 className="text-lg sm:text-xl md:text-2xl font-semibold mb-2">2. رفع المستندات</h3>
          <p className="text-sm sm:text-base md:text-lg">ارفع الأوراق المطلوبة بسهولة من جهازك.</p>
        </div>
        <div className="bg-white p-4 sm:p-6 md:p-8 rounded-lg shadow-md hover:shadow-lg transition duration-300">
          <h3 className="text-lg sm:text-xl md:text-2xl font-semibold mb-2">3. دفع الرسوم</h3>
          <p className="text-sm sm:text-base md:text-lg">ادفع الرسوم أونلاين واستلم إشعار الدفع.</p>
        </div>
      </div>
    </section>
  );
};

export default StepsSection;
