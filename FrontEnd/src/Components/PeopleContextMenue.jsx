const PeopleContextMenu = ({ menuPosition, onOptionClick, options = ["Edit", "Delete", "View"] }) => (
  <ul className="absolute bg-white shadow-lg rounded-md py-2 w-48 z-50" style={{ top: menuPosition.y, left: menuPosition.x }}>
    {options.map(option => (
      <li
        key={option}
        className="px-4 py-2 hover:bg-gray-100 cursor-pointer"
        onClick={() => onOptionClick(option)}
      >
        {option}
      </li>
    ))}
  </ul>
);
export default PeopleContextMenu;