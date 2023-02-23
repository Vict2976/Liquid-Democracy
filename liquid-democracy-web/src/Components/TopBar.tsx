import '../styling/TopBar.css';

export default function TopBar() {
  return (
    <div>
      <header className="Navbar">
        <div className="Toolbar">
          <div className="Logo">
            {" "}
            <span role="img" aria-label="logo">
            🗳️
            </span>{" "}
          </div>
          <div className="Title"> HomePage</div>
          <div>
            <button className="top-bar-button"> Create New Election </button>
          </div>
          <div>
            <button className="top-bar-button"> Log Out </button>
          </div>
        </div>
      </header>
      <div className="Toolbar" />
    </div>
  );
}