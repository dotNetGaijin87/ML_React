import React, { useState, useEffect } from 'react';
import FormControl from '@material-ui/core/FormControl';
import TextField from '@material-ui/core/TextField';


export function SsaOptions({ parentCallback }) {

    const [trainSize, setTrainSize] = useState(100);
    const [seriesLength, setSeriesLength] = useState(20);
    const [confidenceLevel, setConfidenceLevel] = useState(0.95);
    const [windowSize, setWindowSize] = useState(10);
    const [horizon, setHorizon] = useState(5);

    const [isTrainSizeOutOfBounds, setTrainSizeOutOfBounds] = useState();
    const [isSeriesLengthOutOfBounds, setSeriesLengthOutOfBounds] = useState();
    const [isConfidenceLevelOutOfBounds, setConfidenceLevelOutOfBounds] = useState();
    const [isWindowSizeOutOfBounds, setWindowSizeOutOfBounds] = useState();
    const [isHorizonOutOfBounds, setHorizonOutOfBounds] = useState();



    useEffect(() => {
        parentCallback({
            trainSize: trainSize,
            seriesLength: seriesLength,
            confidenceLevel: confidenceLevel,
            windowSize: windowSize,
            horizon: horizon,
            hasValidData: false
        });

    }, [])

    useEffect(() => {
        if (isTrainSizeOutOfBounds          ||
            isSeriesLengthOutOfBounds       ||
            isConfidenceLevelOutOfBounds    ||
            isWindowSizeOutOfBounds         ||
            isHorizonOutOfBounds) {


            parentCallback({
                hasValidData: true
            });


            return;
        }

        parentCallback({
            trainSize: trainSize,
            seriesLength: seriesLength,
            confidenceLevel: confidenceLevel,
            windowSize: windowSize,
            horizon: horizon,
            hasValidData: false
        });

    }, [trainSize, seriesLength, confidenceLevel, windowSize, horizon])


    const handleChangeTrainSize = (event) => {
        let trainSize = event.target.value;
        setTrainSize(trainSize);
        if (trainSize < 0) {
            setTrainSizeOutOfBounds(true);
        }
        else {
            setTrainSizeOutOfBounds(false);
        }
    }


    const handleChangeSeriesLength = (event) => {
        let seriesLength = event.target.value;
        setSeriesLength(seriesLength);
        if (seriesLength < 0) {
            setSeriesLengthOutOfBounds(true);
        }
        else {
            setSeriesLengthOutOfBounds(false);
        }
    }

    const handleChangeConfidenceLevel = (event) => {
        let confidenceLevel = event.target.value;
        setConfidenceLevel(confidenceLevel);
        if (confidenceLevel < 0.0 || confidenceLevel > 1.0) {
            setConfidenceLevelOutOfBounds(true);
        }
        else {
            setConfidenceLevelOutOfBounds(false);
        }
    }

    const handleChangeWindowSize = (event) => {
        let windowSize = event.target.value;
        setWindowSize(windowSize);
        if (windowSize < 0) {
            setWindowSizeOutOfBounds(true);
        }
        else {
            setWindowSizeOutOfBounds(false);
        }
    }

    const handleChangeHorizon = (event) => {
        let horizon = event.target.value;
        setHorizon(horizon);
        if (horizon < 0) {
            setHorizonOutOfBounds(true);
        }
        else {
            setHorizonOutOfBounds(false);
        }
    }


    
    return (
        <FormControl variant="filled">
{/*            <h4>Parameters</h4>*/}
            <TextField
                value={trainSize}
                error={isTrainSizeOutOfBounds}
                helperText={
                    isTrainSizeOutOfBounds === true
                        ? 'Error'
                        : ' '
                }
                onChange={handleChangeTrainSize}
                type="number"
                id="train-size"
                label="Train Size"
                variant="filled" />
            <TextField
                value={seriesLength}
                error={isSeriesLengthOutOfBounds}
                helperText={
                    isSeriesLengthOutOfBounds === true
                        ? 'Error'
                        : ' '
                }
                onChange={handleChangeSeriesLength}
                type="number"
                id="series-length"
                label='Series Length'
                variant="filled" />
            <TextField
                value={confidenceLevel}
                error={isConfidenceLevelOutOfBounds}
                helperText={
                    isConfidenceLevelOutOfBounds === true
                        ? 'Error'
                        : ' '
                }
                onChange={handleChangeConfidenceLevel}
                type="number"
                id="confidence-level"
                label="Confidence Level"
                variant="filled" />
            <TextField
                value={windowSize}
                error={isWindowSizeOutOfBounds}
                helperText={
                    isWindowSizeOutOfBounds === true
                        ? 'Error'
                        : ' '
                }
                onChange={handleChangeWindowSize}
                type="number"
                id="window-size"
                label="Window Size"
                variant="filled" />
            <TextField
                value={horizon}
                error={isHorizonOutOfBounds}
                helperText={
                    isHorizonOutOfBounds === true
                        ? 'Error'
                        : ' '
                }
                onChange={handleChangeHorizon}
                type="number"
                id="window-size"
                label="Horizon"
                variant="filled" />
        </FormControl>
    );
}
