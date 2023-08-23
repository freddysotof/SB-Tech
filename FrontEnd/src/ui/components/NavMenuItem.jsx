import { NavLink } from "react-router-dom";
import { useUiStore } from "../../hooks";

export const NavMenuItem = ({ icon, path, onClick = () => { } }) => {
    const { closeSideBar } = useUiStore();
    const onClickItem = () => {
        onClick();
        closeSideBar()
    }
    return (<NavLink
        onClick={onClickItem}
        to={path}
    >
        {({ isActive, isPending }) => {
            return (
                <>
                    {icon}
                    <span className={isActive ? "active" : ""}></span>
                </>

            )
        }}
    </NavLink>
    );
}