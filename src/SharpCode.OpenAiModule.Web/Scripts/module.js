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
                    console.log(blade)
                    blade.isLoading = true;
                    var rephraseRequest = {
                        "Prompt": blade.currentEntity.content,
                    }
                    openAiService.rephraseDescription(rephraseRequest).then(
                        function (result) {
                            $timeout(function () {
                                blade.$scope.$broadcast('resetContent', { body: result.data });
                                blade.isLoading = false;
                            });
                            
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
                    console.log(blade)
                    blade.isLoading = true;

                    var translateRequest = {
                        "Prompt": blade.currentEntity.content,
                        "Language": blade.currentEntity.languageCode,
                        "ProductId" : ""
                    }

                    if (!translateRequest.Prompt || translateRequest.Prompt == "") {
                        translateRequest.ProductId = blade.item.id;
                    }

                    openAiService.translateDescription(translateRequest).then(
                        function (result) {
                            $timeout(function () {
                                blade.$scope.$broadcast('resetContent', { body: result.data });
                                blade.isLoading = false;
                            });

                        })
                }

            };
            toolbarService.register(translateBlade, 'virtoCommerce.catalogModule.editorialReviewDetailController');

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
                        controller: 'OpenAiModule.generateImageController',
                        template: 'Modules/$(SharpCode.OpenAiModule)/Scripts/blades/generate-image.tpl.html'
                    };
                    bladeNavigationService.showBlade(newBlade, blade);
                },
            };
            toolbarService.register(generateImageBlade, 'virtoCommerce.catalogModule.imagesAddController');
        }
    ]);


