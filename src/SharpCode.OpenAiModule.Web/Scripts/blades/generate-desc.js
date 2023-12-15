angular.module('OpenAiModule')
    .controller('OpenAiModule.generateDescController', ['$scope', 'openAiService', 'platformWebApp.toolbarService',
        function ($scope, openAiService, toolbarService) {
            $scope.prompt = ""
            $scope.language = "en-US"
            $scope.tone = "SEO-friendly"
            $scope.length = 100
            
            var blade = $scope.blade;
            blade.isLoading = false;

            $scope.lang = blade.parentBlade.catalog.languages;

            $scope.changePrompt = function (newPrompt) {
                $scope.prompt = newPrompt;
            }
            $scope.changeLanguage = function (language) {
                $scope.language = language;   
            }

            $scope.changeTone = function (tone) {
                $scope.tone = tone;
            }

            $scope.changeLength = function (length) {
                $scope.length = length;
            }

            $scope.generate = function () {
                blade.isLoading = true;
                openAiService.generateDescription($scope.prompt, $scope.language, "").then(
                    function (result) {
                        blade.isLoading = false;
                        $scope.result = result.data;
                    })
               
            };

            $scope.translate = function () {
                blade.isLoading = true;
                openAiService.translateDescription($scope.prompt, $scope.language).then(
                    function (result) {
                        blade.isLoading = false;
                        $scope.result = result.data;
                    })

            };

            $scope.rephrase = function () {
                blade.isLoading = true;
                openAiService.rephraseDescription($scope.prompt, $scope.tone).then(
                    function (result) {
                        blade.isLoading = false;
                        $scope.result = result.data;
                    })
            };

            blade.toolbarCommands = [
                {
                    name: 'Generate',
                    icon: 'fa fa-plus',
                    index: 10,
                    executeMethod: function (blade) {
                        $scope.generate();
                    },

                    canExecuteMethod: function (blade) {
                        return true;
                    }

                },
                {
                    name: 'Translate',
                    icon: 'fa fa-globe',
                    index: 10,
                    executeMethod: function (blade) {
                        $scope.translate();
                    },

                    canExecuteMethod: function (blade) {
                        return true;
                    }

                },
                {
                    name: 'Rephrase',
                    icon: 'fa fa-paragraph',
                    index: 10,
                    executeMethod: function (blade) {
                        $scope.rephrase();
                    },

                    canExecuteMethod: function (blade) {
                        return true;
                    }

                }
            ];
            
        }]);
