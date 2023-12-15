angular.module('OpenAiModule')
    .controller('OpenAiModule.helloWorldController', ['$scope', 'OpenAiModule.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'OpenAiModule';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'OpenAiModule.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
