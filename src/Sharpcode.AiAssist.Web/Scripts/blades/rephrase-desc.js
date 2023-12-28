angular.module('AiAssistModule')
    .controller('AiAssistModule.rephraseDescController', ['$scope', 'openAiService', 'platformWebApp.bladeNavigationService', '$timeout',
        function ($scope, openAiService, bladeNavigationService, $timeout) {
            var blade = $scope.blade;
            blade.isLoading = false;
            $scope.isValid = false;

            blade.currentEntity = {}
            var currentEntity = blade.currentEntity
            currentEntity.content = blade.parentBlade.currentEntity.content;

            $scope.saveChanges = function () {
                $timeout(function () {
                    blade.parentBlade.$scope.$broadcast('resetContent', { body: $scope.result });
                    blade.isLoading = false;
                });
                $scope.bladeClose();
            }

            $scope.rephrase = function () {
                blade.isLoading = true;
                var rephraseRequest = {
                    "Prompt": blade.currentEntity.content,
                }
                openAiService.rephraseDescription(rephraseRequest).then(
                    function (result) {
                        blade.isLoading = false;
                        if (result.data) {
                            $scope.isValid = true
                        }
                        $timeout(function () {
                            var cleanedContent = result.data.replace(/html/g, '').replace(/```/g, '');
                            $scope.result = cleanedContent
                            $scope.$broadcast('resetContent', { body: $scope.result });
                            blade.isLoading = false;
                        });                       
                    })
            };

            blade.toolbarCommands = [
                {
                    name: 'Rephrase',
                    icon: 'fa fa-paragraph',
                    index: 10,
                    executeMethod: function (blade) {
                        $scope.rephrase();
                    },
                    canExecuteMethod: function (blade) {
                        return blade.currentEntity.content;
                    }

                },
            ];
        }
    ]);
