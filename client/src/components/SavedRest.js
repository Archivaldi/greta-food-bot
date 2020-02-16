import React from 'react';
const SavedRest = (props) => {
    return (
        <div>
                <div className="col s12 m7">
                    <h2 style={{ marginTop: 25 }} className="header">{props.name}</h2>
                    <div className="card horizontal">
                        <div className="card-stacked">
                            <div className="card-content">
                                <p>{props.address}</p>
                                <br />
                                <br />
                                <p className="right">{props.geoTag}</p>
                            </div>
                            <div className="card-action">
                                <p> {props.karmaScore} </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    )
}
export default SavedRest;