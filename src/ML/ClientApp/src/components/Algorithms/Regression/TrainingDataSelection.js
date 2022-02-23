import React, { useState, useEffect } from 'react';
import FormControl from '@material-ui/core/FormControl';
import { CustomMultipleSelect } from '../../Common/Controls/CustomMultipleSelect.js';
import { CustomSelect } from '../../Common/Controls/CustomSelect.js';
import { Notification } from '../../Common/Notification.js';
import { getTrainingFileNames, getTrainigFileHeaders } from '../../Common/Functions/Communication.js';




export function TrainingDataSelection({ modelCategoryName, targetModelType, parentCallback}) {

    const [fileName, setFileName] = useState();
    const [labelColumnName, setLabelColumnName] = useState();
    const [allFeatureColumnNames, setAllFeatureColumnNames] = useState([]);
    const [featureColumnNames, setFeatureColumnNames] = useState([]);
    const [trainingDataFiles, setTrainingDataFiles] = useState([]);
    const [isNotificationShown, showNotification] = useState();
    const [notificationSeverity, setNotificationSeverity] = useState();
    const [notificationMessage, setNotificationMessage] = useState();
 
 

    useEffect(() => {
        (async () => {
            let resp = await getTrainingFileNames(modelCategoryName, targetModelType, setNotification);
            setTrainingDataFiles(resp);
        })()

    }, [targetModelType])

    useEffect(() => {
        (async () => {
            if (fileName) {
                let headers = await getTrainigFileHeaders(modelCategoryName, fileName, targetModelType, setNotification);
                setAllFeatureColumnNames(headers);
                setFeatureColumnNames(headers);
            }
        })()
    }, [fileName]);

    useEffect(() => {
        updateParent();

    }, [labelColumnName, featureColumnNames]);


    const updateParent = () => {
            parentCallback({
            fileName: fileName,
            labelColumnName: "Column" + (allFeatureColumnNames?.indexOf(labelColumnName)),
            featureColumnNames: featureColumnNames,
            allFeatureColumnNames: allFeatureColumnNames,
        });
    }

    const updateMultiSelect = (values) => {
        let columns = [];
        values.map((name) => {
            let index = allFeatureColumnNames?.indexOf(name);
            if (index > -1) {
                columns.push("Column" + (index));
            }
        });
        setFeatureColumnNames(columns);
    }

    const setNotification = (severity, message) => {
        showNotification(true);
        setNotificationSeverity(severity);
        setNotificationMessage(message);
    }

    const hideNotification = () => {
        showNotification(false);
    }


    return (
        <div style={{ marginLeft: 25, marginTop: 20 }}>
            <p>Data Selection</p>
            <FormControl variant='filled'>
                <CustomSelect
                    key={'Training Data Files'}
                    title={'Training Data Files'}
                    parentCallback={
                        (value) => {
                            setFileName(value);
                        }
                    }
                    itemList={trainingDataFiles}>
                </CustomSelect>
                <CustomSelect
                    key={'Label Column Name'}
                    title={'Label Column Name'}
                    parentCallback={
                        (value) => {
                            setLabelColumnName(value);
                        }
                    }
                    itemList={allFeatureColumnNames}>
                </CustomSelect>
                <CustomMultipleSelect
                    title={'Feature Column Names'}
                    parentCallback={updateMultiSelect}
                    itemList={allFeatureColumnNames}>
                </CustomMultipleSelect>   
            </FormControl>
            <Notification
                open={isNotificationShown}
                notificationSeverity={notificationSeverity}
                notificationMessage={notificationMessage}
                parentCallback={hideNotification}>
            </Notification>
        </div>
    );
}
