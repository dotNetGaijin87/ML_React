import React, { Component } from 'react';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@mui/material/Alert';

const Alert = React.forwardRef(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});



export class Notification extends Component {
    render() {
        return (
            <Snackbar
                open={this.props.open}
                onClose={() => this.props.parentCallback()} 
                autoHideDuration={6000}>
                <Alert
                    severity={this.props.notificationSeverity}
                    sx={{ width: '100%' }}>
                    {this.props.notificationMessage}
                </Alert>
            </Snackbar>
        )
    }
}



