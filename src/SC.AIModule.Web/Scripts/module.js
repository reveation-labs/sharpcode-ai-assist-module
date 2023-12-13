// Call this to register your module to main application
var moduleName = 'AIModule';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.AIModuleState', {
                    url: '/AIModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'AIModule.helloWorldController',
                                template: 'Modules/$(SC.AIModule)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/AIModule',
                icon: 'fa fa-cube',
                title: 'AIModule',
                priority: 100,
                action: function () { $state.go('workspace.AIModuleState'); },
                permission: 'AIModule:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
