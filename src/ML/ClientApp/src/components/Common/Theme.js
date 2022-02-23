import { createTheme } from '@material-ui/core/styles';
import { grey, blue } from '@material-ui/core/colors';


export const Theme = createTheme({
    shadows: ["none"],
    palette: {
        primary: {
            main: blue[500],
            static: '#828d9c'
        },
        secondary: {
            main: '#007bff',
            contrastText: blue[900],
        },
        background: '#252530',
        containerBackground: '#2d333b',
        controlBackground: '#22272e',
        controlHovered: '#4b556357',
        border: '#4e5865',
        
    },
});

Theme.props = {
    MuiButton: {
/*        disableElevation: true,*/
    },
    MuiInputLabel: {
        shrink: true,
    },
    MuiInput: {
        disableUnderline: true,
    },
    MuiTooltip: {
        arrow: true,
    },
};



Theme.sizes = {
    global: {
        cornerRadius: 6
    },
    button: {
        width: 150,
    },
    select: {
        width: 200,
    },
    input: {
        width: 300,
    },
    slider: {
        width: 500,
    }
}

Theme.overrides = {
    MuiGrid: {
        container: {
            backgroundColor: Theme.palette.containerBackground,
            borderRadius: Theme.sizes.global.cornerRadius,
            padding: 20,
            margin: 20,
        }
    },
    MuiFormControl: {
        root: {
            marginBottom: 10,
            borderRadius: Theme.sizes.global.cornerRadius,
            flexDirection: 'row',
            flexWrap: 'wrap',
            alignItems: 'center',
  /*          justifyContent: 'center'*/
        },
    },
    MuiDropzoneArea: {
        root: {
            margin: '10px 100px',
            width: 'auto',
            minHeight: 100,
            color: Theme.palette.primary.static,
            backgroundColor: Theme.palette.controlBackground,
        },
        icon: {
            color: Theme.palette.primary.static,
        },
    },
    MuiSelect: {
        root: {
/*            width: Theme.sizes.select.width,*/
            color: Theme.palette.primary.main,
            borderRadius: Theme.sizes.global.cornerRadius,
        },
        icon: {
            color: Theme.palette.primary.static,
        },
        select: {
            backgroundColor: Theme.palette.controlBackground,
            borderRadius: Theme.sizes.global.cornerRadius,
        },
        '&:hover:not($disabled)': {
            background: Theme.palette.controlHovered,
        },
    },
    MuiMenu: {
        list: {
            backgroundColor: Theme.palette.controlBackground,
        }
    },
    MuiListItem: {
        root: {
            background: 'transparent',
            borderRadius: Theme.sizes.global.cornerRadius,
            '&$selected': { // <-- mixing the two classes
                backgroundColor: 'transparent'
            },
            '&:hover:not($disabled)': {
                background: Theme.palette.controlHovered,
                /*             color: CustomTheme.palette.primary.main,*/
            },
        }
    },



    MuiButton: {
        root: {
            textTransform: 'none',
            fontSize: '1.0rem',
            lineHeight: 1.5,
            margin: '10px 10px',
            width: Theme.sizes.button.width,
            padding: '10px'
 
        },
        outlinedPrimary: {
            padding: '5px 5px',
            fontWeight: 700
        },
        containedSecondary: {
            color: '#ebebeb',
            fontWeight: 700,
            backgroundColor: Theme.palette.controlBackground,
            border: `1px solid #007bffa6`,
        }
    },
    MuiInputLabel: {
        root: {
            textTransform: 'None',
            fontSize: '1.25rem',
            color: Theme.palette.primary.static,
            "&$focused": { // increase the specificity for the pseudo class
                color: Theme.palette.primary.static
            },
            backgroundColor: 'transparent',
        },

    },
    MuiInput: {
        root: {
            top: Theme.spacing(2),
            border: `1px solid ${grey[500]}`,
            outline: `1px solid transparent`,
            padding: Theme.spacing(1),
            '&$focused': {
                border: `1px solid ${Theme.palette.primary.main}`,
                outline: `1px solid ${Theme.palette.primary.main}`,
            },
        },
    },
    MuiFilledInput: {
        root: {
            backgroundColor: Theme.palette.controlBackground,
            width: Theme.sizes.input.width,
            borderRadius: Theme.sizes.global.cornerRadius,
            color: Theme.palette.primary.main,
            position: 'relative',
            '&($error)': {
                borderColor: Theme.palette.border,
            },
            '&:hover:not($disabled):not($error)': {
                backgroundColor: Theme.palette.controlBackground,
            },
            '&$focused': {
                backgroundColor: Theme.palette.controlBackground,
            },
        },
        input: {

        }
    },
    MuiOutlinedInput: {
        root: {
            position: 'relative',
            '& $notchedOutline': {
                borderColor: Theme.palette.border,
            },
            '&:hover:not($disabled):not($focused):not($error) $notchedOutline': {
                borderColor: Theme.palette.primary.main,
                '@media (hover: none)': {
                    borderColor: Theme.palette.primary.main,
                },
            },
            '&$focused $notchedOutline': {
                borderColor: Theme.palette.primary.main,
                borderWidth: 1,
            },
        }
    },
    MuiSlider: {
        root: {
            marginTop: 30
            //width: Theme.sizes.slider.width,
            //backgroundColor: Theme.palette.controlBackground,
        },
    },
    MuiTooltip: {
        tooltip: {
            backgroundColor: '#fff',
            border: `2px solid ${Theme.palette.primary.main}`,
            color: Theme.palette.primary.main,
        },
        arrow: {
            color: Theme.palette.primary.main,
        },
    },
};

