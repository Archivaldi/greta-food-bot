import React from 'react';

const AddFoodForm = () => {
    return (
        <form>
            <label for="foodName">Food: </label>
            <input type="text" id="foodName" name="foodName"/>
            <label for="pickUpTime">Time for pick up: </label>
            <input type="text" id="pickUpTime" name="pickUpTime"/>
            <label for="quantity">Quantity: </label>
            <input type="text" id="quantity" name="quantity"/>
        </form>
    )
}

export default AddFoodForm;