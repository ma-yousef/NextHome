angular.module('umbraco')
    .controller('inquiryRequestDetailsController', function ($scope, dialogService) {
        $scope.model = { title: 'Request Details' };

        $scope.close = function () {
            dialogService.closeAll();
        }
    });