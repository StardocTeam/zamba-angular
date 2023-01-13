
//////// GRAFICO DE BARRA ///////
$("canvas[name ='popChart']").each(function (index, a) {

    if (!(a == null)) {

        if (a.dataset.visible == "true") {

            var FormVariables = '';

            var valueForRuleQuery = $(a).attr("value-for-rule-query");
            if (valueForRuleQuery != "")
                FormVariables = ValuesFromTable(valueForRuleQuery);


            var legendVisible = true;
            if (a.dataset.legend != undefined)
                legendVisible = a.dataset.legend;

            var datalegend = $(a).attr("data-legend");
            if (datalegend != "" && datalegend == "false")
                legendVisible = false;

            var resultBarra = Report(parseInt(a.dataset.report), FormVariables);
            resultBarra = JSON.parse(resultBarra);

            var PositionXbarra = a.dataset.x;
            var PositionYbarra = a.dataset.y;

            var arrayBarraX = new Array();
            var arrayBarraY = new Array();


            for (var i = 0; i < resultBarra.length; i++) {

                var IteraResultBar = resultBarra[i][PositionXbarra];
                arrayBarraX.push(IteraResultBar);

            }

            for (var i = 0; i < resultBarra.length; i++) {

                var IteraResultBar = resultBarra[i][PositionYbarra];
                arrayBarraY.push(IteraResultBar);

            }


            var barChart = new Chart(a, {
                type: 'bar',
                data: {
                    labels: arrayBarraX,
                    datasets: [{
                        label: PositionYbarra,
                        data: arrayBarraY,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                        ]
                    }]
                },
                options: {
                    legend: {
                        display: legendVisible,
                    }
                }
            });

        } else {
            $("GraficoBarra").css("display", "none");
        }
    }
});


/////////// GRAFICO DE TORTA ////////

$("canvas[name ='pieChart']").each(function (index, b) {
   // var b = document.getElementById("pieChart");

    if (!(b == null)) {

        var FormVariables = '';

        if (b.dataset.visible == "true") {


            var valueForRuleQuery = $(b).attr("value-for-rule-query");
            if (valueForRuleQuery != "")
                FormVariables = ValuesFromTable(valueForRuleQuery);

            var legendVisible = true;
            if (b.dataset.legend != undefined)
                legendVisible = b.dataset.legend;


            var datalegend = $(b).attr("data-legend");
            if (datalegend != "" && datalegend == "false")
                legendVisible = false;

            var resultTorta = Report(parseInt(b.dataset.report), FormVariables);

            resultTorta = JSON.parse(resultTorta);

            var PositionXtorta = b.dataset.x;
            var PositionYtorta = b.dataset.y;

            var countItemXTorta = new Array();
            var countItemYTorta = new Array();



            for (var i = 0; i < resultTorta.length; i++) {

                countItemXTorta.push(resultTorta[i][PositionXtorta]);
                countItemYTorta.push(resultTorta[i][PositionYtorta]);

            }


            console.log(countItemXTorta);
            console.log(countItemYTorta);


            var myPieChart = new Chart(b, {
                type: 'pie',
                data: {
                    labels: countItemXTorta,
                    datasets: [{
                        label: 'Population',
                        data: countItemYTorta,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                            'rgba(255, 99, 132, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(54, 162, 235, 0.6)',
                            'rgba(255, 206, 86, 0.6)',
                            'rgba(75, 192, 192, 0.6)',
                            'rgba(153, 102, 255, 0.6)',
                            'rgba(255, 159, 64, 0.6)',
                        ],
                        borderWidth: 5,
                    }]
                }
                ,
                options: {
                    legend: {
                        display: legendVisible,
                    }
                }

            });

        } else {

            $("GraficoTorta").css("display", "none");
        }
    }
});
///////////// GRAFICO BARRA HORIZONTAL //////////////////


