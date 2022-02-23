import React, { useState, useEffect } from 'react';
import Button from '@material-ui/core/Button';
import CircularProgress from '@material-ui/core/CircularProgress';
import FormControl from '@material-ui/core/FormControl';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Grid from '@material-ui/core/Grid';
import Slider from '@material-ui/core/Slider';
import Switch from '@material-ui/core/Switch';
import TextField from '@material-ui/core/TextField';
import Typography from '@material-ui/core/Typography';
import TabContext from '@mui/lab/TabContext';
import TabList from '@mui/lab/TabList';
import TabPanel from '@mui/lab/TabPanel';
import Box from '@mui/material/Box';
import Tab from '@mui/material/Tab';
import axios from "axios";
import { SingleVariableBarChart } from '../../Common/Charts/SingleVariableBarChart.js';
import { CustomLineChart } from '../../Common/Charts/CustomLineChart.js';
import { Notification } from '../../Common/Notification.js';
import { RandomForestOptions } from './Options/RandomForestOptions.js';
import { SdcaOptions } from './Options/SdcaOptions.js';
import { TrainingDataSelection } from './TrainingDataSelection.js';
import { getErrorsFromException } from '../../Common/Functions/Communication.js';



export function RegressionFastForestModelBuilder() {

    const [trainingData, setTrainingData] = useState({});
    const [modelName, setModelName] = useState('SampleModel_v1');
    const [selectedModel, setModelType] = useState('FastForest');
    const [models, setModels] = useState([]);
    const [modelToUrl, setModelToUrl] = useState([]);
    const [crossValidationFoldsCount, setCrossValidationFoldsCount] = useState(5);
    const [hasFeatureContributionMetrics, setFeatureContributionMetrics] = useState(true);
    const [crossValidationResults, setCrossValidationResults] = useState([]);
    const [featureImportanceList, setFeatureImportanceList] = useState([]);
    const [fastForestOptions, setFastForestOptions] = useState();
    const [sdcaOptions, setSdcaOptions] = useState();
    const [fastTreeOptions, setFastTreeOptions] = useState();
    const [isValidationDataChartOpen, showHideValidationDataChart] = useState(false);
    const [isProgressCircleOpen, showHideProgressCircle] = useState(false);
    const [isNotificationShown, showHideNotification] = useState(false);
    const [notificationMessage, setNotificationMessage] = useState('');
    const [notificationSeverity, setNotificationSeverity] = useState('');
    const [isModelNameLengthOutOfBounds, setResetModelNameLengthOutOfBounds] = useState(false);

    const MODEL_CATEGORY_NAME = 'regression';

    useEffect(() => {
        (async () => {
            getAvailableModelTypes();
        })()

    }, [])

 
    const getBuilderOptions = () => {

        let options = {
            modelName: modelName,
            trainingDataName: trainingData.fileName,
            labelColumnName: trainingData.labelColumnName,
            featureColumnNames: trainingData.featureColumnNames,
            crossValidationFoldsCount: crossValidationFoldsCount,
            hasFeatureContributionMetrics: hasFeatureContributionMetrics,
        };

 
        if (selectedModel === 'FastForest') {
            options.treesCount = fastForestOptions.treesCount;
            options.leavesCount = fastForestOptions.leavesCount;
            options.minimumExampleCountPerLeaf = fastForestOptions.minExampleCountPerLeaf;
        } else if (selectedModel === 'Sdca') {
            options.biasLearningRate = sdcaOptions.biasLearningRate;
            options.convergenceTolerance = sdcaOptions.convergenceTolerance;
            options.maximumNumberOfIterations = sdcaOptions.maximumNumberOfIterations;
        } else if (selectedModel === 'FastTree') {
            options.treesCount = fastTreeOptions.treesCount;
            options.leavesCount = fastTreeOptions.leavesCount;
            options.minimumExampleCountPerLeaf = fastTreeOptions.minExampleCountPerLeaf;
        }


        return options;
    }
 
    const getAvailableModelTypes= async () => {

        try {
            var result = await axios.get(`/info/category/${MODEL_CATEGORY_NAME}`);
            setModelToUrl(result.data[`${MODEL_CATEGORY_NAME}`]);
            setModels(result.data[`${MODEL_CATEGORY_NAME}`].map(elem => elem.modelType));

        } catch (ex) {
            let errors = getErrorsFromException(ex);
            setNotification('error', errors);
        }
    };


    const buildModel = async () => {
        showHideProgressCircle(true);
        showHideValidationDataChart(false);

        try {
            let options = getBuilderOptions();

            let modelMetricsResp = await axios.post(`${MODEL_CATEGORY_NAME}/${modelToUrl.find(element => element.modelType === selectedModel).url}`, options);

            showHideValidationDataChart(true);

            setCrossValidationResults(modelMetricsResp.data.ValidationResults.map((item, index) => {
                return {
                    value: item.toFixed(2),
                    name: `Result ${index}`
                }
            }));

            setFeatureImportanceList(modelMetricsResp.data.FeatureImportanceList.filter(item => Math.abs(item.toFixed(2)) > 0.01).map((item, index) => {
                return {
                    value: item.toFixed(2),
                    name: trainingData.allFeatureColumnNames[modelMetricsResp.data.ContributingFeatureIndexes[index]]
                }
            }));

            setNotification('success', 'Model built successfully');

        } catch (ex) {
            let errors = getErrorsFromException(ex);
            setNotification('error', errors);
        }

        showHideProgressCircle(false);
    }

    const handleOnClickHasFeatureContributionMetricsSwitch = (event) => {
        if (hasFeatureContributionMetrics) {
            setFeatureContributionMetrics(false);
        }
        else {
            setFeatureContributionMetrics(true);
        }
    }



    const handleOnChangeModelName = (event) => {
        let modelName = event.target.value;
        setModelName(modelName);
        let modelNameLength = modelName.length;
        if (modelNameLength < 3 || modelNameLength > 70) {
            setResetModelNameLengthOutOfBounds(true);
        }
        else {
            setResetModelNameLengthOutOfBounds(false);
        }
    }

    const setNotification = (severity, message) => {
        showHideNotification(true);
        setNotificationSeverity(severity);
        setNotificationMessage(message);
    }



    return (
        <div>
            <h3 id="tabelLabel">Regression</h3>
            <Grid container direction="column" justifyContent="flex-start" alignItems="stretch" >

                <TabContext value={selectedModel}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={(event, newValue) => setModelType(newValue) } aria-label="regression algorithms tabs">
                            <Tab label="Fast Forest" value='FastForest' />
                            <Tab label="Sdca" value='Sdca' />
                            <Tab label="Fast Tree" value='FastTree' />
                        </TabList>
                    </Box>
                    <TabPanel value='FastForest'>
                        <RandomForestOptions
                            parentCallback={(data) => setFastForestOptions(data)}>
                        </RandomForestOptions>
                    </TabPanel>
                    <TabPanel value='Sdca'>
                        <SdcaOptions
                            parentCallback={(data) => setSdcaOptions(data) }>
                        </SdcaOptions>            
                    </TabPanel>
                    <TabPanel value='FastTree'>
                        <RandomForestOptions
                            parentCallback={(data) => setFastTreeOptions(data) }>
                        </RandomForestOptions>
                    </TabPanel>
                    <div style={{ marginLeft: 25 }}>
                        <p >Model Name</p>
                        <FormControl variant="filled">
                            <TextField
                                value={modelName}
                                error={isModelNameLengthOutOfBounds}
                                helperText={
                                    isModelNameLengthOutOfBounds === true
                                        ? 'Allowable length for model name is between 3 and 70'
                                        : ' '
                                }
                                onChange={handleOnChangeModelName}
                                id="model-name"
                                label="Model Name"
                                variant="filled" />
                        </FormControl>
                        </div>
                    <TrainingDataSelection variant="filled"
                        modelCategoryName={`${MODEL_CATEGORY_NAME}`}
                        targetModelType={selectedModel}
                        parentCallback={(data) => setTrainingData(data)}>
                    </TrainingDataSelection>
                    <div >
                        <p style={{ marginLeft: 25, marginTop: 20 }}>Validation Parameters</p>
                        <div className="custom-form-control">
                            <Typography id="discrete-slider" gutterBottom>
                                Cross Validation Folds Count
                        </Typography>
                            <Slider
                                value={crossValidationFoldsCount}
                                aria-labelledby="discrete-slider"
                                valueLabelDisplay="on"
                                step={1}
                                onChange={(e, newValue) => setCrossValidationFoldsCount(newValue)}
                                marks
                                min={1}
                                max={40} />
                        </div>
                        <div className="custom-form-control">
                            <FormControlLabel
                                control={
                                    <Switch
                                        checked={hasFeatureContributionMetrics}
                                        color="primary"
                                        onClick={handleOnClickHasFeatureContributionMetricsSwitch}
                                    />
                                }
                                label="Include Feature Contribution Metrics"
                            />
                        </div>
                    </div>
                    <Grid container justifyContent="center"
                        alignItems="center" style={{ marginTop: 30 }} >
                        <Grid item xs={4}>
                            <Button variant="contained" size="medium" color="secondary"
                                onClick={buildModel}>Build model
                        </Button>
                        </Grid>
                        <Grid item xs={8}>
                            {isProgressCircleOpen && <CircularProgress color="secondary" />}
                        </Grid>

                    </Grid>
                </TabContext>
            </Grid>
            {
                isValidationDataChartOpen && 
                <Grid container justifyContent="center" alignItems="center">
                        <Grid item xs={12}>
                            <h4>Model Metrics</h4>
                        </Grid>
                    <div className="chart-container">
                        <h4>Cross Validation Results</h4>
                        <SingleVariableBarChart
                            key={crossValidationResults}
                            normalized={true}
                            color={'#FF6347'}
                            barHeader={'value'}
                            data={crossValidationResults }>
                        </SingleVariableBarChart>
                    </div>

                    <div className="chart-container" >
                        <h4>Feature Importance</h4>
                        <SingleVariableBarChart
                            key={featureImportanceList}
                            color={'#FF6347'}
                            barHeader={'name'}
                            data={featureImportanceList}>
                        </SingleVariableBarChart>
                    </div>
            </Grid>
            }

            <Notification
                open={isNotificationShown}
                notificationSeverity={notificationSeverity}
                notificationMessage={notificationMessage}
                parentCallback={() => showHideNotification(false)}/>
    </div>
    );
}