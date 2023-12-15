angular.module('OpenAiModule').service('openAiService', ['$http', function ($http, $localStorage) {
    return {
        generateDescription: function (prompt, language, productId) {
            return $http.get('api/openai/generate?prompt=' + prompt + '&language=' + language + '&productId=' + productId);
        },
        translateDescription: function (text, language) {
            return $http.get('api/openai/translate?text=' + text + '&language=' + language);
        },
        rephraseDescription: function (text, tone) {
            return $http.get('api/openai/rephrase?text=' + text + '&tone=' + tone);
        }
    }
}]);
