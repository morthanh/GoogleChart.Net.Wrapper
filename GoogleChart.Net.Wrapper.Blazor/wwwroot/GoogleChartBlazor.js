window.GoogleChart = {
    Draw: function (chartType, elemId, data, options) {
        console.info("Draw " + chartType + " on element " + elemId);

        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var datatable = new google.visualization.DataTable(data);

            var elem = document.getElementById(elemId);

            var chart =
                chartType = "LineChart" ? new google.visualization.LineChart(elem) :
                    chartType = "AreaChart" ? new google.visualization.AreaChart(elem) :
                        (function () { throw "ChartType '" + chartType + "' not supported" });
            chart.draw(datatable, JSON.parse(options));
        }


    }
}