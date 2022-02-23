import React, { useState, useEffect } from 'react';
import axios from "axios";
import Papa from 'papaparse';
import Button from '@material-ui/core/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import Grid from '@material-ui/core/Grid';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';
import { SingleVariableBarChart } from '../../Common/Charts/SingleVariableBarChart.js';
import { SingleVariableLineChart } from '../../Common/Charts/SingleVariableLineChart.js';
import { CustomLineChart } from '../../Common/Charts/CustomLineChart.js';
import { CsvFileDropZone } from '../../Common/Controls/CsvFileDropZone.js';
import { CustomSelect } from '../../Common/Controls/CustomSelect.js';
import { CustomMultipleSelect } from '../../Common/Controls/CustomMultipleSelect.js';
import { Notification } from '../../Common/Notification.js';
import { getErrorsFromException } from '../../Common/Functions/Communication.js';


export function RegressionPrediction() {

    const [modelName, setModelName] = useState();
    const [models, setModelList] = useState([]);
    const [selectedAlgorithm, setModelType] = useState();
    const [multiplePredictions, setMultiplePredictions] = useState([]);
    const [predictions, setPredictions] = useState([]);
    const [testDataColumns, setTestDataColumns] = useState([]);
    const [selectedTestDataColumn, selectTestDataColumn] = useState('');
    const [testDataFile, setTestDataFile] = useState({});
    const [parsedTestData, setParsedTestData] = useState({});
    const [modelPrediction, setModelPrediction] = useState({});
    const [featureContributions, setFeatureContributions] = useState([]);
    const [columnNames, setColumnNames] = useState([]);
    const [isProgressCircleOpen, showHideProgressCircle] = useState(false);
    const [isNotificationShown, showHideNotification] = useState(false);
    const [isValidationDataChartOpen, showHideValidationDataChartOpen] = useState(false);
    const [isPredictionDataChartOpen, showHidePredictionChartOpen] = useState(false);
    const [modelTypes, setModelTypes] = useState([]);
    const [modelToUrl, setModelToUrl] = useState([]);
    const [notificationMessage, setNotificationMessage] = useState('');
    const [notificationSeverity, setNotificationSeverity] = useState('');
    const [hasChartCommonYaxis, toggleHasChartCommonYaxis] = useState(false);



    useEffect(() => {
        getAlgoritms();
    }, []);

    useEffect(() => {
        if (selectedAlgorithm) {
            (async () => {
                getModels();
            })()
        }
    }, [selectedAlgorithm])




    // Called only once to get the list of algorithms
    const getAlgoritms = async () => {
        try {
            let result = await axios.get("/info/category/regression");
            setModelToUrl(result.data['regression']);
            setModelTypes(result.data['regression'].map(elem => elem.modelType));

        } catch (ex) {
            let errors = getErrorsFromException(ex);
            setNotification('error', errors);
        }
    };

    // Retrives a model list for the selected algorithm
    const getModels = async () => {
        try {
            let url = modelToUrl.find(elem => elem.modelType == selectedAlgorithm).url ;
            let resp = await axios.get(`regression/${url}`);
            setModelList(resp.data);
        } catch (ex) {
            let errors = getErrorsFromException(ex);
            setNotification('error', errors);
        }
    }


    // Runs a prediction engine for a single row of data
    const runSinglePrediction = async () => {
        try {
            let parsedData = await parseCsvFile(testDataFile);
            showHideProgressCircle(true);
            showHideValidationDataChartOpen(false);
            sendModelData(parsedData[parsedData.length - 1]); // Only last line is taken into account 
        } catch (ex) {
            showHideProgressCircle(false);
            showHideValidationDataChartOpen(false);
            setNotification('error', ex.message);
        }
    }

    const parseCsvFile = async (csvData) => {
        return new Promise(resolve => {
            Papa.parse(csvData, {
                header: true,
                skipEmptyLines: true,
                complete: results => {
                    resolve(results.data);
                }
            });
        });
    };


    const runSinglePredictionFromMultiple = async (index) => {
        try {
            let parsedData = parsedTestData[index];
            showHideProgressCircle(true);
            showHideValidationDataChartOpen(false);
            let inputModel = {
                modelName: modelName,
                ModelTypeName: selectedAlgorithm,
                Data: parsedData,
            };
            let url = modelToUrl.find(elem => elem.modelType == selectedAlgorithm).url;
            let resp = await axios.post(`regression/${url}:RunSinglePrediction`, inputModel)
            setModelPrediction([{ 'value': resp.data.score, 'name': 'score' }]);


            let dataNew = filterContributions(resp.data.featureContributions, columnNames, resp.data.contributingFeatureIndexes);
            setFeatureContributions(dataNew);

            showHideValidationDataChartOpen(true);
            setNotification('success', 'Model run successfully');
        } catch (ex) {
            setNotification('error', ex.message);
        }
        showHideProgressCircle(false);
    }




    const sendModelData = async (data) => {
        try {
            let inputModel = {
                modelName: modelName,
                ModelTypeName: selectedAlgorithm,
                Data: Object.keys(data).map(function (k) { return data[k]; }),
            };
            let url = modelToUrl.find(elem => elem.modelType == selectedAlgorithm).url;
            let resp = await axios.post(`regression/${url}:RunSinglePrediction`, inputModel)
 
            setModelPrediction([{ 'value': resp.data.score, 'name': 'score' }]);
            setFeatureContributions(filterContributions(resp.data.featureContributions, Object.keys(data).map(function (k) { return k; }), resp.data.contributingFeatureIndexes));
            showHideValidationDataChartOpen(true);
            setNotification('success', 'Model run successfully');

        } catch (ex) {
            let errors = getErrorsFromException(ex);
            setNotification('error', errors);
        }
        showHideProgressCircle(false);
    }

 

    /*
        Extracts feature contributions,
        filters them to include only important ones and modifies them to fit into linear chart
    */
    const filterContributions = (contributions, featureNames, indexes) => {
        let filteredContributions = [];
        for (var i = 0; i < contributions.length; i++) {
            let adjValue = contributions[i].toFixed(2);
            if (adjValue > 0.1 || adjValue < -0.1) {
                filteredContributions[i] = { 'value': adjValue, 'name': featureNames[indexes[i]] };
            }
        }

        return filteredContributions;
    }


    const runMultiplePredictions = async () => {
        try {
            let parsedData = await parseMultipleLinesCsvFile(testDataFile);
            setColumnNames(parsedData[0]);
            showHideProgressCircle(true);
            showHidePredictionChartOpen(false);
            sendMultipleModelData(parsedData);
        } catch (ex) {
            setNotification('error', ex.message);
        }
    }

    const parseMultipleLinesCsvFile = async (csvData) => {
        return new Promise(resolve => {
            Papa.parse(csvData, {
                header: false,
                skipEmptyLines: true,
                complete: results => {
                    resolve(results.data);
                    setParsedTestData(results.data);
                }
            });
        });
    };





    const sendMultipleModelData = async (data) => {
        try {
            showHidePredictionChartOpen(false);
            data.shift();
            let inputModel = {
                modelName: modelName,
                ModelTypeName: selectedAlgorithm,
                Data: data,
            };
            let url = modelToUrl.find(elem => elem.modelType == selectedAlgorithm).url;
            let result = await axios.post(`regression/${url}:RunMultiplePredictions`, inputModel)
            let parsedData = result.data.scores.map(x => x);
            setPredictions(parsedData);
            showHidePredictionChartOpen(true);
 
        } catch (ex) {
            let errors = getErrorsFromException(ex);
            setNotification('error', errors);
        }
        showHideProgressCircle(false);
    }

    const extractColumnNamesFromSelectedFile = async (file) => {
        var data = new Promise(resolve => {
            Papa.parse(file, {
                header: false,
                skipEmptyLines: true,
                complete: results => {
                    resolve(results.data);

                }
            });
        });

        var parsed = await data;
        setTestDataColumns(parsed[0]);
        setParsedTestData(parsed);
    }

    const getActualValuesFromTestData = async (selected) => {

        let lineHeaders = [];
        lineHeaders[0] = 'Prediction';
        var selectedColumnIndexes = [];

        for (var i = 0; i < selected.length; i++) {
            for (var j = 0; j < testDataColumns.length; j++) {
                if (selected[i] === testDataColumns[j]) {
                    lineHeaders.push(selected[i]);
                    selectedColumnIndexes.push(j);
                    break;
                }
            }
        }
        ;

        let chartActualData = [[]];
        chartActualData[0] = predictions;


        for (var j = 0; j < selectedColumnIndexes.length; j++) {
            let rowOfData = [];
            for (var i = 0; i < parsedTestData.length; i++) {
                var actualValue = parsedTestData[i][selectedColumnIndexes[j]];
                if (!isNaN(actualValue)) {
                    rowOfData.push(parseFloat(actualValue));
                } else {
                    rowOfData.push(0);
                }
            }
            chartActualData.push(rowOfData);
        }


        setMultiplePredictions({
            data: chartActualData,
            label: lineHeaders,
            labels: predictions.map((val, index) => index),
            hasCommonYaxis: hasChartCommonYaxis,
            parentCallback: index => runSinglePredictionFromMultiple(index)
        });
    
    }

    const handleOnClickHasChartCommonYaxis = (event) => {
        if (hasChartCommonYaxis) {
            toggleHasChartCommonYaxis(false);

        }
        else {
            toggleHasChartCommonYaxis(true);
        }
    }

    const setNotification = (severity, message) => {
        showHideNotification(true);
        setNotificationSeverity(severity);
        setNotificationMessage(message);
    }


      return (
          <div>
            <h3 id="tabelLabel">Regression Prediction</h3>
            <Grid container direction="column" justifyContent="flex-start" alignItems="stretch" >
                  <p>Input Parameters</p>
                  <CustomSelect
                      key={'Model Types'}
                      title={'Model Types'}
                      parentCallback={ (typeName) => { setModelType(typeName); } }
                      itemList={modelTypes}>
                  </CustomSelect>
                  <CustomSelect
                      key={'Model Name'}
                      title={'Model Name'}
                      parentCallback={(name) => setModelName(name)}
                      itemList={models}>
                  </CustomSelect>
                  <CsvFileDropZone
                      title={"Test Data"}
                      parentCallback={(file) => {
                            setTestDataFile(file);
                            extractColumnNamesFromSelectedFile(file);
                        }
                      }>
                  </CsvFileDropZone>
                  <div style={{ marginTop: 30 }} >
                      <p>Prediction Runner</p>
                      <Button variant="contained" size="medium" color="secondary"
                          onClick={runSinglePrediction}>Run Single
                    </Button>
                      <Button variant="contained" size="medium" color="secondary"
                          onClick={runMultiplePredictions}>Run Multiple
                    </Button>
                      {isProgressCircleOpen && <CircularProgress style={{ marginTop: 10 }}  color="secondary" />}
                    </div>

            </Grid>


              {
                  isPredictionDataChartOpen &&
                  <Grid container justifyContent="center" alignItems="center">
                      <Grid item xs={12}>
                          <h4>Prediction Chart</h4>
                          <CustomMultipleSelect
                              key={'Test Data Columns'}
                              title={'Test Data Columns'}
                              parentCallback={(selected) => {
                                  selectTestDataColumn(selected);
                                  getActualValuesFromTestData(selected);
                                }
                              }
                              itemList={testDataColumns}>
                          </CustomMultipleSelect>
                          <div >
                              <FormControlLabel style={{ marginLeft:'10px' }}
                                  control={
                                      <Switch
                                          checked={hasChartCommonYaxis}
                                          color="primary"
                                          onClick={handleOnClickHasChartCommonYaxis}>
                                      </Switch>
                                  }
                                  label="Common Y axis">
                              </FormControlLabel>
                          </div>
                      </Grid>
                      {
 

                          <div className="chart-container">
                              <CustomLineChart props={multiplePredictions}>
      
                              </CustomLineChart>
                          </div>
                      }
                  </Grid>
              }
              {
                featureContributions.length > 0 && isValidationDataChartOpen &&
                <Grid container justifyContent="center" alignItems="center">
                      <Grid item xs={12}>
                          <h4>Feature Contributions</h4>
                      </Grid>
                      {
                          <div className="chart-container">
                              <SingleVariableBarChart
                                  data={featureContributions}
                                  normalized={true}
                                  color={'#ff6347'}>
                              </SingleVariableBarChart>
                          </div>
                      }
                      {
                          modelPrediction.length > 0 &&
                          <TableContainer component={Paper} >
                              <Table width={1000} aria-label="prediction-table">
                                  <TableHead>
                                      <TableRow>
                                          <TableCell>Feature</TableCell>
                                          <TableCell>Score</TableCell>
                                      </TableRow>
                                  </TableHead>
                                  <TableBody>
                                      {featureContributions.sort((a, b) => Math.abs(b.value) - Math.abs(a.value)).map((row) => (
                                          <TableRow
                                              key={row.name}
                                              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                          >
                                              <TableCell component="th" >
                                                  {row.name}
                                              </TableCell>
                                              <TableCell component="th" >
                                                  {row.value}
                                              </TableCell>
                                          </TableRow>
                                      ))}
                                  </TableBody>
                              </Table>
                          </TableContainer>
                      }


                </Grid>
              }
              <Notification
                  open={isNotificationShown}
                  notificationSeverity={notificationSeverity}
                  notificationMessage={notificationMessage}
                  parentCallback={() => showHideNotification(false)}>
              </Notification>
        </div>
    );
}
