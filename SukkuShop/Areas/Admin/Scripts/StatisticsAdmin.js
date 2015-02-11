var dataPie = [
    {
        value: 0,
        color: "rgba(255, 102, 0, 1)",
        highlight: "rgba(255, 102, 0, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(0, 196, 40, 1)",
        highlight: "rgba(0, 196, 40, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(250, 30, 30, 1)",
        highlight: "rgba(250, 30, 30, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(181, 22, 160, 1)",
        highlight: "rgba(181, 22, 160, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(43, 22, 181, 1)",
        highlight: "rgba(43, 22, 181, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(22, 144, 181, 1)",
        highlight: "rgba(22, 144, 181, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(181, 96, 22, 1)",
        highlight: "rgba(181, 96, 22, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(247, 232, 10, 1)",
        highlight: "rgba(247, 232, 10, 0.7)",
        label: ""
    },
    {
        value: 0,
        color: "rgba(255, 179, 0, 1)",
        highlight: "rgba(255, 179, 0, 0.7)",
        label: ""
    }, {
        value: 0,
        color: "rgba(0, 255, 128, 1)",
        highlight: "rgba(0, 255, 128, 0.7)",
        label: ""
    }
];

var chartData = {
    labels: [],
    datasets: [
        {
            label: "My Second dataset",
            fillColor: "rgba(255, 102, 0, 0.2)",
            strokeColor: "rgba(255, 102, 0, 1)",
            pointColor: "rgba(255, 102, 0, 1)",
            pointStrokeColor: "#fff",
            pointHighlightFill: "#fff",
            pointHighlightStroke: "rgba(255, 102, 0, 1)",
            data: []
        }
    ]
};

var options = {
    scaleShowGridLines: true,
    scaleGridLineColor: "rgba(0,0,0,.05)",
    //Number - Width of the grid lines
    scaleGridLineWidth: 1,

    //Boolean - Whether to show horizontal lines (except X axis)
    scaleShowHorizontalLines: true,

    //Boolean - Whether to show vertical lines (except Y axis)
    scaleShowVerticalLines: true,

    //Boolean - Whether the line is curved between points
    bezierCurve: true,

    //Number - Tension of the bezier curve between points
    bezierCurveTension: 0.4,

    //Boolean - Whether to show a dot for each point
    pointDot: true,

    //Number - Radius of each point dot in pixels
    pointDotRadius: 4,

    //Number - Pixel width of point dot stroke
    pointDotStrokeWidth: 1,

    //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
    pointHitDetectionRadius: 20,

    //Boolean - Whether to show a stroke for datasets
    datasetStroke: true,

    //Number - Pixel width of dataset stroke
    datasetStrokeWidth: 2,

    //Boolean - Whether to fill the dataset with a colour
    datasetFill: true,

    //String - A legend template
    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].strokeColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"
};

function dataForPieMonthChart(selectedmonth, data) {
    dataPie.forEach(function(obj) {
        obj.value = 0;
        obj.label = '';
    });
    if (selectedmonth == maxMonth) {
        $("#titlepie").html("Top " + data.total.length + " zamawianych produktów");
        for (var i = 0; i < data.total.length; i++) {

            dataPie[i].label = data.total[i].name;
            dataPie[i].value = data.total[i].sum;
        }
    } else {
        $("#titlepie").html("Top " + data.ordersbymonth[selectedmonth].products.length + " zamawianych produktów w miesiącu " + months[data.ordersbymonth[selectedmonth].month - 1]);

        for (var j = 0; j < data.ordersbymonth[selectedmonth].products.length; j++) {
            dataPie[j].label = data.ordersbymonth[selectedmonth].products[j].name;
            dataPie[j].value = data.ordersbymonth[selectedmonth].products[j].sum;
        }
    }
}

var months = ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"];

