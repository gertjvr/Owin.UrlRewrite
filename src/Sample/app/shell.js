(function() {

    angular.module('app')
        .controller('Shell', Shell);

    Shell.$inject = ['$log', '$location'];

    function Shell($log, $location) {
        var vm = this;

        activate();

        function activate() {
            $log.info('Activated Shell View');

            var current = $location.path();
            $location.path("/");
            $log.info('location:' + $location.path());
            $location.path(current);
            $log.info('location:' + $location.path());
        }
    }

})();
