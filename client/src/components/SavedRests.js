import React from 'react';

import SavedRest from './SavedRest';
import AddFoodForm from './AddFoodForm';

class SavedRests extends React.Component {
    constructor(){
        super();
        this.state = {
            restaraunts: []
        }
    }
    componentDidMount(){
        fetch("http://34.66.77.56/api/Restaurants")
            .then(r => r.json())
            .then(restaraunts => this.setState({restaraunts}))
    }

    render(){

        if (this.state.restaraunts.length === 0){
            return (
                <div><h1 style={{fontSize: "50px", textAlign:"center"}}>You have no saved restaraunts</h1></div>
            )
        } else {
            return (
                <div>
                        <div className="center-align">
                            <h1>Saved Restaurants</h1>
                        </div>
                        {this.state.restaraunts.map(rest => {
                                return (
                                    <SavedRest
                                        key={rest.id}
                                        rest_info = {rest}
                                    />
                                )
                        }
    
                        )}
                    </div>
            )
        }
    }
}

export default SavedRests;