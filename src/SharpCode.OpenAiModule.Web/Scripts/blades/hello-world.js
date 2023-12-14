angular.module('AIModule')
    .controller('AIModule.helloWorldController', ['$scope', 'AIModule.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'AIModule';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'AIModule.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
