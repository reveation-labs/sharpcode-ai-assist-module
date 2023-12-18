// Call this to register your module to main application
var moduleName = 'OpenAiModule';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.OpenAiModuleState', {
                    url: '/OpenAiModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'OpenAiModule.helloWorldController',
                                template: 'Modules/$(SharpCode.OpenAiModule)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state', 'platformWebApp.toolbarService', 'platformWebApp.widgetService', 'platformWebApp.bladeNavigationService', 'openAiService',
        function (mainMenuService, $state, toolbarService, widgetService, bladeNavigationService, openAiService) {
            //Register module in main menu
            var generateBlade = {
                
                name: 'Generate',
                icon: 'fa fa-plus',
                index: 10,
                canExecuteMethod: function (blade) {
                    return true;
                },

                executeMethod: function (blade) {
                    var newBlade = {
                        id: 'generateDescBlade',
                        currentEntity: blade.currentEntity,
                        controller: 'OpenAiModule.generateDescController',
                        template: 'Modules/$(SharpCode.OpenAiModule)/Scripts/blades/generate-desc.tpl.html'
                    };
                    bladeNavigationService.showBlade(newBlade, blade);
                },
            };
            toolbarService.register(generateBlade, 'virtoCommerce.catalogModule.editorialReviewDetailController');

            var rephraseBlade = {

                name: 'Rephrase',
                icon: 'fa fa-paragraph',
                index: 10,
                canExecuteMethod: function (blade) {
                    return true;
                },

                executeMethod: function (blade) {
                    blade.isLoading = true;
                    var rephraseRequest = {
                        "Prompt": blade.currentEntity.content,
                    }
                    openAiService.rephraseDescription(rephraseRequest).then(
                        function (result) {
                            blade.isLoading = false;
                            $scope.result = result.data;
                        })
                }
            };
            toolbarService.register(rephraseBlade, 'virtoCommerce.catalogModule.editorialReviewDetailController');

            var translateBlade = {

                name: 'Translate',
                icon: 'fa fa-globe',
                index: 10,
                canExecuteMethod: function (blade) {
                    return true;
                },
                executeMethod: function (blade) {
                    blade.isLoading = true;
                    var translateRequest = {
                        "language": blade.currentEntity.languageCode,
                        "productId": blade.parentBlade.item.id
                    }
                    openAiService.translateDescription(translateRequest).then(
                        function (result) {
                            blade.isLoading = false;
                            blade.origEntity.content = result.data;            
                            blade.currentEntity.content = result.data
                            blade.scope.setForm(blade.currentEntity)
                            console.log(blade)
                        })
                },
                parentRefresh: function (data) { $scope.formScope.content = blade.currentEntity.content; },

            };
            toolbarService.register(translateBlade, 'virtoCommerce.catalogModule.editorialReviewDetailController');
        }
    ]);


