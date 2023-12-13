angular.module('AIModule')
    .factory('AIModule.webApi', ['$resource', function ($resource) {
        return $resource('api/ai-module');
    }]);
