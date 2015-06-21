(function () {

    angular.module('app')
        .controller('Route1', Route1);

    Route1.$inject = ['$log'];

    function Route1($log) {
        var vm = this;

        activate();

        function activate() {
            $log.info('Activated Route1 View');
        }
    }

})();