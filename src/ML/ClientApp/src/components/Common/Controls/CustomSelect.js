import React, { useState, useEffect } from 'react';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import ListItemText from '@material-ui/core/ListItemText';


 
export function CustomSelect({ title, itemList, parentCallback }) {

    const [selected, setSelection] = useState();
 
    useEffect(() => {
        if (itemList?.length > 0) {
            if (selected != itemList[0]) {
                setSelection(itemList[0]);
                parentCallback(itemList[0]);
            }
        }
    }, [itemList])

    const handleOnChange = (event) => {
        setSelection(event.target.value);
        parentCallback(event.target.value);
    }

        return (
            <div style={{ marginRight: 27 }}>
                <FormControl variant="filled">
                    <InputLabel
                        label="Select"
                        value={selected}
                        id="input-label-select-outlined">
                        {title}
                    </InputLabel>
                    <Select
                        MenuProps={{
                            anchorOrigin: {
                                vertical: "bottom",
                                horizontal: "left"
                            },
                            transformOrigin: {
                                vertical: "top",
                                horizontal: "left"
                            },
                            transitionDuration: 700,
                            getContentAnchorEl: null,
                        }}

                        labelId="input-label-select-outlined"
                        id="select-outlined"
                        value={itemList?.includes(selected) ? selected : ''}
                        defaultValue=''
                        onChange={handleOnChange}
                        renderValue={(selected) => {
                            let renderedValue = Object.assign(selected);
                            if (renderedValue.length > 25) {
                                renderedValue = renderedValue.substring(0, 25).concat('', '...');
                            }

                            return renderedValue;
                        }}>
                        {
                            itemList?.map((name) => (
                            <MenuItem key={name} value={name}>
                                <ListItemText primary={name} />
                            </MenuItem>))
                        }
                    </Select>
                </FormControl>
            </div>
        )
}

