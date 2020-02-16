import React from "react";
const Header =() => {
        return (
            <nav>
                <div className="nav-wrapper">
                <a href="#" className="brand-logo">Greta-food-bot</a>
                <ul id="nav-mobile" className="right hide-on-med-and-down">
                    <li><a href="/">Add new restaurants</a></li>
                    <li><a href="/savedRestaurants">Saved restaurants</a></li>
                </ul>
                </div>
            </nav>
        )
    }

export default Header;