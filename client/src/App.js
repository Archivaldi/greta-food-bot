import React from 'react';
import { Route, BrowserRouter } from "react-router-dom";

import Header from './components/Header';
import Landing from "./components/Landing";
import SavedRests from "./components/SavedRests";
import Footer from "./components/Footer";

const App = () =>
  (

    <div>
      <BrowserRouter>
        <div className="container">
          <Header />
          <Route exact path="/" component={Landing} />
          <Route exact path="/savedRestaurants" component={SavedRests} />
          <Footer />
        </div>
      </BrowserRouter>
    </div>
  )


export default App