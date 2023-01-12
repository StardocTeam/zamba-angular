 var app = angular.module("app", ['chart.js']);
   
    app.controller("ControladorPrincipal",  function ($scope, $filter, $http, GraficoService) {

        var varFecha = [];
        var series = [];
        var ValorTable = "";

        // GRAFICO BARRAS 
        
        $scope.datos = [ [65], [50], [28], [90], [41], [12]];

        // GRAFICO TORTA 

        $scope.datosTorta = [65, 50, 28, 90, 41, 12];

        // GRAFICO RADAR 


        $scope.dataRadar = [[65, 59, 90, 81, 56, 55, 40]];

        //Se invoca el servico para obtener los resultatos

        var d = GraficoService.Report(parametroURL('id'), parametroURL('User'));
        var Results = JSON.parse(d);

        // Se invoca y mapea la fecha para el grafico de barras

        varFecha = ObtenerEtiquetas(Results);

        if (varFecha != undefined && varFecha != "" && varFecha != null) {

            let arrayFechas = varFecha.map((fechaActual) => new Date(fechaActual));
            var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
            var max = new Date(Math.max.apply(null, arrayFechas));
            var min = new Date(Math.min.apply(null, arrayFechas));
            // se cambia el formato de las fechas maximas y minimas
            $scope.etiquetas = [min.toLocaleDateString("es-ES", options), max.toLocaleDateString("es-ES", options)];

        }

        $scope.series = ObtenerSeries(Results);

        var datos = ObtenerDatos(Results, $scope.series);

        

        


        

       


       
        // Funcion para obtener rango de fechas
        function ObtenerEtiquetas(Results) {
            var arrayFecha = [];
            
            //Se debe agregar validaciones que correspondan para diferentes registros
            if (Results[0]["Fecha Solicitante"] != undefined) {
                for (var i = 0; i < Results.length; i++) {
                    var insert = Results[i]["Fecha Solicitante"];
                    arrayFecha.push(insert);
                }
            }
            return arrayFecha; 
        }

        // Funcion para obtener series
        function ObtenerSeries() {
            var arrayEtapa = [];
            if (Results[0]["Etapa"] != undefined) {
                ValorTable = "Etapa";
                for (var i = 0; i < Results.length; i++) {
                    var insert = Results[i]["Etapa"];
                    if (arrayEtapa.indexOf(insert) == -1) {
                        arrayEtapa.push(insert);
                    }   
                }
            }
            return arrayEtapa;
        }

        // Funcion para obtener Datos
        function ObtenerDatos(Results, series) {
            var arrayDatos = [];
            for (var i = 0; i < series.length; i++) {
                for (var f = 0; f < Results.length; f++) {
                    //var ValueSeries = series[f];
                    
                }
            }

            return arrayDatos;
        }

    });