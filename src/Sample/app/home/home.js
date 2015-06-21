(function () {

    angular.module('app')
        .controller('Home', Home);

    Home.$inject = ['$log'];

    function Home($log) {
        var vm = this;

        activate();

        function activate() {
            $log.info('Activated Home View');
        }
    }

})();