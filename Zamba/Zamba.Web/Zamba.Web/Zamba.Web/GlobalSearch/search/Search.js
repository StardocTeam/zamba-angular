var enableGlobalSearch = "true", zambaApplication ="ZambaSearch";

app.controller('searchController', function ($scope, $http) {
    //Se utiliza para la busqueda de tareas
    $scope.CheckIfExecuteSearchByQueryStringOrLoadDefaultView = function (d) {
       
        var parameters = [];

        function parameter(editMode, id, name, type, operator, value, placeholder, value2) {
            this.color = "b1";
            this.editMode = editMode;
            this.groupnum = 1;
            this.id = id;
            this.maingroup = type == 0 ? true : editMode;
            this.name = name || "";
            this.type = type;//0: palabra
            this.operator = operator || "=";
            this.placeholder = placeholder || "";
            this.value = value || "";
            this.value2 = value2 || "";
        }

        function GetQSAttribute(d) {

            var parameters = [];
            var parametersIndexs = [];

            var typesURL = URLParam.Types();
            var searchsURL = URLParam.Search();
            var attributesURL = URLParam.Attr();

            var types = typesURL.split(",")
            var attributes = attributesURL.split(",")
            var searchs = searchsURL.split(",")

            for (var i = 0; i < d.length; i++) {

                for (var h = 0; h < types.length; h++) {

                    var type = d[i];

                    if (types[h] == type.id) {

                        for (var j = 0; j < type.indexes.length; j++) {

                            for (var k = 0; k < attributes.length; k++) {

                                var index = type.indexes[j];
                                if (attributes[k] == index.id && parametersIndexs.indexOf(attributes[k]) == -1)
                                {
                                    if (attributes[k] == index.id && parametersIndexs.indexOf(attributes[k]) == -1) {
                                    parameters.push(new parameter(true, attributes[k], index.name, 2, "=", searchs[k]));
                                    parametersIndexs.push(attributes[k]);
                                    break;
                                }
                            }
                        }
                        parameters.push(new parameter(false, types[h], type.name, 1));
                        break;
                    }
                }
            }
            return parameters;
        }

        var URLParam = {
            Types: function () {
                return getUrlParameters().types || "";
            },
            Attr: function () {
                return getUrlParameters().attr || "";
            },
            Search: function () {
                return getUrlParameters().search || "";
            }
        };

        if (URLParam.Attr() == "" && URLParam.Types() == "") {
            //Busqueda solo por palabras
            var txt = URLParam.Search();
            if (txt)
                parameters.push(new parameter(false, 0, txt, 0, "Empieza", txt));
        }
        else {
            var parameters = GetQSAttribute(d);
            if (parameters == undefined) {
                bootbox.alert("Parametros incorrectos");
                return;
            }
        }
        if (parameters.length)
            angular.element($('#zambasearchcontrol')).scope().DoSearchByQS(parameters);

    };

});



var SearchConfig = {
    IsSearchConfig: function () {
        return true;
    },
    UserId: function () {
        return GetUID();
    }
};

//function GetUID() { return getUrlParameters().user || "" };

function getUrlParameters() {
    var pairs = window.location.search.substring(1).split(/[&?]/);
    var res = {}, i, pair;
    for (i = 0; i < pairs.length; i++) {
        pair = pairs[i].toLowerCase().split('=');
        if (pair[1])
            res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
    }
    return res;
}