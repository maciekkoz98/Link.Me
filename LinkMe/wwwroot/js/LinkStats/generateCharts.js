window.onload = async () => {
    var id = window.location.href.slice(-1);
    await fetch(`https://localhost:44388/api/LinkStats/${id}`,
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
            createClickChart(json.dateStatsDtos);
            createMapChart(json.regionDtos);
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
    series.stroke = am4core.color("#02d3c7");
    series.strokeWidth = 2;
    series.tooltipText = "{dateX}: [b]{valueY}[/]";
    series.bullets.push(new am4charts.CircleBullet());
    series.data = dateStatsDtos;

    chart.cursor = new am4charts.XYCursor();
}

function createMapChart(regionDtos) {
    console.log(regionDtos);
    var map = am4core.create("locationsMap", am4maps.MapChart);
    map.geodata = am4geodata_worldHigh;
    map.projections = new am4maps.projections.Miller();

    var polygonSeries = map.series.push(new am4maps.MapPolygonSeries());
    polygonSeries.heatRules.push({
        "property": "fill",
        "target": polygonSeries.mapPolygons.template,
        "min": am4core.color("#006661"),
        "max": am4core.color("#5ce2db"),
    });
    polygonSeries.useGeodata = true;
    polygonSeries.dataFields.categoryX = "countryCode";
    polygonSeries.dataFields.valueY = "count";

    var polygonTemplate = polygonSeries.mapPolygons.template;
    polygonTemplate.tooltipText = "{name}: [b]{value}[/]";
}