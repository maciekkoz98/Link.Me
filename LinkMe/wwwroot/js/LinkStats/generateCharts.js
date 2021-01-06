window.onload = async () => {
    var id = window.location.href.split("/")[4];
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
    prepareDateStats(dateStatsDtos);

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
    var map = am4core.create("locationsMap", am4maps.MapChart);
    map.geodata = am4geodata_worldHigh;
    map.geodataNames = am4geodata_lang_PL;
    map.projections = new am4maps.projections.Mercator();

    var polygonSeries = map.series.push(new am4maps.MapPolygonSeries());
    polygonSeries.heatRules.push({
        "property": "fill",
        "target": polygonSeries.mapPolygons.template,
        "min": am4core.color("#34dbd2"),
        "max": am4core.color("#01938b"),
    });
    polygonSeries.useGeodata = true;
    polygonSeries.data = regionDtos;

    var polygonTemplate = polygonSeries.mapPolygons.template;
    polygonTemplate.tooltipText = "{name}: [b]{value}[/]";
}

function prepareDateStats(dateStatsDtos) {
    for (var i = 0; i < dateStatsDtos.length - 1; i++) {
        var stat = dateStatsDtos[i];
        var statDate = new Date(stat.clickDate);
        statDate.setDate(statDate.getDate() + 1);
        var nextStat = dateStatsDtos[i + 1];
        var nextStatDate = new Date(nextStat.clickDate);
        if (statDate.getTime() !== nextStatDate.getTime()) {
            dateStatsDtos.splice(i + 1, 0, { clickDate: statDate, count: 0 });
        }
    }
}