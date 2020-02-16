import React from 'react';
import "./components/style.css"
import Header from './components/Header';
import SearchRest from './components/SearchRest';
import Footer from "./components/Footer";
import AddFoodForm from "./components/AddFoodForm";

class App extends React.Component {
  constructor() {
    super();
    this.state = {
      restorants: [],
      id: ""
    };
  }

  render() {
    if (this.state.restorants.length === 0 && this.state.id === "") {
      return (
        <div className="container">
          <Header />
          <SearchRest />
          <Footer />
        </div>
      )
    } else {
      return (
        <div className="container">
          <Header />
          <AddFoodForm />
          <Footer />
        </div>
      )
    }
  }
}

export default App;
