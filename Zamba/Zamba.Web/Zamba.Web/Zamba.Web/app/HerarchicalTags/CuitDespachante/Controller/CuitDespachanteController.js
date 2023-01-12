//var app = angular.module("ZambaHerarchicalTagsApp", []);

app.controller('ZambaHerarchicalTagsController', function ($scope, $filter, $http, ZambaHerarchicalTagsService) {


    $scope.getArrayValues = function (parentValue) {
        var options = [];
        var userId = parseInt(GetUID());

        var herarchicalValues = ZambaHerarchicalTagsService.getHerarchicalValues(userId, parentValue);
        var herarchicalValuesToObject = JSON.parse(herarchicalValues);

        if (herarchicalValuesToObject != null) {
            if (herarchicalValuesToObject.length > 0) {
                for (var item in herarchicalValuesToObject) {
                    options.push(herarchicalValuesToObject[item].Items);
                }
            }
        }

        return options;
    };

    $scope.setChildList = function (parentId) {
        var target = $('#zamba_index_' + parentId);
        var childeNodeNumbreFromParent = target.attr("childindexid");
        var childNode = $('#zamba_index_' + childeNodeNumbreFromParent);
        var childOptions = {};

        var oldvalue = childNode.val();

        cleanOptions(childNode)

        if (target.val() == null || target.val() == "") {
            childNode.prop('disabled', true);
        } else {
            childNode.prop('disabled', false);
            childOptions = $scope.getArrayValues(target.val());
        }

        setDropDownValues(childNode, childOptions, oldvalue);
    }

    function cleanOptions(node) {
        node.empty();
    }


    function setDropDownValues(tagId, options, oldvalue) {

        var optionsAsString = '';
        var hasValue = false;
        for (var i = 0; i < options.length; i++) {
            if (oldvalue != undefined && oldvalue != '' && oldvalue == options[i].split("-")[0].trim()) {
                
                optionsAsString += "<option value='" + options[i].split("-")[0].trim() + "' selected>" + options[i] + "</option>";
                hasValue = true;
            }
            else {
                optionsAsString += "<option value='" + options[i] + "'>" + options[i] + "</option>";
            }
        }
        if (hasValue) {
            optionsAsString = "<option value='A definir'>A definir</option>" + optionsAsString;
        } else {
            optionsAsString = "<option value='A definir' selected>A definir</option>" + optionsAsString;
        }
        tagId.append(optionsAsString);
    }


});