$(document).ready(function () {

    $("#searchResults").tablesorter({ sortList: [[0, 0], [4, 0]] });
    var cheapestItem = $("#searchResults tbody tr:first-child");
    $("#cheapestItemTable").append(cheapestItem);
    calculateAvgPrice();

    // add chart and style it
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

    calculateTotalPrice();

    $('.highcharts-yaxis-labels text').prepend('£');
    $('.totalPrice').prepend('£');
    $('.shippingPrice').prepend('£');
    $('.price').prepend('£');
});

//better responsive design for mobile viewers
window.onload = function () {

    $(window).resize(function () {
        if ($(window).width() < 500) {

            $('.mobile-information').append("<h3>Tap image to reveal more information about the item</<h3>")

            $('td:nth-child(n+3)').hide();
            $('th:nth-child(n+3)').hide();
            $('td:nth-child(7)').show();
            $('th:nth-child(7)').show();

            $(".table").on("click", "td", function () {

                $('td').show("slow");
                $('th').show("slow");
            });

            $('#searchResults td:nth-child(odd)').css('width', '20px');
            //$('td').css('padding-right', '20px');

        }
        else {


        }
    }).resize();

}


function calculateAvgPrice() {
    var sum = 0;
    // iterate through each td based on class and add the values
    $(".price").each(function () {
        var numItems = $('.price').length;
        parseFloat(numItems);
        var value = $(this).text();

        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {

            sum += parseFloat(value);
        }

        calculatePrice = (sum / numItems);
        calculatePrice = calculatePrice.toFixed(2);
        document.getElementById("avgPrice").innerHTML = "£" + calculatePrice;
    });

}
//get total price: add price item + shipping-> add it to totalPrice cell
function calculateTotalPrice() {
    $(" table tbody tr").each(function () {
        var price = 0;
        var price = $(this).children('td').slice(4).text();
        var shippingPrice = $(this).children('td').slice(5).text();
        var ParsPrice = parseFloat(price);
        var ParsShippingPrice = parseFloat(shippingPrice);
        var totalPrice = $(this).children('.totalPrice');
        if (!isNaN(ParsShippingPrice) && ParsShippingPrice.length != 0) {

            totalPrice.append(ParsPrice + ParsShippingPrice);
        }
        if ($(this).children('td').slice(6).text() < 6) {
            //var actPrice = $(this).children('td').slice(6).text();
            //$(this).children('td').slice(6).text() = parseFloat($(this).children('td').slice(6).text());
        }



    });
}