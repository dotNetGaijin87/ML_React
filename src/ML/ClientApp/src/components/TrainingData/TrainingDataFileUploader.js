import React, { Component } from 'react';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import axios from "axios";
import { CsvFileDropZone } from '../Common/Controls/CsvFileDropZone';
import { Notification } from '../Common/Notification.js';
import { CustomSelect } from '../Common/Controls/CustomSelect.js';
import { getErrorsFromException } from '../Common/Functions/Communication.js';

export class TrainingDataFileUploader extends Component {
    constructor(props) {
        super(props);
        this.state = {
            fileSentNotificationIsShown: false,
            notificationMessage: '',
            notificationSeverity:'',
            file: {},
            fileName: '',
            modelDictionary: {},
            categories: [],
            models: [],
            selectedCategory: '',
            selectedModelType: '',

        };
        this.getFile = this.getFile.bind(this);
        this.getModelType = this.getModelType.bind(this);
        this.uploadFile = this.uploadFile.bind(this);
        this.getAvailableModels = this.getAvailableModels.bind(this);
        this.updateModels = this.updateModels.bind(this);
        this.getAvailableCategories();
    }


    hideNotification = () => { this.setState({ fileSentNotificationIsShown: false }) }

    getModelType = (selectedModelType) => {
        this.setState({
            selectedModelType: selectedModelType
        })
    }


    getFile = (selectedFile) => { this.setState({ file: selectedFile }) }

    updateModels = (categoryName) => {
        this.setState({ selectedCategory: categoryName });
        this.setState({ models: this.state.modelDictionary[categoryName].map(elem => elem[0]) });
 
    }


    getAvailableCategories = async () => {

        try {
            var result = await axios.get("/info/category");
 
            this.setState({ categories: result.data });

            this.setState({ selectedCategory: result.data[0] });
 
        } catch (ex) {
            console.log(ex);
    
        }
    };


    getAvailableModels = async (selectedModel) => {

        try {
            this.setState({ selectedCategory: selectedModel });
            var result = await axios.get(`/info/category/${selectedModel}`);

            this.setState({ models: result.data[selectedModel].map(elem => elem.modelType) });


        } catch (ex) {
            console.log(ex);
            this.setState({
                notificationMessage: 'Error while fetching ai models from the server!',
                notificationSeverity: 'error',
                fileSentNotificationIsShown: true
            });
        }
    };

    async uploadFile(e) {
        const formData = new FormData();
        formData.append("formFile", this.state.file);
        formData.append("modelCategoryName", this.state.selectedCategory);
        formData.append("modelTypeName", this.state.selectedModelType);

        try {
            await axios.post("/training-data", formData);
            this.setState({
                notificationMessage: 'File saved successfully',
                notificationSeverity: 'success',
                fileSentNotificationIsShown: true
            });

        } catch (ex) {
            let errors = getErrorsFromException(ex);
            this.setState({
                notificationMessage: errors,
                notificationSeverity: 'error',
                fileSentNotificationIsShown: true
            });
        }
    };
 
 
    render() {
        return (
            <Grid
                container direction="column"
                justifyContent="flex-start"
                alignItems="stretch" >
                <CustomSelect
                    parentCallback={this.getAvailableModels}
                    itemList={this.state.categories}
                    title={'Category Type'}>
                </CustomSelect>
                <CustomSelect
                    parentCallback={this.getModelType}
                    itemList={this.state.models}
                    title={'Model Type'}>
                </CustomSelect>
                <CsvFileDropZone
                    title={"Training Data"}
                    parentCallback={this.getFile}>
                </CsvFileDropZone>
                <Button variant="outlined" size="medium" color="primary"
                        type="submit" onClick={this.uploadFile}>Submit
                </Button>  
                <Notification
                    open={this.state.fileSentNotificationIsShown}
                    notificationSeverity={this.state.notificationSeverity}
                    notificationMessage={this.state.notificationMessage}
                    parentCallback={this.hideNotification}>
                </Notification>
            </Grid>
        );
    };
}