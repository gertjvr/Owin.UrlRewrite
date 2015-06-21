(function () {

    angular.module('app')
        .controller('Route2', Route2);

    Route2.$inject = ['$log'];

    function Route2($log) {
        var vm = this;

        activate();

        function activate() {
            $log.info('Activated Route2 View');
        }
    }

})();