import React, { PureComponent } from 'react';
import {
    CartesianGrid,
    ResponsiveContainer,
    Tooltip,
    XAxis,
    YAxis,
    LineChart,
    Line
} from 'recharts';



export class SingleVariableLineChart extends PureComponent {
    constructor(props) {
        super(props);
    }


    render() {











        return (         
            <ResponsiveContainer width="100%" height="100%">
                <LineChart
                    width={this.props.width}
                    height={this.props.height}
                    data={this.props.data}
                    margin={{
                        top: 25,
                        right: 30,
                        left: 20,
                        bottom: 5,
                    }}>
                    <CartesianGrid vertical={false} horizontal={false} stroke="#EEEEEE13" fillOpacity={0.2} />
                    <defs>
                        <linearGradient
                            id='value'
                            x1="0%" y1="0%" x2="0%" y2="100%">
                            <stop offset="35%" stopColor={`${this.props.color}8f`} />
                            <stop offset="70%" stopColor={`${this.props.color}7f`} />
                            <stop offset="90%" stopColor={`${this.props.color}6e`} />
                            <stop offset="98%" stopColor={`${this.props.color}58`} />
                        </linearGradient>
                    </defs>
                    <YAxis  />
                    <XAxis dataKey="name">
                    </XAxis >
 
                    <Line type="monotone" dataKey='value' stroke={this.props.color} strokeWidth={2} dot={false} />
                    <Line type="monotone" dataKey='actual' stroke={`#FF6347`} strokeWidth={1} dot={false} />
                </LineChart>
            </ResponsiveContainer>
        );
    }
}