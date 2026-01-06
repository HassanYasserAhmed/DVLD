import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./Components/HomePage";
import LoginPage from "./Pages/LoginPage";
import PeoplePage from "./Pages/PeoplePage";
import TestPage from "./Pages/TestPage";
function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/Login" element={<LoginPage />} />
        <Route path="/People" element={<PeoplePage />} />
        <Route path="/test" element={<TestPage />} />
      </Routes>
    </Router>
  );
}
export default App;
