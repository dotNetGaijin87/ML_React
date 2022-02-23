import React, { PureComponent } from 'react';
import { Bar, BarChart, CartesianGrid, LabelList, Legend, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts';



export class CustomBarChart extends PureComponent {
    constructor(props) {
        super(props);

    }
    render() {
        return (
            <ResponsiveContainer width="100%" height="100%">
                <BarChart
                width={this.props.width}
                height={this.props.height}
                data={this.props.data}
                margin={{
                    top: 5,
                    right: 30,
                    left: 20,
                    bottom: 5,
                }}
                >

                    <CartesianGrid vertical={false} horizontal={false} stroke="#EEEEEE13" fillOpacity={0.2}  />
                <defs>
                        <linearGradient
                            id='rSquared'
                            x1="0%" y1="0%" x2="0%" y2="100%">
                            <stop offset="35%" stopColor="#FFC1078f" />
                            <stop offset="70%" stopColor="#FFC1074f" />
                            <stop offset="90%" stopColor="#E5AD060e" />
                            <stop offset="98%" stopColor="#E5AD0601" />
                        </linearGradient>
                        <linearGradient
                            id='pinkGradient'
                            x1="0%" y1="0%" x2="0%" y2="100%">
                            <stop offset="35%" stopColor="#ff9900af" />
                            <stop offset="70%" stopColor="#ff99004f" />
                            <stop offset="90%" stopColor="#ff99000e" />
                            <stop offset="98%" stopColor="#ff990000" />
                        </linearGradient>
                        <linearGradient
                            id='propsGradient'
                            x1="0%" y1="0%" x2="0%" y2="100%">
                            <stop offset="35%" stopColor={`${this.props.color}af`} />
                            <stop offset="70%" stopColor={`${this.props.color}4f`}  />
                            <stop offset="90%" stopColor={`${this.props.color}0e`}/>
                            <stop offset="98%" stopColor={`${this.props.color}01`} />
                        </linearGradient>                     
                </defs>
                    <XAxis dataKey="name" tick={false} />
                <YAxis />
                    <Tooltip
                        cursor={{ fill: '#FFFFFF0e' }}
                        formatter={function (value, name) {
                            return `${value}`;
                        }}
                        labelFormatter={function (value) {
                            return `label: ${value}`;
                        }}
                    />
                <Legend cursor={{ fill: '#212529c2' }} />
     
                    <Bar
                        radius={[5, 5, 0, 0]}
                        dataKey='value'
                        fill={`url(#propsGradient)`}>
                        <LabelList dataKey="value" fill='#eee' position="top" />
                    </Bar>
                </BarChart>
                </ResponsiveContainer>
        );
    }
}
