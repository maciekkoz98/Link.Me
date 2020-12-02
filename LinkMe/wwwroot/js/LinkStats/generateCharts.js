window.onload = async () => {
    var id = window.location.href.slice(-1);
    var response = await fetch(`https://localhost:44388/api/LinkStats/${id}`,
        {
            method: "GET"
        })
        .then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(resp.status);
            }
        })
        .then((json) => {
            console.log(json.dateStatsDtos);
            createClickChart(json.dateStatsDtos);
        })
        .catch((error) => {
            console.log('Error: ', error);
        })
}

function createClickChart(dateStatsDtos) {
    var chart = am4core.create("clickChart", am4charts.XYChart);

    var categoryXAxes = chart.xAxes.push(new am4charts.DateAxis());
    categoryXAxes.title.text = "Data kliknięcia";
    categoryXAxes.renderer.grid.template.location = 0;
    categoryXAxes.renderer.minGridDistance = 20;

    var valueYAxes = chart.yAxes.push(new am4charts.ValueAxis());
    valueYAxes.title.text = "Liczba kliknięć";

    var series = chart.series.push(new am4charts.LineSeries());
    series.name = "Kliknięcia";
    series.dataFields.dateX = "clickDate";
    series.dataFields.valueY = "count";
    series.fill = "#02D3C7";
    series.tooltipText = "{dateX}: [b]{valueY}[/]";
    series.bullets.push(new am4charts.CircleBullet());
    series.data = dateStatsDtos;

    chart.cursor = new am4charts.XYCursor();
}