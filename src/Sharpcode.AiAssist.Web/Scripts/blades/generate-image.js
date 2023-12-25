angular.module('AiAssistModule')
    .controller('AiAssistModule.generateImageController', ['$scope', 'openAiService', 'FileUploader', 'platformWebApp.bladeNavigationService',
        function ($scope, openAiService, FileUploader, bladeNavigationService) {

            var blade = $scope.blade;
            blade.isLoading = false;

            $scope.models = ['Dall_e_2', 'Dall_e_3'];
            $scope.qualities = [];
            $scope.sizes = [];
            $scope.isValid = false

            blade.currentEntity = {}
            var currentEntity = blade.currentEntity

            currentEntity.prompt = "";
            currentEntity.selectedModel = '';
            currentEntity.selectedQuality = ''
            currentEntity.selectedSize = ''
            currentEntity.numOfImages = 1;

            currentEntity.selectedImages = []; 

            $scope.toggleSelection = function (image) {
                if (currentEntity.selectedImages[image]) {
                    if (currentEntity.selectedImages.indexOf(image) === -1) {
                        currentEntity.selectedImages.push(image);
                    }
                } else {
                    const index = currentEntity.selectedImages.indexOf(image);
                    if (index > -1) {
                        currentEntity.selectedImages.splice(index, 1);
                    }
                }
            };

            $scope.updateDropdownOptions = function () {
                if (currentEntity.selectedModel === 'Dall_e_2') {
                    $scope.qualities = ['standard'];
                    $scope.sizes = ['Size256x256', 'Size512x512', 'Size1024x1024'];
                    $scope.maxNumOfImages = 10
                } else if (currentEntity.selectedModel === 'Dall_e_3') {
                    $scope.qualities = ['standard', 'hd'];
                    $scope.sizes = ['Size1024x1792', 'Size1792x1024', 'Size1024x1024'];
                    $scope.maxNumOfImages = 1
                }
            };

            $scope.saveChanges = function () {
                var uploader = $scope.uploader = new FileUploader({
                    autoUpload: true
                });

                uploader.url = getImageUrl(blade.parentBlade.folderPath, blade.imageType).relative;
                async function dataURItoBlob(base64Data) {
                    const base64Response = await fetch(`data:image/webp;base64,${base64Data}`);
                    return await base64Response.blob();
                }
                angular.forEach(blade.currentEntity.selectedImages, function (base64String) {
                    dataURItoBlob(base64String).then(blob => {
                        var timestamp = new Date().getTime();
                        var file = new File([blob], `image_${timestamp}.webp`, { type: 'image/webp' });
                        uploader.addToQueue([file]);
                    })
                });

                uploader.onSuccessItem = function (fileItem, images, status) {
                    angular.forEach(images, function (image) {
                        image.isImage = true;
                        image.group = blade.imageType;
                        blade.parentBlade.currentEntities.push(image);
                    });
                    $scope.bladeClose();
                };

                uploader.onAfterAddingAll = function (addedItems) {
                    bladeNavigationService.setError(null, blade);                   
                };

                uploader.onErrorItem = function (element, response, status, headers) {
                    bladeNavigationService.setError(element._file.name + ' failed: ' + (response.message ? response.message : status), blade);
                };
            }

            function getImageUrl(path, imageType) {
                var folderUrl = 'catalog/' + (path + (imageType ? '/' + imageType : ''));
                return { folderUrl: folderUrl, relative: 'api/assets?folderUrl=' + folderUrl };
            }

            $scope.generate = function () {
                blade.isLoading = true;

                var generateImageRequest = {
                    "prompt": currentEntity.prompt,
                    "productId": currentEntity.useProductPropFlag ? blade.parentBlade.item.id : "",
                    "model": currentEntity.selectedModel,
                    "quality": currentEntity.selectedQuality,
                    "size": currentEntity.selectedSize,
                    "n": currentEntity.numOfImages,
                    "responseFormat": "b64_json"
                }

                openAiService.generateImage(generateImageRequest).then(
                    function (result) { 
                        blade.isLoading = false;
                        $scope.result = result.data;
                        $scope.isValid = true
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
                        return blade.currentEntity.prompt && blade.currentEntity.selectedModel && blade.currentEntity.selectedQuality && blade.currentEntity.selectedSize
                    }
                },
            ];

        }]);