$("canvas[name ='densityChart']").each(function (index, c) {

  //  var c = document.getElementById("densityChart");

    if (!(c == null)) {

        var FormVariables = '';

        if (c.dataset.visible == "true") {


            var densityCanvas = document.getElementById("densityChart");

            var valueForRuleQuery = $(c).attr("value-for-rule-query");
            if (valueForRuleQuery != "")
                FormVariables = ValuesFromTable(valueForRuleQuery);

            var legendVisible = true;
            if (c.dataset.legend != undefined)
                legendVisible = c.dataset.legend;

            var datalegend = $(c).attr("data-legend");
            if (datalegend != "" && datalegend == "false")
                legendVisible = false;


            var resultBarraHor = Report(parseInt(c.dataset.report), FormVariables);
            resultBarraHor = JSON.parse(resultBarraHor);

            var PositionXbarraHor = c.dataset.x;
            var PositionYbarraHor = c.dataset.y;

            var arrayBarraHorx = new Array();
            var arrayBarraHorY = new Array();



            for (var i = 0; i < resultBarraHor.length; i++) {

                var IteraResultBarHor = resultBarraHor[i][PositionXbarraHor];
                arrayBarraHorx.push(IteraResultBarHor);

            }

            for (var i = 0; i < resultBarraHor.length; i++) {

                var IteraResultBarHor = resultBarraHor[i][PositionYbarraHor];
                arrayBarraHorY.push(IteraResultBarHor);

            }


            //var countItemXBarraHor = new Array();		
            //for (var i = 0; i < arrayBarraHor.length; i++) {

            //    if(!countItemXBarraHor.includes(arrayBarraHor[i])){ 
            //      countItemXBarraHor.push(arrayBarraHor[i]);
            //      }  
            //  } 	

            //var countItemYBarraHor = new Array();	
            //for (var i = 0; i < countItemXBarraHor.length; i++) {

            //    countItemYBarraHor.push(0);
            //    for (var y = 0; y < arrayBarraHor.length; y++) {

            //      if(arrayBarraHor[y] == countItemXBarraHor[i]){
            //        countItemYBarraHor[i] += 1;
            //      }

            //    }

            //  } 


            Chart.defaults.global.defaultFontFamily = "Lato";
            Chart.defaults.global.defaultFontSize = 18;

            var densityData = {
                label: PositionYbarraHor,
                data: arrayBarraHorY,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.6)',
                    'rgba(54, 162, 235, 0.6)',
                    'rgba(255, 206, 86, 0.6)',
                    'rgba(75, 192, 192, 0.6)',
                    'rgba(153, 102, 255, 0.6)',
                    'rgba(255, 159, 64, 0.6)',
                    'rgba(255, 99, 132, 0.6)',
                    'rgba(54, 162, 235, 0.6)',
                    'rgba(255, 206, 86, 0.6)',
                    'rgba(75, 192, 192, 0.6)',
                    'rgba(153, 102, 255, 0.6)'
                ],

                borderWidth: 2,
                hoverBorderWidth: 0
            };

            var chartOptions = {
                scales: {
                    yAxes: [{
                        barPercentage: 0.5
                    }]
                },
                elements: {
                    rectangle: {
                        borderSkipped: 'left',
                    }
                },
                legend: {
                    display: legendVisible,
                }
            };

            var barChart = new Chart(densityCanvas, {
                type: 'horizontalBar',
                data: {
                    labels: arrayBarraHorx,
                    datasets: [densityData],
                },
                options: chartOptions
            });

        } else {
            $("GraficoBarraHor").css("display", "none");
        }
    }
});


///////////////////////// GRAFICO DE LINEA /////////////////


$("canvas[name ='speedChart']").each(function (index, e) {
   // var e = document.getElementById("speedChart");

    if (!(e == null)) {

        var FormVariables = '';

        if (e.dataset.visible == "true") {

            var speedCanvas = document.getElementById("speedChart");

            var valueForRuleQuery = $(e).attr("value-for-rule-query");
            if (valueForRuleQuery != "")
                FormVariables = ValuesFromTable(valueForRuleQuery);

            var legendVisible = true;
            if (e.dataset.legend != undefined)
                legendVisible = e.dataset.legend;

            var datalegend = $(e).attr("data-legend");
            if (datalegend != "" && datalegend == "false")
                legendVisible = false;

            var resultLine = Report(parseInt(e.dataset.report), FormVariables);
            resultLine = JSON.parse(resultLine);

            var PositionXLine = e.dataset.x;

            var arrayBarraLine = new Array();


            for (var i = 0; i < resultLine.length; i++) {

                var IteraResultLine = resultLine[i][PositionXLine];
                arrayBarraLine.push(IteraResultLine);

            }

            var countItemXLine = new Array();
            for (var i = 0; i < arrayBarraLine.length; i++) {

                if (!countItemXLine.includes(arrayBarraLine[i])) {
                    countItemXLine.push(arrayBarraLine[i]);
                }
            }

            var countItemYLine = new Array();
            for (var i = 0; i < countItemXLine.length; i++) {

                countItemYLine.push(0);
                for (var y = 0; y < arrayBarraLine.length; y++) {

                    if (arrayBarraLine[y] == countItemXLine[i]) {
                        countItemYLine[i] += 1;
                    }

                }

            }


            Chart.defaults.global.defaultFontFamily = "Lato";
            Chart.defaults.global.defaultFontSize = 18;

            var speedData = {
                labels: countItemXLine,
                datasets: [{
                    label: PositionXLine,
                    data: countItemYLine,
                }]
            };

            var chartOptions = {
                legend: {
                    display: legendVisible,
                    position: 'top',
                    labels: {
                        boxWidth: 80,
                        fontColor: 'black'
                    }
                }
            };

            var lineChart = new Chart(speedCanvas, {
                type: 'line',
                data: speedData,
                options: chartOptions
            });
        } else {

            $("GraficoLinea").css("display", "none");
        }
    }
});
/////////// GRAFICO DOBLE LINEA //////



