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

  // takeRest = () => {
  //   let restName = document.getElementById("takeRest").value;
  //   fetch("/Restaurants", {

  //   })
  // }

  saveRest = (id) => {
      let rest_info = {};
      for (let i = 0; i < this.state.restaurants.length; i++) {
          if (this.state.restaurants[i].id === id) {
              rest_info = this.state.restaurants[i];
          }
      }

      let saved_rest = {
              name: rest_info.name,
              address: rest_info.address,
              geoTag: rest_info.geoTag,
              id: rest_info.id
      }

      this.setState({
        restaurants: [saved_rest],
        callSent: false
      })


      fetch(`http://34.66.77.56/api/Restaurants`, {
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
          },
          method: "POST",
          body: JSON.stringify({ saved_rest })
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
          {this.state.restaurants.map(r => {
                            return (
                                <Restaurant
                                    key={r.id}
                                    restName={r.restName}
                                    address={r.address}
                                    karmaScore={r.karmaScore}
                                    geoTag={r.geoTag}
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
          <AddFoodForm />
        </div>
      )
    }
  }
}

export default Landing;