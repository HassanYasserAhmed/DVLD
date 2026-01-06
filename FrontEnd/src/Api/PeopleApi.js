export async function fetchPeopleApi(token) {
  try {
    const response = await fetch(
      `https://localhost:7205/api/People/GetAll?Token=${token}`,
      {
        method: "GET",
        headers: {
          "Accept": "application/json"
        }
      }
    );

    const {success,message,data} = await response.json();

    if (!success) {
      throw new Error(message || "Failed to fetch people");
    }

    return {success,message,data};

  } catch (error) {
    console.error("Fetch People Error:", error);
    return [];
  }
}

export async function AddPersonApi(token, personData) {
  const url = "https://localhost:7205/api/People";

  const payload = {
    token,
    personData
  };

  try {
    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Accept": "*/*",
        "Content-Type": "application/json"
      },
      body: JSON.stringify(payload)
    });

    if (!response.ok) {
      // لو الـ API رجّع status error
      const errorText = await response.text();
      throw new Error(errorText);
    }

    const data = await response.json();
    console.log("Success:", data);
    return data;

  } catch (error) {
    console.error("API Error:", error.message);
    throw error;
  }
}
