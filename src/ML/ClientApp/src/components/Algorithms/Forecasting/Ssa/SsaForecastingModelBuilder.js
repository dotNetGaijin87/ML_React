import React, { useState } from 'react';
import axios from "axios";
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

import { CustomLineChart } from '../../../Common/Charts/CustomLineChart.js';
import { Notification } from '../../../Common/Notification.js';
import { TrainingDataSelection } from './TrainingDataSelection.js';
import { SsaOptions } from './Options/SsaOptions.js';
import { getErrorsFromException } from '../../../Common/Functions/Communication.js';


export function SsaForecastingModelBuilder() {

    const [trainingData, setTrainingData] = useState({});
    const [modelName, setModelName] = useState('Forecasting_Ssa_model');
    const [selectedModel, setModelType] = useState('ssa');
    const [forecastedData, setForecastedData] = useState({});
    const [ssaOptions, setSsaOptions] = useState();
    const [isValidationDataChartOpen, showHideValidationDataChart] = useState(false);
    const [isProgressCircleOpen, showHideProgressCircle] = useState(false);
    const [isNotificationShown, showHideNotification] = useState(false);
    const [notificationMessage, setNotificationMessage] = useState('');
    const [notificationSeverity, setNotificationSeverity] = useState('');
    const [isModelNameLengthOutOfBounds, setResetModelNameLengthOutOfBounds] = useState(false);
    const [hasChartCommonYaxis, toggleHasChartCommonYaxis] = useState(false);
 
    const MODEL_CATEGORY_NAME = 'forecasting';
 
    const getBuilderOptions = () => {

        let options = {
            modelName: modelName,
            trainingDataName: trainingData.fileName,
            inputColumnName: trainingData.featureColumnNames[0],
        };

        let nameLowercase = selectedModel.toLowerCase();
        if (nameLowercase === 'ssa') {
            options.trainSize = ssaOptions.trainSize;
            options.seriesLength = ssaOptions.seriesLength;
            options.confidenceLevel = ssaOptions.confidenceLevel;
            options.windowSize = ssaOptions.windowSize;
            options.horizon = ssaOptions.horizon;
        }

        return options;
    }

    const buildModel = async () => {

        if (ssaOptions.hasValidData) {
            setNotification('error', 'Invalid parameters');
            return;
        }

        showHideProgressCircle(true);
        showHideValidationDataChart(false);

        try {
            let options = getBuilderOptions();
            let modelMetricsResp = await axios.post(`${MODEL_CATEGORY_NAME}/${selectedModel}`, options);

            setForecastedData({
                data: [modelMetricsResp.data.forecast],
                label: ['Forecast'],
                labels: modelMetricsResp.data.forecast.map((val, index) => index)
            });
            showHideValidationDataChart(true);
            setNotification('success', 'Model built successfully');

        } catch (ex) {
            let errors = getErrorsFromException(ex);
            setNotification('error', errors);
        }

        showHideProgressCircle(false);
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

    const handleOnClickHasChartCommonYaxis = (event) => {
        if (hasChartCommonYaxis) {
            toggleHasChartCommonYaxis(false);
            setForecastedData({
                hasCommonYaxis: false
            });
        }
        else {
            toggleHasChartCommonYaxis(true);
            setForecastedData({
                hasCommonYaxis: true
            });
        }
    }

    return (
        <div>
            <h3 id="tabelLabel">Forecasting</h3>
            <Grid container direction="column" justifyContent="flex-start" alignItems="stretch" >
                <h4>Algorithms</h4>
                <TabContext value={selectedModel}>
                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <TabList onChange={(event, newValue) => setModelType(newValue) } aria-label="regression algorithms tabs">
                            <Tab label="Ssa" value='ssa' />
                        </TabList>
                    </Box>
                    <TabPanel value='ssa'>
                        <SsaOptions
                            parentCallback={(data) => setSsaOptions(data)}>
                        </SsaOptions>
                    </TabPanel>
                    <div style={{ marginLeft: 25 }}>
                        <p >Model Name</p>
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
                            variant="filled">
                        </TextField>
                        <p >Data Selection</p>
                        <TrainingDataSelection variant="filled"
                            modelCategoryName={`${MODEL_CATEGORY_NAME}`}
                            targetModelType={selectedModel}
                            parentCallback={(data) => setTrainingData(data)}>
                        </TrainingDataSelection>
                        <Grid container justifyContent="center" alignItems="center">
                            <Grid item xs={6}>
                                <Button variant="contained" size="medium" color="secondary"
                                    onClick={buildModel}>Build model
                                </Button>
                            </Grid>
                            <Grid item xs={6}>
                                {isProgressCircleOpen && <CircularProgress color="secondary" />}
                            </Grid>
                        </Grid>
                    </div>
                </TabContext>
            </Grid>
            {
                isValidationDataChartOpen &&

                <div className="custom-form-control">
                    <FormControlLabel
                        control={
                            <Switch
                                checked={hasChartCommonYaxis}
                                color="primary"
                                onClick={handleOnClickHasChartCommonYaxis}>
                            </Switch>
                        }
                        label="Common Y axis">
                    </FormControlLabel>
                </div> &&
                <Grid container justifyContent="center" alignItems="center">
                        <Grid item xs={12}>
                            <h4>Model Forecasts</h4>
                        </Grid>

                    <div className="chart-container">
                        <CustomLineChart props={forecastedData}>

                        </CustomLineChart>
                    </div>
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