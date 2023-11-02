function createBasicBarChart(name, categories, chartData) {
    var options = {
        chart: {
            type: 'bar',
            height: 400,
        },
        series: [{
            name: name,
            data: chartData
        }],
        xaxis: {
            categories: categories
        }
    };

    var chart = new ApexCharts(document.querySelector("#basic-bar-chart"), options);
    chart.render();
}

function createDataLabelBarChart(name, categories, chartData) {
    var options = {
        series: [{
            data: chartData
        }],
        chart: {
            type: 'bar',
            height: 380
        },
        plotOptions: {
            bar: {
                barHeight: '100%',
                distributed: true,
                horizontal: true,
                dataLabels: {
                    position: 'bottom'
                },
            }
        },
        colors: ['#33b2df', '#546E7A', '#d4526e', '#13d8aa', '#A5978B', '#2b908f', '#f9a3a4', '#90ee7e',
            '#f48024', '#69d2e7'
        ],
        dataLabels: {
            enabled: true,
            textAnchor: 'start',
            style: {
                colors: ['#fff']
            },
            formatter: function (val, opt) {
                return opt.w.globals.labels[opt.dataPointIndex] + ":  " + val
            },
            offsetX: 0,
            dropShadow: {
                enabled: true
            }
        },
        stroke: {
            width: 1,
            colors: ['#fff']
        },
        xaxis: {
            categories: categories,
        },
        yaxis: {
            labels: {
                show: false
            }
        },
        //title: {
        //    text: 'Custom DataLabels',
        //    align: 'center',
        //    floating: true
        //},
        //subtitle: {
        //    text: 'Category Names as DataLabels inside bars',
        //    align: 'center',
        //},
        tooltip: {
            theme: 'dark',
            x: {
                show: false
            },
            y: {
                title: {
                    formatter: function () {
                        return ''
                    }
                }
            }
        }
    };

    var chart = new ApexCharts(document.querySelector("#data-label-bar-chart"), options);
    chart.render();
}

function createBasicPieChart(name, categories, chartData) {
    var options = {
        series: chartData,
        chart: {
            width: 380,
            type: 'pie',
        },
        labels: categories,
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#basic-pie-chart"), options);
    chart.render();
}
