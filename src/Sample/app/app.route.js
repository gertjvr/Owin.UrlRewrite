(function () {
    'use strict';

    angular
        .module('app')
        .run(appRun);

    appRun.$inject = ['routerHelper'];

    function appRun(routerHelper) {
        var otherwise = '/404';
        routerHelper.configureStates(getStates(), otherwise);
    }

    function getStates() {
        return [
            {
                state: 'home',
                config: {
                    url: '/',
                    templateUrl: 'home/home.html',
                    controller: 'Home',
                    controllerAs: 'vm',
                    bindToController: true
                }
            },
            {
                state: 'route1',
                config: {
                    url: '/route1',
                    templateUrl: 'route1/route1.html',
                    controller: 'Route1',
                    controllerAs: 'vm',
                    bindToController: true
                }
            },
            {
                state: 'route2',
                config: {
                    url: '/route2',
                    templateUrl: 'route2/route2.html',
                    controller: 'Route2',
                    controllerAs: 'vm',
                    bindToController: true
                }
            },
            {
                state: '404',
                config: {
                    url: 'core/404',
                    templateUrl: '404.html'
                }
            }
        ];
    }

})();
