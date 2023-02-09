app.run(['$http', '$rootScope', 'authService', function ($http, $rootScope, authService     ) {
    try {
        authService.checkToken();

    } catch (e) {
        console.log(e.message);
        authService.logOut();
        return;
    }
}]);

app.config(['$httpProvider', function ($httpProvider) {
    
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
    
}
]);
