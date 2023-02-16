'use strict';

app.controller('AutoCompleteController', function ($scope, $filter, $http, AutoCompleteServices, ZambaUserService) {
    var self = this;
    self.querySearch = querySearch;

    function querySearch(query, emailsList) {

        if ($scope.EmailsObtained.length != 0) {

            if (query == undefined)
                self.querySearchText = "";
            else {
                self.querySearchText = query.split(";")[query.split(";").length - 1]
            }

            emailsList.forEach(function (item, index) {
                emailsList[index] = item.trim();
            });

            var res = $scope.EmailsObtained.filter(item => !emailsList.includes(item.value));

            $scope.ListedEmails = res.filter(createFilterFor(self.querySearchText));

            //la devolucion determina si se muestra el listBox.
            if ($scope.ListedEmails.length > 0) {
                return true;
            } else {
                return false;
            }

        } else {
            return false;
        }
    }
    function createFilterFor(query) {
        var lowercaseQuery = query.trim().toLowerCase();

        return function filterFn(item) {
            return (item.value.indexOf(lowercaseQuery) === 0);
        };
    }

    //Validacion de los correos establecidos, escritos y/u obtenidos del listbox
    function ValidateEmails(EmailList, Validado, field) {
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

        if (EmailList != undefined) {
            if (EmailList != "") {
                EmailList.split(",").forEach(function (elem, index) {
                    if (Validado) {
                        if (elem.trim() != "") {
                            if (reg.test(elem.trim()) == false) {
                                console.log("Iteracion erronea: " + elem.trim());
                                ValMessage += "• " + field + ": Hay caracteres o correo no valido.\n";
                                Validado = false;
                            }
                        } else {
                            ValMessage += "• " + field + ": Ha escrito doble coma o una coma al final.\n";
                            Validado = false;
                        }
                    }
                });
            }
        }
        return Validado;
    }

    $scope.GetMails = function (DocIdsChecked) {
        return AutoCompleteServices.GetEmailsUsersOfTask(DocIdsChecked);
    }


    $scope.CollapsingCombo = function () {
        document.querySelector("#ListEmails" + $scope.attribute).style.display = "none";
        document.querySelector("#hidePanel").style.display = "none";

        if (document.querySelector("#hidePanelZip") != undefined)
            document.querySelector("#hidePanelZip").style.display = "none";
    }

    $scope.ExpandCombo = function ($event) {

        var input = event.target;
        $scope.selectionStart = input.selectionStart;
        $scope.Value = $scope.Value.replaceAll(',', ';');
        var emailsList = $scope.Value.split(';');

        var counterChar = 0;
        var startRead = 0;
        var endRead = 0;

        var i = 0;

        while (counterChar <= $scope.selectionStart) {
            startRead = counterChar;

            counterChar += emailsList[i].length + 1;

            endRead = counterChar;
            i++;
        }

        var search = emailsList[i - 1];
        search = search.trim();

        if (self.querySearch(search, emailsList)) {
            document.querySelector("#ListEmails" + $scope.attribute).style.display = "flex";
            document.querySelector("#hidePanel").style.display = "block";

            if (document.querySelector("#hidePanelZip") != undefined)
                document.querySelector("#hidePanelZip").style.display = "block";

        } else {
            document.querySelector("#ListEmails" + $scope.attribute).style.display = "none";
            document.querySelector("#hidePanel").style.display = "none";

            if (document.querySelector("#hidePanelZip") != undefined)
                document.querySelector("#hidePanelZip").style.display = "none";

        }
    }

    $scope.appendEmail = function (emailSelected) {
        var input = event.target;
        //$scope.selectionStart = input.selectionStart;
        $scope.Value = $scope.Value.replaceAll(',', ';');
        var emailsList = $scope.Value.split(';');

        var counterChar = 0;
        var startRead = 0;
        var endRead = 0;

        var i = 0;

        while (counterChar <= $scope.selectionStart) {
            startRead = counterChar;

            counterChar += emailsList[i].length + 1;

            endRead = counterChar;
            i++;
        }

        emailsList[i - 1] = emailSelected.value;

        emailsList.forEach(function (item, index) {
            emailsList[index] = item.trim();
        });

        var stringMails = emailsList.join('; ');
        $scope.Value = stringMails;

        document.querySelector("#ListEmails" + $scope.attribute).style.display = "none";
        document.querySelector("#hidePanel").style.display = "none";

        if (document.querySelector("#hidePanelZip") != undefined)
            document.querySelector("#hidePanelZip").style.display = "block";


        if (document.querySelector("#hidePanelZip") != undefined)
            document.querySelector("#hidePanelZip").style.display = "none";
    }

    function onblur() {
        document.querySelector("#ListEmails" + $scope.attribute).style.display = "none";
        document.querySelector("#hidePanel").style.display = "none";

        if (document.querySelector("#hidePanelZip") != undefined)
            document.querySelector("#hidePanelZip").style.display = "none";
    }

    $scope.$on('EmailsObtained', function (event, args) {
        $scope.EmailsObtained = args.Emails;
    });


});




app.directive('zambaAutoComplete', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.attribute = attributes.attribute;
            $scope.querySearchText;

            $scope.EmailsObtained = [];
            $scope.ListedEmails = [];
            $scope.selectionStart;

            $scope.Value = "";
            $scope.EstablishedEmailsList = [];
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/AutoComplete/AutoCompleteTemplate.html'),
    }
});