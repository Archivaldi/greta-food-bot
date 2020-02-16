import React from "react";
const SearchRest = (props) => {
    return (
        <div>
                <input type="text" placeholder="Search for a restaurant" id="restName" name="restName"/>
                <button onClick={props.takeRest}>Submit</button>
        </div>
    )
}
export default SearchRest;