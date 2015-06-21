(function() {
    'use strict';

    angular
        .module('app', ['ngRoute'])
        .config(route)
        .run();

    route.$inject = ['$routeProvider', '$locationProvider'];

    function route($routeProvider, $locationProvider) {

        $locationProvider
            .html5Mode({
                enabled: true
            });

        $routeProvider.
            when('/route1', {
                templateUrl: 'route1.html',
                controller: 'Route1',
                controllerAs: 'vm',
                bindToController: true
            }).
            when('/route2', {
                templateUrl: 'route2.html',
                controller: 'Route2',
                controllerAs: 'vm',
                bindToController: true
            }).
            otherwise({
                redirectTo: '/'
            });
    }

})();
