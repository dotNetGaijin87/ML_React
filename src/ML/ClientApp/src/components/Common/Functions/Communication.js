import axios from 'axios';

export {
    getTrainingFileNames,
    getTrainigFileHeaders,
    getErrorsFromException
};

const getTrainingFileNames = async (modelCategoryName, targetModelType, callback) => {
    try {
        let req = {
            modelCategoryName: modelCategoryName,
            modelTypeName: targetModelType
        };
        console.log(req);
        let resp = await axios.get(`/training-data/${modelCategoryName}/${targetModelType}`);
        return resp.data;

    } catch (ex) {
        callback('error', 'Error loading training data files');
    }
}


const getTrainigFileHeaders = async (modelCategoryName, fileName, targetModelType, callback) => {
    try {
        let req = {
            modelCategoryName: modelCategoryName,
            fileName: fileName,
            modelTypeName: targetModelType
        };
        let fileHeaders = await axios.post('/training-data:GetHeaders', req);
 
        return fileHeaders.data;

    } catch (ex) {
        callback('error', 'Downloading file headers error');
    }
}


const getErrorsFromException = (ex) => {
    let parsedErrors = [];
    let response = ex.response.data;
    // If errors property exists it means it is ValidationProblemDetails object
    if (response.hasOwnProperty('errors')) {
        for (const [key, value] of Object.entries(response.errors)) {
            if (Array.isArray(value)) {
                value.forEach((item, index) => {
                    parsedErrors.push(`${item}, `);
                });
            } else {
                parsedErrors.push(`${value.message}, `);
            }
        }
    } else if (response.hasOwnProperty('detail')) { // If detail property exists it is ProblemDetails object
        parsedErrors = response.detail;
    } else {
        parsedErrors = 'Error ocurred';
    }

    return parsedErrors;
}