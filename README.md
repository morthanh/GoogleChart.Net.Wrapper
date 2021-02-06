# GoogleChart.Net.Wrapper

Feeded by a need for an easy way to include Google Charts into my .Net Core websites I created this library. 
It is designed to hide as many JavaScript details as possible letting you focus on where data is coming from. 

Create a Google Chart DataTable instance from an existing IEnumerable using

	IEnumerable myData = store.GetData(...);
	var dataTable = myData.ToDataTable(conf => ... );
	
From here it is simple to extract the JSON needed to create the graph e.i. inside a razor page

	DataTableJson = dataTable.ToJson();
	
	...

	<script>
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
		
    function drawChart() {
        var data = new google.visualization.DataTable('@Html.Raw(Model.DataTableJson)');
        var chart = new google.visualization.LineChart(document.getElementById('chartDiv'));
        chart.draw(data, {});
    }
	</script>
