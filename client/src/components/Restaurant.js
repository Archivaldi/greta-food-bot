import React from "react";
const Restaurant = (props) => {
    return (
        <div>
                <div className="col s12 m7">
                    <h2 style={{ marginTop: 25 }} className="header">{props.restName}</h2>
                    <div className="card horizontal">
                        <div className="card-stacked">
                            <div className="card-content">
                                <p>{props.address}</p>
                                <br />
                                <br />
                                <p className="right">{props.karmaScore}</p>
                            </div>
                            <div className="card-action">
                                <p> {props.geoTag} </p>
                                <p>
                                    <button onClick={props.saveRest} style={{ backgroundColor: "#1387ff", display: "inline-block" }} className="btn-floating btn-large waves-effect waves-light right">Save</button>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    )
}

export default Restaurant;