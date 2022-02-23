import React, { PureComponent } from 'react';
import { Bar, BarChart, CartesianGrid, LabelList, Legend, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts';



export class SingleVariableBarChart extends PureComponent {
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
                        top: 25,
                        right: 30,
                        left: 20,
                        bottom: 5,
                    }}>
                    <CartesianGrid vertical={false} horizontal={false} stroke="#EEEEEE13" fill='#22272e'/>
                    <defs>
                        <linearGradient 
                            id='value'
                            x1="0%" y1="0%" x2="0%" y2="100%">
                            <stop offset="35%" stopColor={`${this.props.color}ff`} />
                            <stop offset="70%" stopColor={`${this.props.color}8f`} />
                            <stop offset="90%" stopColor={`${this.props.color}4e`} />
                            <stop offset="98%" stopColor={`${this.props.color}18`} />
                        </linearGradient>
                    </defs>
                    <YAxis type="number" domain={[-1.2, 1.2]} />
                    <XAxis dataKey="name" tick={false}>
                    </XAxis >
                    <Tooltip
                        cursor={{ fill: '#FFFFFF0e' }}
                        formatter={function (value, name) {
                            return `${value}`;
                        }}
                        labelFormatter={function (value) {
                            return `label: ${value}`;
                        }}
                    />
         
                    <Bar
                        barSize={50}
                        radius={[5, 5, 5, 5]}
                        dataKey='value'
                        fill={`url(#value)`}>
                        <LabelList dataKey={this.props.barHeader} fill='#eee' position="top" />
                    </Bar>
                </BarChart>
            </ResponsiveContainer>
        );
    }
}