$("canvas[name ='speedChartDouble']").each(function (index, speedCanvas) {
   // var speedCanvas = document.getElementById("speedChartDouble");

    if (!(speedCanvas == null)) {

        var FormVariables = '';

        if (speedCanvas.dataset.visible == "true") {

            var resultLineDoble = Report(parseInt(speedCanvas.dataset.report));
            resultLineDoble = JSON.parse(resultLineDoble);

            var legendVisible = true;
            if (speedCanvas.dataset.legend != undefined)
                legendVisible = speedCanvas.dataset.legend;

            var datalegend = $(speedCanvas).attr("data-legend");
            if (datalegend != "" && datalegend == "false")
                legendVisible = false;


            var PositionXLineaDoble = speedCanvas.dataset.x;
            var PositionY1LineaDoble = speedCanvas.dataset.y1;
            var PositionY2LineaDoble = speedCanvas.dataset.y2;

            var arrayLineaDoblex = new Array();
            var arrayLineaDobley1 = new Array();
            var arrayLineaDobley2 = new Array();



            for (var i = 0; i < resultLineDoble.length; i++) {

                var IteraResultBarHor = resultLineDoble[i][PositionXLineaDoble];
                arrayLineaDoblex.push(IteraResultBarHor);

            }

            for (var i = 0; i < resultLineDoble.length; i++) {

                var IteraResultBarHor = resultLineDoble[i][PositionY1LineaDoble];
                arrayLineaDobley1.push(IteraResultBarHor);

            }

            for (var i = 0; i < resultLineDoble.length; i++) {

                var IteraResultBarHor = resultLineDoble[i][PositionY2LineaDoble];
                arrayLineaDobley2.push(IteraResultBarHor);

            }

            Chart.defaults.global.defaultFontFamily = "Lato";
            Chart.defaults.global.defaultFontSize = 18;

            var dataFirst = {
                label: PositionY2LineaDoble,
                data: arrayLineaDobley2,
                lineTension: 0,
                fill: false,
                borderColor: 'rgba(255, 99, 132, 0.6)'
            };

            var dataSecond = {
                label: PositionY1LineaDoble,
                data: arrayLineaDobley1,
                lineTension: 0,
                fill: false,
                borderColor: 'rgba(54, 162, 235, 0.6)'
            };

            var speedData = {
                labels: arrayLineaDoblex,
                datasets: [dataFirst, dataSecond]
            };

            var chartOptions = {
                legend: {
                    display: false,
                    drawOnChartArea: false,
                    position: 'top',
                    labels: {
                        boxWidth: 100,
                        fontColor: 'black'
                    }
                },
                scales: {
                    xAxes: [{
                        display: false,
                        gridLines: {
                            color: "rgba(0, 0, 0, 0)",
                            display: false
                        }
                    }],
                    yAxes: [{
                        display: false,
                        gridLines: {
                            color: "rgba(0, 0, 0, 0)",
                            display: false
                        }
                    }]
                }
            };

            var lineChart = new Chart(speedCanvas, {
                type: 'line',
                data: speedData,
                options: chartOptions
            });
        } else {
            $("GraficoDobleLinea").css("display", "none");
        }
    }
});


function ValuesFromTable(valueForRuleQuery) {
    var ResultValues = [];
    try {

        if (valueForRuleQuery != undefined) {
            var valor = valueForRuleQuery.split(",");

            for (var i = 0; i < valor.length; i++) {

                var columns = valor[i].split("=");

                var VarName = columns[0];
                var IndexValue = $("#" + columns[1]).val();

                ResultValues.push({ name: VarName, value: IndexValue });

                var jsonlist = JSON.stringify(ResultValues);
            }
        }
    } catch (e) {
        console.error(e);
    }
    return jsonlist;
}

