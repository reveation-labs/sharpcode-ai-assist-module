// Call this to register your module to main application
var moduleName = 'AiAssistModule';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.AiAssistModuleState', {
                    url: '/AiAssistModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state', 'platformWebApp.toolbarService', 'platformWebApp.widgetService', 'platformWebApp.bladeNavigationService', 'openAiService', '$timeout',
        function (mainMenuService, $state, toolbarService, widgetService, bladeNavigationService, openAiService, $timeout) {
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
                        controller: 'AiAssistModule.generateDescController',
                        template: 'Modules/$(Sharpcode.AiAssistModule)/Scripts/blades/generate-desc.tpl.html'
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
                    var newBlade = {
                        id: 'rephraseDescBlade',
                        currentEntity: blade.currentEntity,
                        controller: 'AiAssistModule.rephraseDescController',
                        template: 'Modules/$(Sharpcode.AiAssistModule)/Scripts/blades/rephrase-desc.tpl.html'
                    };
                    bladeNavigationService.showBlade(newBlade, blade);
                }
            };
            toolbarService.register(rephraseBlade, 'virtoCommerce.catalogModule.editorialReviewDetailController');

            var translateBlade = {
                name: 'Translate',
                icon: 'fa fa-globe',
                index: 10,
                canExecuteMethod: function (blade) {
                    if (!blade.$scope.gridApi) {
                        return false;
                    }
                    var selectedItemsLength = blade.$scope.gridApi.selection.getSelectedRows().length;
                    return selectedItemsLength == 1;
                },
                executeMethod: function (blade) {
                    var selectedItems = blade.$scope.gridApi.selection.getSelectedRows();
                    var newBlade = {
                        id: 'translateDescBlade',
                        currentEntity: blade.currentEntity,
                        selectedContent: selectedItems[0],
                        controller: 'AiAssistModule.translateDescController',
                        template: 'Modules/$(Sharpcode.AiAssistModule)/Scripts/blades/translate-desc.tpl.html'
                    };
                    bladeNavigationService.showBlade(newBlade, blade);
                }
            };
            toolbarService.register(translateBlade, 'virtoCommerce.catalogModule.editorialReviewsListController');

            var generateImageBlade = {
                name: 'Generate',
                icon: 'fa fa-magic',
                index: 10,
                canExecuteMethod: function (blade) {
                    return true;
                },
                executeMethod: function (blade) {                   
                    var newBlade = {
                        id: 'generateImageBlade',
                        controller: 'AiAssistModule.generateImageController',
                        template: 'Modules/$(Sharpcode.AiAssistModule)/Scripts/blades/generate-image.tpl.html'
                    };
                    bladeNavigationService.showBlade(newBlade, blade);
                },
            };
            toolbarService.register(generateImageBlade, 'virtoCommerce.catalogModule.imagesAddController');
        }
    ]);


