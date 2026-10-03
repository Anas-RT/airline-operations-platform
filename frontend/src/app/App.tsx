import "./App.css";
import { Navbar } from "../shared/components";
import { Outlet } from "react-router-dom";

function App() {
  return (
    <div className="app-shell">
      <div className="nav-container">
        <Navbar />
      </div>
      <main className="main-content">
        <Outlet />
      </main>
    </div>
  );
}

export default App;
