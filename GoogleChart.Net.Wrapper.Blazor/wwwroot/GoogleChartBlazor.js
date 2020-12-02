window.GoogleChart = {
    Draw: function (chartType, elemId, data, options) {
        console.info("Draw " + chartType + " on element " + elemId);

        var gPackages = [];
        if (chartType == "LineChart" || chartType == "AreaChart") { gPackages.push("corechart"); }
        if (chartType == "Table") { gPackages.push("table"); }


        google.charts.load('current', { 'packages': gPackages });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var datatable = new google.visualization.DataTable(data);

            var elem = document.getElementById(elemId);

            var chart =
                chartType == "LineChart" ? new google.visualization.LineChart(elem) :
                    chartType == "AreaChart" ? new google.visualization.AreaChart(elem) :
                        chartType == "Table" ? new google.visualization.Table(elem) :
                null;

            if (chart == null) {
                throw "ChartType '" + chartType + "' not supported";
            }

            chart.draw(datatable, JSON.parse(options));
        }


    }
}