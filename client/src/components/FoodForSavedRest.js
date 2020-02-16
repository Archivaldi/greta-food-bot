import React from 'react';
const FoodForSavedRest = (props) => {
    return (
        <div class="row">
            <div class="col s12 m3">
                <div class="card">
                    <div class="card-image waves-effect waves-block waves-light">
                        <img class="activator" src={props.food_info.imageUrl} />
                    </div>
                    <div class="card-content">
                        <span class="card-title activator grey-text text-darken-4">{props.food_info.name}<i class="material-icons right">more_vert</i></span>
                        <p><a href="#">{props.food_info.availableTime}</a></p>
                    </div>
                    <div class="card-reveal">
                        <span class="card-title grey-text text-darken-4">{props.food_info.name}<i class="material-icons right">close</i></span>
                        <input type="text" placeholder="PROMOCODE" id="PROMO" />
                        <button onClick={props.enterPromo}>Submit</button>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default FoodForSavedRest;