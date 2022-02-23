import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { TrainingData } from './components/TrainingData/TrainingData';
import { RegressionFastForestModelBuilder } from './components/Algorithms/Regression/RegressionModelBuilder';
import { RegressionPrediction }   from './components/Algorithms/Regression/RegressionPrediction';
import { SsaForecastingModelBuilder } from './components/Algorithms/Forecasting/Ssa/SsaForecastingModelBuilder';

import { ThemeProvider } from "@material-ui/core/styles";
import { Theme } from "../src/components/Common/Theme";



import '../src/css/custom.css'
import '../src/css/button.css'
import '../src/css/input.css'
import '../src/css/customDropzoneArea.css'
import '../src/css/materialUI.css'


export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
          <ThemeProvider theme={Theme}>
            <Layout>
                  <Route exact path='/' component={Home} />
                  <Route path='/algorithms/forecasting/ssa/model-builder' component={SsaForecastingModelBuilder} />
 
                  <Route path='/algorithms/regression/model-builder' component={RegressionFastForestModelBuilder} />
                  <Route path='/algorithms/regression/prediction' component={RegressionPrediction} />       
                <Route path='/training-data' component={TrainingData} />
            </Layout>
          </ThemeProvider>
    );
  }
}
