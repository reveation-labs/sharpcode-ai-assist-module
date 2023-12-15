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
    .run(['platformWebApp.mainMenuService', '$state', 'platformWebApp.toolbarService', 'platformWebApp.widgetService', 'platformWebApp.bladeNavigationService',
        function (mainMenuService, $state, toolbarService, widgetService, bladeNavigationService) {
            //Register module in main menu
            var bladesOpenAi = {
                name: 'AI Tools',
                icon: 'fa fa-lightbulb-o',
                index: 10,
                executeMethod: function (blade) {
                    var newBlade = {
                        id: 'generateDescBlade',
                        currentEntity: blade.currentEntity,
                        controller: 'OpenAiModule.generateDescController',
                        template: 'Modules/$(SharpCode.OpenAiModule)/Scripts/blades/generate-desc.tpl.html'
                    };
                    bladeNavigationService.showBlade(newBlade, blade);
                },

                canExecuteMethod: function (blade) {
                    return true;
                }
                
            };
            toolbarService.register(bladesOpenAi, 'virtoCommerce.catalogModule.editorialReviewsListController');
        }
    ]);


