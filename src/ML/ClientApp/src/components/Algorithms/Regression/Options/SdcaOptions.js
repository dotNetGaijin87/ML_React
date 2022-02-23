import React, { useState, useEffect } from 'react';
import FormControl from '@material-ui/core/FormControl';
import TextField from '@material-ui/core/TextField';


export function SdcaOptions({ parentCallback }) {

    const [biasLearningRate, setBiasLearningRate] = useState(0.01);
    const [convergenceTolerance, setConvergenceTolerance] = useState(0.02);
    const [maximumNumberOfIterations, setMaximumNumberOfIterations] = useState(20);
    const [isBiasLearningRateOutOfBounds, setBiasLearningRateOutOfBounds] = useState();
    const [isConvergenceToleranceOutOfBounds, setConvergenceToleranceOutOfBounds] = useState();
    const [isMaximumNumberOfIterationsOutOfBounds, setMaximumNumberOfIterationsOutOfBounds] = useState();


    useEffect(() => {
        if (isBiasLearningRateOutOfBounds || isConvergenceToleranceOutOfBounds || isMaximumNumberOfIterationsOutOfBounds) {
            return;
        }

        parentCallback({
            biasLearningRate: biasLearningRate,
            convergenceTolerance: convergenceTolerance,
            maximumNumberOfIterations: maximumNumberOfIterations,
        });

    }, [biasLearningRate, convergenceTolerance, maximumNumberOfIterations])


    const handleChangeBiasLearningRate = (event) => {
        let biasLearningRate = event.target.value;
        setBiasLearningRate(biasLearningRate);
        if (biasLearningRate < 0.0001 || biasLearningRate > 10) {
            setBiasLearningRateOutOfBounds(true);
        }
        else {
            setBiasLearningRateOutOfBounds(false);
        }
    }


    const handleChangeConvergenceTolerance = (event) => {
        let convergenceTolerance = event.target.value;
        setConvergenceTolerance(convergenceTolerance);
        if (convergenceTolerance < 0.00001 || convergenceTolerance > 100) {
            setConvergenceToleranceOutOfBounds(true);
        }
        else {
            setConvergenceToleranceOutOfBounds(false);
        }
    }

    const handleChangeMaximumNumberOfIterations = (event) => {
        let maximumNumberOfIterations = event.target.value;
        setMaximumNumberOfIterations(maximumNumberOfIterations);
        if (maximumNumberOfIterations < 1 || maximumNumberOfIterations > 200) {
            setMaximumNumberOfIterationsOutOfBounds(true);
        }
        else {
            setMaximumNumberOfIterationsOutOfBounds(false);
        }
    }


    return (
        <div>
            <p>Parameters</p>
            <FormControl variant="filled">
                <TextField
                    value={biasLearningRate}
                    error={isBiasLearningRateOutOfBounds}
                    helperText={
                        isBiasLearningRateOutOfBounds === true
                            ? 'Allowable range for trees count is between 0.0001 and 10'
                            : ' '
                    }
                    onChange={handleChangeBiasLearningRate}
                    type="number"
                    id="bias-learning-rate"
                    label="Bias Learning Rate"
                    variant="filled" />
                <TextField
                    value={convergenceTolerance}
                    error={isConvergenceToleranceOutOfBounds}
                    helperText={
                        isConvergenceToleranceOutOfBounds === true
                            ? 'Allowable range for leaves count is between 0.00001 and 100'
                            : ' '
                    }
                    onChange={handleChangeConvergenceTolerance}
                    type="number"
                    id="covergance-tolerance"
                    label='Covergance Tolerance'
                    variant="filled" />
                <TextField
                    value={maximumNumberOfIterations}
                    error={isMaximumNumberOfIterationsOutOfBounds}
                    helperText={
                        isMaximumNumberOfIterationsOutOfBounds === true
                            ? 'Allowable range for minimum example count per leaf is between 1 and 1000'
                            : ' '
                    }
                    onChange={handleChangeMaximumNumberOfIterations}
                    type="number"
                    id="max-num-of-iterations"
                    label="Max Number of Iterations"
                    variant="filled" />
            </FormControl>
            </div>

    );
}
