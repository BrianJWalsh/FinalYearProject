$(document).ready(function () {
    $("#searchResults").tablesorter({ sortList: [[0, 0], [4, 0]] });
    // var cheapestItem = "<table>";
    var cheapestItem = $("#searchResults tbody tr:first-child");
    // cheapestItem += "</table>"
    $("#cheapestItemTable").append(cheapestItem);
    calculateAvgPrice();
    calculateTotalPrice();

    // $('table.highchart').highchartTable();
    $('table.highchart')
                   .bind('highchartTable.beforeRender', function (event, highChartConfig) {
                       highChartConfig.chart.backgroundColor = '#262626';
                       highChartConfig.chart.borderRadius = '8';
                       highChartConfig.chart.style = { "fontFamily": "inherit" };
                       highChartConfig.chart.style = { "pading": "10px" };
                       highChartConfig.title.style = { "color": "#CCCCCC" };
                       highChartConfig.tooltip.style = { "color": "#000000" };
                       highChartConfig.tooltip.backgroundColor = '#CCCCCC';
                       highChartConfig.tooltip.borderWidth = '3';
                       highChartConfig.tooltip.borderColor = '#8294E0';
                       highChartConfig.tooltip.borderRadius = '8';
                       for (var i = 0; i < highChartConfig.yAxis.length; i++) {
                           highChartConfig.yAxis[i].labels.style = { "color": "#CCCCCC" };
                       }
                   })
                   .highchartTable();


    $('.shippingPrice').prepend('£');
    $('.price').prepend('£');
    $('.totalPrice').prepend('£');
    $('.highcharts-yaxis-labels text').prepend('£');
    $('.totalPrice').toFixed(2);
    $('.totalPrice').trunc(5);
   
});

function calculateAvgPrice() {
    var sum = 0;
    // iterate through each td based on class and add the values
    $(".price").each(function () {
        var numItems = $('.price').length;
        parseFloat(numItems);
        var value = $(this).text();
        // $('.totalPrice').prepend(value);

        //value = value;

        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {

            sum += parseFloat(value);
        }

        calculatePrice = (sum / numItems);
        calculatePrice = calculatePrice.toFixed(2);
        document.getElementById("avgPrice").innerHTML = "£" + calculatePrice;
    });
    //

}
//get total price: add price item + shipping-> add it to totalPrice cell
function calculateTotalPrice() {
    $(" table tbody tr").each(function () {

        var price = $(this).children('td').slice(4).text();
        //alert(price);
        var shippingPrice = $(this).children('td').slice(5).text();
        // var total = price + shippingPrice;
        var ParsPrice = parseFloat(price);
        var ParsShippingPrice = parseFloat(shippingPrice);
        var totalPrice = $(this).children('.totalPrice');

        totalPrice.append(ParsPrice + ParsShippingPrice);
        totalPrice = parseFloat(totalPrice).toFixed(2);
    });
}