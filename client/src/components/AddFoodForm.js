import React from 'react';

const AddFoodForm = (props) => {
    return (
        <form>
            <label for="foodName">Food: </label>
            <input type="text" id="foodName" name="foodName"/>
            <label for="pickUpTime">Time for pick up: </label>
            <input type="text" id="pickUpTime" name="pickUpTime"/>
            <label for="photo">Photo: </label>
            <input type="text" id="photo" name="photo" placeholder="Write an image URL"/>
            <button onClick={props.updateFood}>Submit</button>
        </form>
    )
}

export default AddFoodForm;