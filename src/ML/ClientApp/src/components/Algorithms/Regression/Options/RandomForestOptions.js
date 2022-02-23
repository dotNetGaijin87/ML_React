import React, { useState, useEffect } from 'react';
import FormControl from '@material-ui/core/FormControl';
import TextField from '@material-ui/core/TextField';


export function RandomForestOptions({ parentCallback }) {

    const [treesCount, setTreesCount] = useState(100);
    const [leavesCount, setLeavesCount] = useState(10);
    const [minExampleCountPerLeaf, setMinExampleCountPerLeaf] = useState(10);
    const [isTreesCountOutOfBounds, setTreesCountOutOfBounds] = useState();
    const [isLeavesCountOutOfBounds, setLeavesCountOutOfBounds] = useState();
    const [isMinExampleCountPerLeafOutOfBounds, setMinExampleCountPerLeafOutOfBounds] = useState();


    useEffect(() => {
        if (isTreesCountOutOfBounds || isLeavesCountOutOfBounds || isMinExampleCountPerLeafOutOfBounds) {
            return;
        }

        parentCallback({
            treesCount: treesCount,
            leavesCount: leavesCount,
            minExampleCountPerLeaf: minExampleCountPerLeaf,
        });

    }, [treesCount, leavesCount, minExampleCountPerLeaf])

    const handleOnChangeTreesCount = (event) => {
        let treesCnt = event.target.value;
        setTreesCount(treesCnt);
        if (treesCnt < 100 || treesCnt > 10000) {
            setTreesCountOutOfBounds(true);
        }
        else {
            setTreesCountOutOfBounds(false);
        }
    }

    const handleOnChangeLeavesCount = (event) => {
        let leavesCount = event.target.value;
        setLeavesCount(leavesCount);
        if (leavesCount < 10 || leavesCount > 1000) {
            setLeavesCountOutOfBounds(true);
        }
        else {
            setLeavesCountOutOfBounds(false);
        }
    }

    const handleOnChangeMinExampleCountPerLeaf = (event) => {
        let minExampleCountPerLeaf = event.target.value;
        setMinExampleCountPerLeaf(minExampleCountPerLeaf);
        if (minExampleCountPerLeaf < 2 || minExampleCountPerLeaf > 100) {
            setMinExampleCountPerLeafOutOfBounds(true);
        }
        else {
            setMinExampleCountPerLeafOutOfBounds(false);
        }
    }


    return (
        <div>
            <p>Parameters</p>
            <FormControl variant="filled">
                <TextField
                    value={treesCount}
                    error={isTreesCountOutOfBounds}
                    helperText={
                        isTreesCountOutOfBounds === true
                            ? 'Allowable range for trees count is between 100 and 10000'
                            : ' '
                    }
                    onChange={handleOnChangeTreesCount}
                    type="number"
                    id="leaves-count"
                    label="Trees Count"
                    variant="filled" />
                <TextField
                    value={leavesCount}
                    error={isLeavesCountOutOfBounds}
                    helperText={
                        isLeavesCountOutOfBounds === true
                            ? 'Allowable range for leaves count is between 10 and 1000'
                            : ' '
                    }
                    onChange={handleOnChangeLeavesCount}
                    type="number"
                    id="leaves-count"
                    label="Leaves Count"
                    variant="filled" />
                <TextField
                    value={minExampleCountPerLeaf}
                    error={isMinExampleCountPerLeafOutOfBounds}
                    helperText={
                        isMinExampleCountPerLeafOutOfBounds === true
                            ? 'Allowable range for minimum example count per leaf is between 2 and 100'
                            : ' '
                    }
                    onChange={handleOnChangeMinExampleCountPerLeaf}
                    type="number"
                    id="min-leaves-count-per-leaf"
                    label="Min Example Per Leaf"
                    variant="filled" />
            </FormControl>
            </div>

    );
}
