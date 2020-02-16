import React from 'react';
import "./style.css"
import SearchRest from './SearchRest';
import AddFoodForm from "./AddFoodForm";
import Restaurant from "./Restaurant";

class Landing extends React.Component {
  constructor() {
    super();
    this.state = {
      restaurants: [],
      food: [],
      callSent: false,
      id: ""
    };
  }

  takeRest = () => {
    let restName = document.getElementById("restName").value;
    fetch("http://34.66.77.56/api/Map/" + restName)
        .then(r => r.json())
        .then(restaurants => {
            this.setState({
                restaurants: restaurants,
                callSent: true
            })
        })
  }

  updateFood = () => {
    let name = document.getElementById("foodName").value;
    let time = document.getElementById("pickUpTime").value;
    let image = document.getElementById("photo").value;
    let restId = this.state.restaurants[0].id;

    let food_info = {
        name: name,
        ImageUrl: image,
        availableTime: time,
        restaurantId: restId
    }
    fetch("http://34.66.77.56/api/Food", {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: "POST",
        body: JSON.stringify( food_info )
    })
  }


  saveRest = (id) => {
      let rest_info = {};
      for (let i = 0; i < this.state.restaurants.results.length; i++) {
          if (this.state.restaurants.results[i].id === id) {
              rest_info = this.state.restaurants.results[i];
          }
      }

      let saved_rest = {
              name: rest_info.poi.name,
              address: rest_info.address.freeformAddress,
              geoTag: rest_info.position.lat + "," + rest_info.position.lon,
              id: rest_info.id
      }


      fetch(`http://34.66.77.56/api/Restaurants`, {
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
          },
          method: "POST",
          body: JSON.stringify( saved_rest )
      })
            .then(r => {
                return r.json()
            })
            .then(json => {
                let id = json.id;
                saved_rest.id = id;
            })
      
            

    
    this.setState({
        restaurants: [saved_rest],
        callSent: false
    })
  }

  render() {
    if (this.state.restaurants.length === 0 && this.state.callSent === false) {
      return (
        <div className="container">
          <SearchRest takeRest={() => this.takeRest()}/>
        </div>
      )
    } else if (this.state.restaurants !== 0 && this.state.callSent === true) {
      return (
        <div className="container">
          {this.state.restaurants.results.map(r => {
                            return (
                                <Restaurant
                                    key={r.id}
                                    id = {r.id}
                                    restName={r.poi.name}
                                    address={r.address.freeformAddress}
                                    karmaScore={r.karmaScore}
                                    geoTag={r.position.lat + "," + r.position.lon}
                                    saveRest={()=> this.saveRest(r.id)}
                                />
                            )
                        })
                        
          }
        </div>
      )
    } else if (this.state.restaurants.length === 1){
      return (
        <div>
          <AddFoodForm updateFood={() => this.updateFood()}/>
        </div>
      )
    }
  }
}

export default Landing;