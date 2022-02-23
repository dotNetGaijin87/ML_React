import React, { useState, useEffect } from 'react';
import FormControl from '@material-ui/core/FormControl';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';


/* 
    Select component which includes checkboxes to allow multiple selection
*/
export function CustomMultipleSelect({ itemList, parentCallback }) {

    const [selectedItems, setSelectedItems] = useState([]);
    const [isSelectAllItemsCheckboxChecked, setAllItemsSelectedCheckbox] = useState(false);

    useEffect(() => {
        setSelectedItems([]);
        parentCallback([]);
    }, [itemList])


    /*
        Handles a single change of one of the items, which in turn are passed to the parent component
    */
    const handleOnChange = (event) => {
        setSelectedItems(event.target.value);
        parentCallback(event.target.value);
    };


    /*
        Handles selection and deselection of all items, and passes the selected items list to the parent component
    */
    const handleOnChangeSelectDeselectAllItems = (event) => {
        if (isSelectAllItemsCheckboxChecked) {
            setSelectedItems([]);
            parentCallback([])
            setAllItemsSelectedCheckbox(false);
        }
        else {
            setSelectedItems(itemList);
            parentCallback(itemList)
            setAllItemsSelectedCheckbox(true);
        }
    };


    return (
        <div>
            <FormControl variant="filled">
                <InputLabel
                    value={selectedItems}
                    id="mutiple-checkbox-label">Feature Column Names</InputLabel>
                    <Select
                        labelId="mutiple-checkbox-label"
                        id="select-multiple-checkbox"
                        multiple
                        displayEmpty
                        value={selectedItems}
                        onChange={handleOnChange}
                        renderValue={(selected) => selected?.join(', ')}>
                    <InputLabel id="mutiple-checkbox-label">
                        <Checkbox color="secondary"
                            checked={isSelectAllItemsCheckboxChecked} onChange={handleOnChangeSelectDeselectAllItems} >
                        </Checkbox>Check/Uncheck All
                    </InputLabel>
                    {
                        itemList?.map((name) => {
                            return (<MenuItem key={name} value={name}>
                                <Checkbox color="primary"
                                    checked={selectedItems.indexOf(name) > -1} />
                                <ListItemText primary={name} />
                            </MenuItem>)
                        })
                    }
                    </Select>
            </FormControl>
        </div>
    )
}

