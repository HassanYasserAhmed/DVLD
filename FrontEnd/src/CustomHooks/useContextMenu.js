import { useState, useRef } from "react";

const useContextMenu = () => {
  const [showMenu, setShowMenu] = useState(false);
  const [menuPosition, setMenuPosition] = useState({ x: 0, y: 0 });
  const [selectedRow, setSelectedRow] = useState(null);

  const touchTimer = useRef(null);

  const handleOptionClick = (option) => {
    alert(`${option} clicked for ${selectedRow.name}`);
    setShowMenu(false);
  };
  const handleContextMenu = (event, user) => {
    event.preventDefault();
    setSelectedRow(user);
    setMenuPosition({ x: event.pageX, y: event.pageY });
    setShowMenu(true);
  };
  const handleClick = () => {
    setShowMenu(false);
    setSelectedRow(null);
  };
  const handleTouchStart = (event, user) => {
    touchTimer.current = setTimeout(() => {
      setSelectedRow(user);
      setMenuPosition({
        x: event.touches[0].pageX,
        y: event.touches[0].pageY,
      });
      setShowMenu(true);
    }, 500);
  };

  const handleTouchEnd = () => {
    clearTimeout(touchTimer.current);
  };

  const closeMenu = () => setShowMenu(false);

  return {
    showMenu,
    menuPosition,
    selectedRow,
    handleContextMenu,
    handleTouchStart,
    handleTouchEnd,
    handleOptionClick,
    handleClick,
    closeMenu,
  };
};

export default useContextMenu;
