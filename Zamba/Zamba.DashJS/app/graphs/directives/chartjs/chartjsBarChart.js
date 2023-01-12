'use strict';

angular.module('app.graphs').directive('chartjsBarChart', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {

            var barOptions = {
                //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
                scaleBeginAtZero : true,
                //Boolean - Whether grid lines are shown across the chart
                scaleShowGridLines : true,
                //String - Colour of the grid lines
                scaleGridLineColor : "rgba(0,0,0,.05)",
                //Number - Width of the grid lines
                scaleGridLineWidth : 1,
                //Boolean - If there is a stroke on each bar
                barShowStroke : true,
                //Number - Pixel width of the bar stroke
                barStrokeWidth : 1,
                //Number - Spacing between each of the X value sets
                barValueSpacing : 5,
                //Number - Spacing between data sets within X values
                barDatasetSpacing : 1,
                //Boolean - Re-draw chart on page resize
                responsive: true,
                //String - A legend template
                legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"
            }
            var valuex = [];
            var valuey = [];
            var countY = [];
            ////////////////// ARREGLO PARA LA X ////////
            var JsonVars = JSON.parse(localStorage.getItem("JsonVars"));
            for (var i = 0; i < JsonVars.length; i++) {
                var value = JsonVars[i].varx;
                if(!valuex.includes(value)){
                    valuex.push(value);
                }
            }
            ////////////////// se optienen los valores de y a mapear ////////       
            for (var i = 0; i < JsonVars.length; i++) {
                var value = JsonVars[i].vary;
                if(!countY.includes(value)){
                    countY.push(value);
                }
            }
            
            ////////////////// ARREGLO PARA LA Y SI ESTA POSEE SOLO DOS VALLORES ////////
            var List1 = [];
            var List2 = [];
            if (countY.length == 2) {
                for (var i = 0; i < valuex.length; i++) {
                    var countValueUno = 0;
                    var countValueDos = 0;
                    for (var y = 0; y < JsonVars.length; y++) {

                        if (valuex[i] == JsonVars[y].varx ) {

                            var value = JsonVars[y].vary;

                            if (value  == countY[0] ) {
                                countValueUno = countValueUno + 1;
                            } 
                            if (value  == countY[1] ) {
                                countValueDos = countValueDos + 1;
                            }     
                        }
                    }
                       
                    List1.push(countValueUno);
                    List2.push(countValueDos);
                }
                

            } 
            
            

            var barData = {
                labels: valuex,
                datasets: [
                    {
                        label: "My First dataset",
                        fillColor: "rgba(220,220,220,0.5)",
                        strokeColor: "rgba(220,220,220,0.8)",
                        highlightFill: "rgba(220,220,220,0.75)",
                        highlightStroke: "rgba(220,220,220,1)",
                        data: List1
                    },
                    {
                        label: "My Second dataset",
                        fillColor: "rgba(151,187,205,0.5)",
                        strokeColor: "rgba(151,187,205,0.8)",
                        highlightFill: "rgba(151,187,205,0.75)",
                        highlightStroke: "rgba(151,187,205,1)",
                        data: List2
                    }
                ]
            };

            var ctx = element[0].getContext("2d");
            new Chart(ctx).Bar(barData, barOptions);

        }
    }
});