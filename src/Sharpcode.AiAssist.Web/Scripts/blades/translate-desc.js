angular.module('AiAssistModule')
    .controller('AiAssistModule.translateDescController', ['$scope', 'openAiService', 'platformWebApp.bladeNavigationService', '$timeout',
        function ($scope, openAiService, bladeNavigationService, $timeout) {
            var blade = $scope.blade;
            blade.isLoading = false;

            $scope.languages = blade.parentBlade.catalog.languages;
            $scope.isValid = false;

            blade.currentEntity = {}
            var currentEntity = blade.currentEntity
            currentEntity.content = blade.selectedContent.content;
            currentEntity.language = blade.selectedContent.languageCode;

            $scope.translate = function () {
                blade.isLoading = true;
                var translateRequest = {
                    "Prompt": blade.currentEntity.content,
                    "Language": blade.currentEntity.languageCode,
                }

                openAiService.translateDescription(translateRequest).then(
                    function (result) {
                        blade.isLoading = false;
                        $scope.result = result.data
                        if ($scope.result) {
                            $scope.isValid = true
                        }
                    })
            };

            $scope.saveChanges = function () {
                    
                var result = {
                    content: '',
                    languageCode: currentEntity.languageCode,
                    reviewType: blade.selectedContent.reviewType
                }
                var newBlade = {
                    id: 'editorialReview',
                    currentEntity: result,
                    item: blade.parentBlade.item,
                    catalog: blade.parentBlade.catalog,
                    languages: blade.parentBlade.catalog.languages,
                    controller: 'virtoCommerce.catalogModule.editorialReviewDetailController',
                    template: 'Modules/$(VirtoCommerce.Catalog)/Scripts/blades/editorialReview-detail.tpl.html',
                };

                bladeNavigationService.showBlade(newBlade, $scope.blade);
                $timeout(function () {
                    bladeNavigationService.currentBlade.currentEntity.content = $scope.result
                    bladeNavigationService.currentBlade.$scope.$broadcast('resetContent', { body: $scope.result });
                    blade.isLoading = false;
                });
                $scope.bladeClose();                
            }

            blade.toolbarCommands = [
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
            ];
        }
    ]);
