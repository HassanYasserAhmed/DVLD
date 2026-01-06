import Navbar from "./Navbar";
import Footer from "./Footer";
import HomeBackground from "./HomeBackground";
const HomePage = () => {
  return (
    <div className="font-sans relative">
      <Navbar />
     <HomeBackground/>
      <Footer />
    </div>
  );
};

export default HomePage;
