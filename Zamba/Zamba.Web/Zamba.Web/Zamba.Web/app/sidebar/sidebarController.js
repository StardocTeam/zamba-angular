
app.controller('sidebarCtrl', function ($scope, $timeout, $mdSidenav, ZambaTaskService, authService, Search, UserRightsFactory) {

    $scope.isLocked = false;
    $scope.states = ['unlocked', 'locked-expanded', 'locked-collapsed'];
    $scope.lockTitle = "";
    $scope.UserRightsFactory = UserRightsFactory;
    $scope.Environment = getValueFromWebConfig('Environment').toLowerCase();
    $scope.getSettingsClasses = function () {
        var classSettingsIsOpen = $scope.settingsIsOpen ? 'settingsIsOpenStyle' : 'settingsIsClosStyle';
        var classEnvironment = $scope.Environment == 'desa' ? 'sidenavColor-desa' : ($scope.Environment == 'test' ? 'sidenavColor-test' : 'sidenavColor-prod');

        return classSettingsIsOpen + ' ' + classEnvironment;
    }
    

    $scope.preventUserPlayWithButton = function () {
        $('#btnAnchorSidebar').hide();
        setTimeout(function () {
            $('#btnAnchorSidebar').show();
            $('.z-icon-anchor-sidebar').first().text('lock_open');
        }, 100);
    }

    $scope.$on('sidebarPermissionLoaded', function (event, args) {
    });


    $scope.$on('$viewContentLoaded', function () {
    });

    $scope.movePanel = function () {
        if ($scope.currentState == 'locked-expanded') {
            $('#zamba-sidepanel-control').removeClass('md-closed');
            $('#zamba-sidepanel-control').removeClass('z-sidepanel-unlocked');
            $('#pushPanel').addClass('pushedPanel');
            setTimeout(function () {
                $('#zamba-sidepanel-control').removeClass('md-closed');
            }, 100);
        }
        else if ($scope.currentState == 'unlocked') {
            $('#zamba-sidepanel-control').addClass('z-sidepanel-unlocking');
            $('#pushPanel').removeClass('pushedPanel');
            setTimeout(function () {
                $('#zamba-sidepanel-control').addClass('z-sidepanel-unlocked');
                $('#zamba-sidepanel-control').removeClass('z-sidepanel-unlocking');
            }, 100);
        } else {
            $('#zamba-sidepanel-control').addClass('z-sidepanel-unlocking');
            $('#pushPanel').removeClass('pushedPanel');
            setTimeout(function () {
                $('#zamba-sidepanel-control').removeClass('md-closed');
            }, 0);
        }
    };

    $scope.movePanelByLockedStateLoaded = function () {
        if ($scope.currentState == 'locked-expanded') {
            $('.z-button-icon-item-sidebar').removeClass('z-button-icon-item-sidebar-parent-hover');
            $('.z-button-text-item-sidebar').css('display', 'inline-block');//mostrar el texto de los botones de navegacion
            $('.z-userName').css('display', 'inline-block');
            $('.z-imgUserAvatar').removeClass('z-sidenav-avatar-mini');
            $('#avatarRow').removeClass('z-sidenav-avatar-justify-right');
            $('#zamba-sidepanel-control').removeClass('md-closed');
            $('#zamba-sidepanel-control').removeClass('z-sidepanel-unlocked');
            $('#pushPanel').addClass('pushedPanel');
            setTimeout(function () {
                $('#zamba-sidepanel-control').removeClass('md-closed');
            }, 0);
        }
        else if ($scope.currentState == 'unlocked') {
            $('#zamba-sidepanel-control').addClass('z-sidepanel-unlocking');
            $('#pushPanel').removeClass('pushedPanel');
            setTimeout(function () {
                $('#zamba-sidepanel-control').addClass('z-sidepanel-unlocked');
                $('#zamba-sidepanel-control').removeClass('z-sidepanel-unlocking');
            }, 0);
        } else {
            $('#zamba-sidepanel-control').addClass('z-sidepanel-unlocking');
            $('#pushPanel').removeClass('pushedPanel');
            setTimeout(function () {
                $('#zamba-sidepanel-control').removeClass('md-closed');
            }, 0);
        }
    }
    $scope.setIconState = function () {
        if ($scope.clicksCount == 0)
            $scope.preventUserPlayWithButton()
        else
            $('.z-icon-anchor-sidebar').first().text('lock');
    }

    $scope.LoadLockedState = function () {
        try {
            var LastLockedState = 2;// ZambaTaskService.GetSidebarLockedState();
            if (LastLockedState != undefined) {
                $scope.clicksCount = parseInt(LastLockedState);
            }
            if ($scope.clicksCount == 0) {
                $scope.lockTitle = "Bloquear 'Abierto'";
            } else if ($scope.clicksCount == 1) {
                $scope.lockTitle = "Bloquear 'Cerrado'"
            } else if ($scope.clicksCount == 2) {
                $scope.lockTitle = "Desbloquear"
            }
            if ($scope.clicksCount == 0)
                $('.z-icon-anchor-sidebar').first().text('lock_open');
            else
                $('.z-icon-anchor-sidebar').first().text('lock');

            $scope.currentState = $scope.states[$scope.clicksCount];

            //state locked-collapsed
            if ($scope.currentState == 'locked-collapsed')
                $('#zamba-sidepanel-control').removeClass('z-sidepanel-unlocked');

            setTimeout(function () {
                if ($scope.currentState == 'locked-expanded') {
                    $('.z-button-icon-item-sidebar').removeClass('z-button-icon-item-sidebar-parent-hover');
                    $('.z-button-text-item-sidebar').css('display', 'inline-block');//mostrar el texto de los botones de navegacion
                    $('.z-userName').css('display', 'inline-block');
                    $('.z-imgUserAvatar').removeClass('z-sidenav-avatar-mini');
                    $('#avatarRow').removeClass('z-sidenav-avatar-justify-right');
                }

            }, 0)

            $scope.movePanelByLockedStateLoaded();

        } catch (e) {
            console.error(e);
        }

    }

    $scope.SideBarButtonClicked = false;
    //Hover
    $('#zamba-sidepanel-control').hover(function () {

        if ($scope.currentState == 'locked-expanded') {
            $('.z-button-icon-item-sidebar').removeClass('z-button-icon-item-sidebar-parent-hover');
            $('.z-button-text-item-sidebar').css('display', 'inline-block');//mostrar el texto de los botones de navegacion
            $('.z-userName').css('display', 'inline-block');
            $('.z-imgUserAvatar').removeClass('z-sidenav-avatar-mini');
            $('#avatarRow').removeClass('z-sidenav-avatar-justify-right');
        }
        else {
            setTimeout(function () {
                if ($('#zamba-sidepanel-control').is(":hover") && ($scope.currentState == 'unlocked' || $scope.currentState == 'locked-expanded')) {
                    $('.z-button-icon-item-sidebar').removeClass('z-button-icon-item-sidebar-parent-hover');
                    $('.z-button-text-item-sidebar').css('display', 'inline-block');//mostrar el texto de los botones de navegacion
                    $('.z-userName').css('display', 'inline-block');
                    $('.z-imgUserAvatar').removeClass('z-sidenav-avatar-mini');
                    $('#avatarRow').removeClass('z-sidenav-avatar-justify-right');
                }
                $scope.SideBarButtonClicked = false;
            }, 0)
        }


    }, function () {
        //Leave hover
        setTimeout(function () {
            if (!$('#zamba-sidepanel-control').is(":hover")) {
                if (($scope.currentState == 'unlocked' || $scope.currentState == 'locked-collapsed') && $scope.currentState != 'locked-expanded') {
                    $('.z-button-icon-item-sidebar').addClass('z-button-icon-item-sidebar-parent-hover');
                    $('.z-button-text-item-sidebar').css('display', 'none');//ocultar el texto de los botones de navegacion
                    $('.z-userName').css('display', 'none');
                    $('.z-imgUserAvatar').addClass('z-sidenav-avatar-mini');
                    $('#avatarRow').addClass('z-sidenav-avatar-justify-right');
                }
            }
            $scope.SideBarButtonClicked = false;
        }, 0)
    });

    $scope.nextSidebarState = function () {
        $scope.clicksCount++;
        if ($scope.clicksCount == 3) {
            $scope.clicksCount = 0;
            $scope.lockTitle = "Bloquear 'Abierto'";
        } else if ($scope.clicksCount == 1) {
            $scope.lockTitle = "Bloquear 'Cerrado'"
        } else if ($scope.clicksCount == 2) {
            $scope.lockTitle = "Desbloquear"
        }
        //guarda el estado en la BD
        ZambaTaskService.SaveSidebarLockedState($scope.clicksCount);
        $scope.setIconState();
        $scope.currentState = $scope.states[$scope.clicksCount];
        if ($scope.clicksCount == 0 || $scope.clicksCount == 1) {
            $('.z-button-icon-item-sidebar').removeClass('z-button-icon-item-sidebar-parent-hover');
            $('.z-button-text-item-sidebar').css('display', 'inline-block');//mostrar el texto de los botones de navegacion
            $('.z-userName').css('display', 'inline-block');
            $('.z-imgUserAvatar').removeClass('z-sidenav-avatar-mini');
            $('#avatarRow').removeClass('z-sidenav-avatar-justify-right');
        }
    };





    $scope.setLockedState = function () {
        $scope.nextSidebarState();
        $scope.movePanel();
    }



    $scope.ThumbsPathHome = function () {
        var userid = GetUID();
        if (userid != undefined && userid > 0) {
            userid = parseInt(userid);
            if (window.localStorage) {
                var userPhoto = window.localStorage.getItem('userPhoto-' + userid);
                if (userPhoto != undefined && userPhoto != null) {
                    try {
                        var response = userPhoto;
                        $scope.thumphoto = userPhoto;

                        return response;

                    } catch (e) {
                        console.error(e);
                        var response = ZambaTaskService.LoadUserPhotoFromDB(userid);
                        $scope.thumphoto = response;
                        return response;
                    }
                }
                else {
                    var response = ZambaTaskService.LoadUserPhotoFromDB(userid);
                    $scope.thumphoto = response;
                    return response;
                }
            }
            else {
                var response = ZambaTaskService.LoadUserPhotoFromDB(userid);
                $scope.thumphoto = response;
                return response;
            }
        }

    };

    $scope.LoadUserNameFromDB = function () {

        var idusuario = GetUID();
        if (idusuario != undefined && idusuario > 0) {

            var response = ZambaTaskService.LoadUserNameFromDB(idusuario);
            var a = JSON.parse(response)
            $scope.CurrentUserName = a[0];
            $scope.CurrentApellido = a[1];
            $scope.CurrentUsuario = a[2];
            $scope.CurrentPuesto = a[3];
            $scope.CurrentTelefono = a[4];

            if (window.localStorage) {
                window.localStorage.setItem('UD|' + idusuario, a);
            }

            var name = $scope.CurrentUserName + ' ' + $scope.CurrentApellido;
            $scope.setInitials(name);
            $scope.setInitialsImage();
        }
    };

    $scope.setInitials = function (name) {
        try {
            var names = name.split(' ');
            $scope.initials = names[0].substring(0, 1).toUpperCase();

            if (names.length > 1) {
                $scope.initials += names[names.length - 1].substring(0, 1).toUpperCase();
            }

        } catch (e) {
            $scope.initials = '';
        }
    };

    $scope.setInitialsImage = function () {

        var canvas = document.createElement('canvas');
        canvas.style.display = 'none';
        canvas.width = '32';
        canvas.height = '32';
        document.body.appendChild(canvas);
        var context = canvas.getContext('2d');
        context.fillStyle = "#ffff";
        context.fillRect(0, 0, canvas.width, canvas.height);
        context.font = "16px Arial";
        context.fillStyle = "#337ab7";

        context.fillText($scope.initials, 3, 23);

        $scope.InitialsImage = canvas.toDataURL();
        document.body.removeChild(canvas);

    };
    $scope.ThumbsPathHome();
    $scope.LoadUserNameFromDB();

    $scope.getValueFromWebConfig = function (key) {
        var baseUrl = window.location.protocol + "//" + window.location.host  + "/" + window.location.pathname.split('/')[1];

        $.ajax({
            "async": false,
            "crossDomain": true,
            "url": baseUrl + "/Services/ViewsService.asmx/getValueFromWebConfig?key=" + key,
            "method": "GET",
            "headers": {
                "cache-control": "no-cache"
            },
            "success": function (response) {
                return response;
            },

            "error": function (response) { }
        });
    }

    $scope.Logout = function () {
        try {
            $scope.OpenedTasks.forEach(function (e) {
                e.close();
            })
        } catch (e) {
            console.error(e);
        }
        
        authService.logOut();
        window.onbeforeunload = function () {
            showedDialog = true;
        };
        sessionStorage?.removeItem('lastmainmenuitem|' + GetUID());
        var destinationURL = "../../views/Security/Login.aspx?ReturnUrl=" + window.location;
        document.location = destinationURL;
    }

    $scope.settingsIsOpen = false;

    $scope.hoverIn = function () {

        if (angular.element("#optionMenuItem").hasClass('settingsIsOpenStyle') == true)
            $scope.settingsIsOpen = false;

        if (angular.element("#optionMenuItem").hasClass('settingsIsClosStyle') == true)
            $scope.settingsIsOpen = true;
    }

    $scope.closeSettings = function () {

        $scope.settingsIsOpen = false;
    }


    $scope.getUserRightExisting = function (rightId, uid) {
        var existInLocalStorage = window.localStorage.getItem("getUserRight-" + rightId + "-" + uid);
        if (existInLocalStorage == undefined || existInLocalStorage == null)
            return false
        else
            return true;
    };



});


app.directive('zambaSidebar', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {

            $scope.zambaVersion = "Zamba Web Ver. " + getValueFromWebConfig("ZambaVersion");
            $scope.LoadLockedState();
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/sidebar/sidebartemplate.html?v=248')
    }
});