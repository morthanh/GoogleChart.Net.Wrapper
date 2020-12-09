window.GoogleChart = {
    Draw: function (chartType, elemId, data, opt) {
        console.info("Draw " + chartType + " on element " + elemId);

        var gPackages = [];
        if (chartType == "LineChart" || chartType == "AreaChart") { gPackages.push("corechart"); }
        if (chartType == "Table") { gPackages.push("table"); }
        if (chartType == "Gauge") { gPackages.push("gauge"); }


        google.charts.load('current', { 'packages': gPackages });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var datatable = new google.visualization.DataTable(data);
            var options = JSON.parse(opt);

            var elem = document.getElementById(elemId);

            var chart =
                chartType == "LineChart" ? new google.visualization.LineChart(elem) :
                    chartType == "AreaChart" ? new google.visualization.AreaChart(elem) :
                        chartType == "Table" ? new google.visualization.Table(elem) :
                            chartType == "Gauge" ? new google.visualization.Gauge(elem) :
                null;

            if (chart == null) {
                throw "ChartType '" + chartType + "' not supported";
            }

            chart.draw(datatable, options);

            GoogleChart.Charts[elemId] = { 'c': chart, 'dt': datatable, 'opt': options };
        }


    },
    Charts: [],
    SetValue: function (elemId, row, column, value) {
        var chart = this.Charts[elemId]['c'];
        var datatable = this.Charts[elemId]['dt'];
        var options = this.Charts[elemId]['opt']

        datatable.setValue(row, column, value);
        chart.draw(datatable, options);
    }
}