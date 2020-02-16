import React from 'react';
import FoodForSavedRest from './FoodForSavedRest';

const SavedRest = (props) => {
    return (
            <div>
                <div className="col s12 m7">
                    <h2 style={{ marginTop: 25 }} className="header">{props.rest_info.name}</h2>
                    <div className="card horizontal">
                        {/* <div className="card-image">
                            <img src={props.img} alt={props.title} />
                        </div> */}
                        <div className="card-stacked">
                            <div className="card-content">
                                {props.rest_info.foodEntities.map(food => {
                                    return (
                                        <FoodForSavedRest 
                                        key={food.id}
                                        food_info={food}
                                    />
                                    )

                                })}
                            </div>
                            <div className="card-action">
                                <p>Address: {props.rest_info.address} </p>
                                <p>Coordinates: {props.rest_info.geoTag} </p>
                                <p>Karma score: {props.rest_info.karmaScore} </p>
                                <a href = {`https://testnet.algoexplorer.io/address/${props.rest_info.algorandAddress}`} >Algorand: https://testnet.algoexplorer.io/address/{props.rest_info.algorandAddress}</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    )
}
export default SavedRest;