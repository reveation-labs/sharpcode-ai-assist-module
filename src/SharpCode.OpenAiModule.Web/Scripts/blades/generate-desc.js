angular.module('OpenAiModule')
    .controller('OpenAiModule.generateDescController', ['$scope', 'openAiService', 'platformWebApp.toolbarService', 'platformWebApp.settings', '$timeout',
        function ($scope, openAiService, toolbarService, settings ) {

            var blade = $scope.blade;
            var currentEntity = blade.currentEntity;
            blade.isLoading = false;

            currentEntity.language = blade.parentBlade.currentEntity.languageCode
            currentEntity.length = 100
            currentEntity.reviewType = blade.parentBlade.currentEntity.reviewType
            currentEntity.includeProductProperties = false
            $scope.languages = blade.parentBlade.languages;

            settings.getValues({ id: 'Catalog.EditorialReviewTypes' }, function (data) {
                $scope.reviewTypes = data;
                if (!blade.currentEntity.reviewType) {
                    blade.currentEntity.reviewType = $scope.types[0];
                }
            });

            $scope.generate = function () {
                blade.isLoading = true;

                var generateRequest = {
                    "prompt": currentEntity.prompt,
                    "language": currentEntity.language,
                    "productId": currentEntity.includeProductProperties ? blade.parentBlade.currentEntity.ProductId : "",
                    "descriptionType": currentEntity.reviewType,
                    "descriptionLength": currentEntity.length
                }

                openAiService.generateDescription(generateRequest).then(
                    function (result) {
                        blade.isLoading = false;
                        $scope.result = result.data;
                        console.log(blade)
                        blade.parentBlade.currentEntity.content = result.data

                        console.log(blade)
                    })

            };

            $scope.translate = function () {
                blade.isLoading = true;
                var translateRequest = {
                    "language": currentEntity.language,
                    "productId": blade.parentBlade.item.id
                }
                openAiService.translateDescription(translateRequest).then(
                    function (result) {
                        blade.isLoading = false;
                        $scope.result = result.data;
                    })

            };

            $scope.rephrase = function () {
                blade.isLoading = true;
                var rephraseRequest = {
                    "Prompt": currentEntity.prompt,
                }
                openAiService.rephraseDescription(rephraseRequest).then(
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
            ];
            
        }]);
