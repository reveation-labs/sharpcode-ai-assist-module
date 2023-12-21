angular.module('OpenAiModule')
    .controller('OpenAiModule.generateDescController', ['$scope', 'openAiService', 'platformWebApp.toolbarService', 'platformWebApp.settings', '$timeout',
        function ($scope, openAiService, toolbarService, settings, $timeout ) {

            var blade = $scope.blade;
            var currentEntity = blade.currentEntity;
            blade.isLoading = false;

            currentEntity.language = blade.parentBlade.currentEntity.languageCode
            currentEntity.length = 100
            currentEntity.reviewType = blade.parentBlade.currentEntity.reviewType
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
                    "productId": currentEntity.useProductPropFlag ? blade.parentBlade.item.id : "",
                    "descriptionType": currentEntity.reviewType,
                    "descriptionLength": currentEntity.length
                }

                openAiService.generateDescription(generateRequest).then(
                    function (result) {
                        blade.isLoading = false;
                        $scope.result = result.data;
                        $timeout(function () {
                            blade.parentBlade.$scope.$broadcast('resetContent', { body: result.data });
                            blade.isLoading = false;
                        });

                        console.log(blade)
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
