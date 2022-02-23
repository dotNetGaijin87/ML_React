import React, { useState, useEffect } from 'react';
import axios from 'axios';
import FormControl from '@material-ui/core/FormControl';
import { CustomMultipleSelect } from '../../../Common/Controls/CustomMultipleSelect.js';
import { CustomSelect } from '../../../Common/Controls/CustomSelect.js';
import { Notification } from '../../../Common/Notification.js';
import { getTrainingFileNames, getTrainigFileHeaders } from '../../../Common/Functions/Communication.js';

export function TrainingDataSelection({ targetModelType, parentCallback, modelCategoryName }) {

    const [fileName, setFileName] = useState();
    const [allFeatureColumnNames, setAllFeatureColumnNames] = useState([]);
    const [featureColumnName, setFeatureColumnName] = useState();
    const [trainingDataFiles, setTrainingDataFiles] = useState([]);
    const [isNotificationShown, showNotification] = useState();
    const [notificationSeverity, setNotificationSeverity] = useState();
    const [notificationMessage, setNotificationMessage] = useState();
 

    useEffect(() => {
        updateParent();

    }, []);

    useEffect(() => {
        (async () => {
            let names = await getTrainingFileNames(modelCategoryName, targetModelType, setNotification);
            setTrainingDataFiles(names);
        })()

    }, [targetModelType])

    useEffect(() => {
        (async () => {
            if (fileName) {
                let headers = await getTrainigFileHeaders(modelCategoryName, fileName, targetModelType, setNotification);
                setAllFeatureColumnNames(headers);
            }
        })()
    }, [fileName]);

    useEffect(() => {
        updateParent();

    }, [featureColumnName]);

    const updateParent = () => {
            parentCallback({
            fileName: fileName,
            featureColumnNames: featureColumnName,
            allFeatureColumnNames: allFeatureColumnNames,
        });
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
        <div>
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
                <CustomMultipleSelect
                    title={'Feature Column Names'}
                    parentCallback={(value) => {
                        setFeatureColumnName(value);
                    }}

                    
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
