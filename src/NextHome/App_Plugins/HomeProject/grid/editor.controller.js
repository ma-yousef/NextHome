angular.module("umbraco")
    .controller("HomeProject.GridEditor",
        function ($scope, dialogService) {

            if (!$scope.control.value) {
                $scope.control.value = {
                    projectImage: null,
                    imageUrl: null,
                    projectId: null,
                    projectName: null
                };
            }

            $scope.openSearchWindow = function () {
                //console.log($scope.model.config.vimeoClientId, $scope.model.config.vimeoClientSecret)
                //dialogService.open({
                //	// set the location of the view
                //	template: "/App_Plugins/HomeProject/dialog/search.html",
                //	// pass in data used in dialog
                //	dialogData: {
                //	},
                //	// function called when dialog is closed
                //	callback: function (value) {
                //                 $scope.control.projectName = value.Name;
                //                 $scope.control.projectId = value.Id;
                //	}
                //});


                dialogService.contentPicker({
                    multiPicker: false,
                    filterAdvanced: true,
                    filter: function (nodes) {
                        return nodes.metaData.contentType != 'project';
                    },
                    callback: function (data) {
                        $scope.control.value.projectName = data.name;
                        $scope.control.value.projectId = data.id;
                    }
                });
            }

            $scope.openMediaPicker = function () {

                //dialogService.mediaPicker({ callback: populatePicture });
                $scope.mediaPickerOverlay = {
                    view: "mediapicker",
                    title: "Select Picture",
                    startNodeId: 0,
                    multiPicker: false, // adjust as per your requirement
                    onlyImages: true, // adjust as per your requirement
                    disableFolderSelect: true, // adjust as per your requirement
                    show: true,
                    submit: function (model) {

                        // add your logic here if you using more than on image, for me its just 1st selected image

                        //populatePicture(model.selectedImages[0]); // you must have this function in your code to populate the image

                        $scope.control.value.projectImage = model.selectedImages[0].udi;
                        $scope.control.value.imageUrl = model.selectedImages[0].image;
                        $scope.mediaPickerOverlay.show = false;
                        $scope.mediaPickerOverlay = null;
                    }
                };
            }



        });