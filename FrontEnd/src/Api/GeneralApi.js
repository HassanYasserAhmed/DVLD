export async function getCountriesApi() {
  const url = "https://localhost:7205/api/General/Countries";

  try {
    const response = await fetch(url, {
      method: "GET",
      headers: {
        "Accept": "*/*"
      }
    });
    const {success,message,data} = await response.json();

   if(success)
    return data;

  } catch (error) {
    console.error("Error fetching countries:", error.message);
    throw error;
  }
}