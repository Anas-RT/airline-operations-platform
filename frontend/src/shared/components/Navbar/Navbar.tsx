import { NavLink } from "react-router-dom";
import styles from "./Navbar.module.css";

export default function Navbar() {
  return (
    <>
      <div className={styles.projectTitle}>
        <p>
          Airline Operations <br />
          <span>Intelligence Platform</span>
        </p>
      </div>
      <nav className={styles.nav} aria-label="Main navigation">
        <p>Dashboard Stories</p>
        <ul>
          <li>
            <NavLink
              className={({ isActive }) => (isActive ? styles.active : "")}
              to="/network-overview"
            >
              <span>Network Overview</span>
            </NavLink>
          </li>
          <li>
            <NavLink
              className={({ isActive }) => (isActive ? styles.active : "")}
              to="/airlines"
            >
              <span>Airline Performance</span>
            </NavLink>
          </li>
          <li>
            <NavLink
              className={({ isActive }) => (isActive ? styles.active : "")}
              to="/delay-severity"
            >
              <span>Delay Severity</span>
            </NavLink>
          </li>
        </ul>
      </nav>
    </>
  );
}
