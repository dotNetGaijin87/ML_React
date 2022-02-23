import React, { Component } from 'react';
import FormControl from '@material-ui/core/FormControl';
import { Notification } from '../Notification.js';


export class CsvFileDropZone extends Component {
    constructor(props) {
        super(props);
        this.state = {
            fileName: '',
            fileSentNotificationIsShown: false,
            notificationMessage: '',
            notificationSeverity: '',
        };
    }

    hideNotification = () => {
        this.setState({
            fileSentNotificationIsShown: false,
        });
    }

    getFile = (event) => {
        if (event.target.files === 'undefined') {
            return;
        }
        if (event.target.files.length > 0) {
            let name = event.target.files[0].name;
            if (!name.includes('.csv')) {
                this.setState({
                    notificationMessage: 'Wrong file format: Only csv files are accepted',
                    notificationSeverity: 'error',
                    fileSentNotificationIsShown: true
                });
                return;
            }
            if (name.length > 20) {
                name = name.substring(0, 20).concat('', '...');
            }
            this.setState({fileName: name })
            this.props.parentCallback(event.target.files[0]);
        }
    }


    render() {
        return (
            <FormControl variant="filled"  >
                <label for="file-input" className="customDropzoneArea">{this.props.title}
                    <input id="file-input" type="file" onChange={this.getFile} />
                    <div style={{ whiteSpace: "pre-line", color: "#2196f3", fontSize: "1.1rem", marginTop: "-7px" }} > {this.state.fileName}</div>
                </label>
                <Notification
                    open={this.state.fileSentNotificationIsShown}
                    notificationSeverity={this.state.notificationSeverity}
                    notificationMessage={this.state.notificationMessage}
                    parentCallback={this.hideNotification}>
                </Notification>
            </FormControl>
        )
    }
}


