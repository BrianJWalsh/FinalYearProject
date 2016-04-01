$(document).ready(function () {
    $("#searchResults").tablesorter({ sortList: [[0, 0], [4, 0]] });
    // var cheapestItem = "<table>";
    var cheapestItem = $("#searchResults tbody tr:first-child");
    // cheapestItem += "</table>"
    $("#cheapestItemTable").append(cheapestItem);
    alert("Test")
    $(calculatePrice);
});

function calculatePrice() {
    var sum = 0;
    // iterate through each td based on class and add the values
    $(".price").each(function () {

        var value = $(this).text();
        // add only if the value is number
        if (!isNaN(value) && value.length != 0) {
            sum += parseFloat(value);
        }
        $('#avgPrice').append("Test")
        
    });
}