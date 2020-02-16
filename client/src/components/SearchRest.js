import React from "react";
const SearchRest = () => {
    return (
        <div>
            <form action="/mapsAPI" method="POST">
                <input type="text" placeholder="Search for a restaurant" name="restName"/>
                <button>Submit</button>
            </form>
        </div>
    )
}
export default SearchRest;