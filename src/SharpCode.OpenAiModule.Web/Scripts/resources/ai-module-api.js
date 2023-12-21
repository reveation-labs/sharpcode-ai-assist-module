angular.module('OpenAiModule').service('openAiService', ['$http', function ($http, $localStorage) {
    return {
        generateDescription: function (generateRequest) {
            return $http.post('api/openai/generate', generateRequest);
        },
        translateDescription: function (translateRequest) {
            return $http.post('api/openai/translate', translateRequest);
        },
        rephraseDescription: function (rephraseRequest) {
            return $http.post('api/openai/rephrase', rephraseRequest);
        },
        generateImage: function (generateImageRequest) {
            return $http.post('api/openai/generate-image', generateImageRequest);
        }
    }
}]);