function setNavigtionText(data, selectedmonth) {
    if (selectedmonth == maxMonth) {
        $("#left").html(months[data.ordersbymonth[data.ordersbymonth.length - 1].month - 1]);
        $("#current").html("Total");
        $("#right").html("");
    } else if (selectedmonth + 1 == maxMonth && selectedmonth != 0) {
        $("#right").html("Wszystko");
        $("#left").html(months[data.ordersbymonth[data.ordersbymonth.length - 2].month - 1]);
        $("#current").html(months[data.ordersbymonth[data.ordersbymonth.length - 1].month - 1]);
    } else if (selectedmonth + 1 == maxMonth && selectedmonth == 0) {
        $("#right").html("Wszystko");
        $("#left").html("");
        $("#current").html(months[data.ordersbymonth[data.ordersbymonth.length - 1].month - 1]);
    } else if
    (selectedmonth == 0 && maxMonth != 1) {
        $("#left").html("");
        $("#right").html(months[data.ordersbymonth[1].month - 1]);
        $("#current").html(months[data.ordersbymonth[0].month - 1]);

    } else if (selectedmonth == 0 && maxMonth != 1) {
        $("#right").html("Wszystko");
        $("#left").html("");
        $("#current").html(months[data.ordersbymonth[selectedmonth].month - 1]);
    } else {
        $("#right").html(months[data.ordersbymonth[selectedmonth + 1].month - 1]);
        $("#left").html(months[data.ordersbymonth[selectedmonth - 1].month - 1]);
        $("#current").html(months[data.ordersbymonth[selectedmonth].month - 1]);

    }
}


var maxMonth = 0;
var selectedmonth = -1;
$("#left").click(function() {
    if (selectedmonth > 0) {
        selectedmonth--;
        setNavigtionText(piedataplz, selectedmonth);
        dataForPieMonthChart(selectedmonth, piedataplz);
        $("#canvas-wrapper").html("").html("<canvas id='top5productsChart' width='800' height='400' style='margin:auto;display: block;'></canvas>");
        var ctxPie = $("#top5productsChart").get(0).getContext("2d");
        var myPieChart = new Chart(ctxPie).Pie(dataPie, options);
    }
});

$("#right").click(function() {
    if (selectedmonth <= maxMonth) {
        selectedmonth++;
        setNavigtionText(piedataplz, selectedmonth);
        dataForPieMonthChart(selectedmonth, piedataplz);
        $("#canvas-wrapper").html("").html("<canvas id='top5productsChart' width='800' height='400' style='margin:auto;display: block;'></canvas>");
        var ctxPie = $("#top5productsChart").get(0).getContext("2d");
        var myPieChart = new Chart(ctxPie).Pie(dataPie, options);
    }
});

var piedataplz = [];

$(document).ready(function() {


    $.get("/Admin/Statystyki/GetOrderData", function(data) {
        for (var i = 0; i < data.length; i++) {
            chartData.labels.push(data[i].date);
            chartData.datasets[0].data.push(data[i].count);
        }
        var ctx = $("#ordersChart").get(0).getContext("2d");
        // This will get the first returned node in the jQuery collection.
        var myLineChart = new Chart(ctx).Line(chartData, options);
    }).error(function(xhr, status, error) {
        alert(error);
        console.log(xhr.responseText);
    });

    $.get("/Admin/Statystyki/GetTopProducts", function(data) {
        maxMonth = data.ordersbymonth.length;
        selectedmonth = maxMonth;
        dataForPieMonthChart(selectedmonth, data);
        piedataplz = data;
        setNavigtionText(data, selectedmonth);

        // For a pie chart
        var ctxPie = $("#top5productsChart").get(0).getContext("2d");
        var myPieChart = new Chart(ctxPie).Pie(dataPie, options);
    }).error(function(xhr, status, error) {
        alert(error);
        console.log(xhr.responseText);
    });

    $.get("/Admin/Statystyki/GetOrdersByCategory", function(data) {
        //for (var i = 0; i < data.length; i++) {
        //    dataPie[i].label = data[i].name;
        //    dataPie[i].value = data[i].sum;
        //}
        //// For a pie chart
        //var ctxPie = $("#top5productsChart").get(0).getContext("2d");
        //var myPieChart = new Chart(ctxPie).Pie(dataPie, options);
    }).error(function(xhr, status, error) {
        alert(error);
        console.log(xhr.responseText);
    });

});