
export async function validateEmailApi(email) {
  try {
    const response = await fetch("https://localhost:7205/api/People/Validations/Email", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Accept": "*/*"
      },
      body: JSON.stringify(email) // مهم تحوله ل JSON
    });

    const {success,message} = await response.json(); // افترض إن الـ API بيرجع JSON
    return {success,message}; // ممكن يكون true/false أو رسالة من السيرفر
  } catch (error) {
    console.error("Error validating email:", error);
    return null;
  }
}

export async function validateNationalNumberApi(email) {
  try {
    const response = await fetch("https://localhost:7205/api/People/Validations/NationalNumber", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Accept": "*/*"
      },
      body: JSON.stringify(email) // مهم تحوله ل JSON
    });

    const {success,message} = await response.json(); // افترض إن الـ API بيرجع JSON
    return {success,message}; // ممكن يكون true/false أو رسالة من السيرفر
  } catch (error) {
    console.error("Error validating National Number:", error);
    return null;
  }
}

export async function validatePhoneApi(phone) {
  try {
    const response = await fetch("https://localhost:7205/api/People/Validations/Phone", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Accept": "*/*"
      },
      body: JSON.stringify(phone) // مهم تحوله ل JSON
    });

    const {success,message} = await response.json(); // افترض إن الـ API بيرجع JSON
    return {success,message}; // ممكن يكون true/false أو رسالة من السيرفر
  } catch (error) {
    console.error("Error validating Phone:", error);
    return null;
  }
}