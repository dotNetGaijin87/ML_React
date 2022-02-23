import { Line, Chart } from "react-chartjs-2";
import React from "react";
import zoomPlugin from "chartjs-plugin-zoom";

Chart.register(zoomPlugin);

 



export function CustomLineChart({ props }) {

/*    let draw = Chart.controllers.line.prototype.draw;*/
//draw = function () {
//    let chart = this.chart;
//    let ctx = chart.ctx;
//    let _stroke = ctx.stroke;
//    ctx.stroke = function () {
//        ctx.save();
//        ctx.shadowColor = ctx.strokeStyle;
//        ctx.shadowBlur = 5;
//        ctx.shadowOffsetX = 0;
//        ctx.shadowOffsetY = 4;
//        _stroke.apply(this, arguments);
//        ctx.restore();
//    };
//    draw.apply(this, arguments);
//    ctx.stroke = _stroke;
//};


    const axisFontSize = 16;
 
    const data = (canvas) => {
        var ctx = document.getElementById("chart").getContext("2d");

        var data0Gradient = ctx.createLinearGradient(0, 0, 0, 500);
        data0Gradient.addColorStop(0, 'rgba(40,175,250,.25)');
        data0Gradient.addColorStop(1, 'rgba(40,175,250,.01)');

        var data1Gradient = ctx.createLinearGradient(0, 0, 0, 200);
        data1Gradient.addColorStop(0, 'rgba(255,99,71,.25)');
        data1Gradient.addColorStop(1, 'rgba(255,99,71,0.01)');


        var data2Gradient = ctx.createLinearGradient(0, 0, 0, 200);
        data2Gradient.addColorStop(0, 'rgba(255,165,0,.25)');
        data2Gradient.addColorStop(1, 'rgba(255,165,0,0.01)');

        var data3Gradient = ctx.createLinearGradient(0, 0, 0, 200);
        data3Gradient.addColorStop(0, 'rgba(255, 99, 169,.25)');
        data3Gradient.addColorStop(1, 'rgba(255, 99, 169,0.01)');


        return {
            labels: props.labels,
            datasets: [
                {
                    label: props.label[0],
                    data: props.data[0],
                    fill: 'start',
                    backgroundColor: data0Gradient, 
                    borderColor: "#28AFFA",
                    borderWidth: 2,
                    pointRadius: 0, 
                    pointColor: "#19283F",
                    pointStrokeColor: "#28AFFA",
                    pointHighlightFill: "#19283F",
                    pointHighlightStroke: "#28AFFA",
                    lineTension: 0.4,
                    yAxisID: "left-y-axis",
                },
                {
                    label: props.label[1],
                    data: props.data[1],
                    fill: 'start',
                    backgroundColor: data1Gradient,
                    borderColor: "#FF6347",
                    borderWidth: 1,
                    pointRadius: 0,
                    pointColor: "#19283F",
                    pointStrokeColor: "#ff6347",
                    pointHighlightFill: "#19283F",
                    pointHighlightStroke: "#FF6347",
                    lineTension: 0.4,
                    yAxisID: !props.hasCommonYaxis ? "right-y-axis-1" : 'left-y-axis',
                },
                {
                    label: props.label[2],
                    data: props.data[2],
                    fill: 'start',
                    backgroundColor: data2Gradient,
                    borderColor: "#FFA500",
                    borderWidth: 1,
                    pointRadius: 1,
                    pointColor: "#FFA500",
                    pointStrokeColor: "446347",
                    pointHighlightFill: "#FFA500",
                    pointHighlightStroke: "#446347",
                    lineTension: 0.4,
                    yAxisID: !props.hasCommonYaxis ? "right-y-axis-2" : 'left-y-axis',
                },
                {
                    label: props.label[3],
                    data: props.data[3],
                    fill: 'start',
                    backgroundColor: data3Gradient,
                    borderColor: "#FF63A9",
                    borderWidth: 1,
                    pointRadius: 1,
                    pointColor: "#FF63A9",
                    pointStrokeColor: "446347",
                    pointHighlightFill: "#FF63A9",
                    pointHighlightStroke: "#446347",
                    lineTension: 0.4,
                    yAxisID: !props.hasCommonYaxis ? "right-y-axis-3" : 'left-y-axis',
                }
            ]
        }
    }


    const options = {
        maintainAspectRatio: false,
        responsive: true,
        onClick: function (evt, element) {
            if (element.length > 0) {
                var index = element[0].index;
                props.parentCallback(index);
            }
        },
        scales: {

                'left-y-axis': {
                display: (props.data && props.data[0]) ? true : false,
                    position: 'left',
                    gridLines: {
                        drawBorder: false,
                    },
                    ticks: {
                        beginAtZero: true,
                        color: '#28AFFA',
                        font: {
                            size: axisFontSize
                        },
                    },

                },
                'right-y-axis-1': {
                    display: (props.data && props.data[1] && !props.hasCommonYaxis) ? true : false,
                    position: 'right',
                    gridLines: {
                        drawBorder: false,
                    },
                    ticks: {
                        beginAtZero: true,
                        color: '#FF6347',
                        font: {
                            size: axisFontSize
                        },
                    },

                },
            'right-y-axis-2': {
                display: (props.data && props.data[2] && !props.hasCommonYaxis) ? true : false,
                position: 'right',
                gridLines: {
                    drawBorder: false,
                },
                ticks: {
                    beginAtZero: true,
                    color: '#FFA500',
                    font: {
                        size: axisFontSize
                    },
                },

            },
            'right-y-axis-3': {
                display: (props.data && props.data[3] && !props.hasCommonYaxis) ? true : false,
                position: 'right',
                gridLines: {
                    drawBorder: false,
                },
                ticks: {
                    beginAtZero: true,
                    color: '#FF63A9',
                    font: {
                        size: axisFontSize
                    },
                },

            },
            x: {
                gridLines: {
                    display: false,
                },
                ticks: {
                    color: '#fff',
                },
                font: {
                    size: axisFontSize
                },
            },
        },
        plugins: {
            legend: {
                labels: {
                    color: '#fff',
                    font: {
                        size: 18,
                    },
                    filter: function (item, chart) {
                        if (item.text) {
                            return item;
                        } else {
                            return false;
                        }
                    }
                }
            },
            zoom: {
                zoom: {
                    wheel: {
                        enabled: true,
                    },
                    drag: {
                        enabled: true,
                        modifierKey: 'ctrl',
                        backgroundColor: 'rgba(124,252,0,0.05)',
                        borderColor: 'rgba(124,252,0,0.8)',
                        borderWidth: '1'
                    },
                    mode: 'x',
                    speed: '0.1'
                },
                pan: {
                    enabled: true,
                    mode: 'x',
                },
            },
            deferred: {
                xOffset: 150,
                yOffset: '50%',
                delay: 500
            }
        }
    };

    return (
        <>
            <Line id="chart" data={data} options={options} plugins={[
                {
                    beforeDraw: function (chart, args, options) {
                        const ctx = chart.ctx;
                        const canvas = chart.canvas;
                        const chartArea = chart.chartArea;

                        // Chart background
                        var gradient = canvas
                            .getContext("2d")
                            .createLinearGradient(
                                canvas.width / 2,
                                0,
                                canvas.width / 2,
                                canvas.height
                            );
                        gradient.addColorStop(0.1, "rgba(34, 39, 46, 1)");
                        //gradient.addColorStop(0.2, "rgba(41, 41, 41, 0.95)");
                        //gradient.addColorStop(0.3, "rgba(41, 41, 41, 0.85)");
                        //gradient.addColorStop(0.4, "rgba(41, 41, 41, 0.75)");
                        //gradient.addColorStop(0.5, "rgba(41, 41, 41, 0.65)");
                        //gradient.addColorStop(0.6, "rgba(41, 41, 41, 0.55)");
                        //gradient.addColorStop(0.7, "rgba(41, 41, 41, 0.65)");
                        //gradient.addColorStop(0.8, "rgba(41, 41, 41, 0.85)");
                        //gradient.addColorStop(0.9, "rgba(41, 41, 41, 0.95)");
                        //gradient.addColorStop(1.0, "rgba(41, 41, 41, 1)");

                        ctx.fillStyle = gradient;
                        ctx.fillRect(
                            chartArea.left,
                            chartArea.bottom,
                            chartArea.right - chartArea.left,
                            chartArea.top - chartArea.bottom
                        );
                    }
                }
            ]} />
        </>);
}
