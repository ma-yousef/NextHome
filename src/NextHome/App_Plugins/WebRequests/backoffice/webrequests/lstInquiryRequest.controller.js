angular.module('umbraco')
    .controller('lstInquiryRequestController', function ($scope, apiHelper, notificationsService, dialogService) {

        $scope.selectedIds = [];

        $scope.currentPage = 1;
        $scope.itemsPerPage = 10;
        $scope.totalPages = 1;

        $scope.reverse = true;

        $scope.searchTerm = "";
        $scope.predicate = 'id';

        function fetchData() {
            apiHelper.getPagedRequests($scope.itemsPerPage, $scope.currentPage, $scope.predicate, $scope.reverse ? "desc" : "asc", $scope.searchTerm).then(function (response) {
                $scope.people = response.data.Items;
                $scope.totalPages = response.data.TotalPages;
            }, function (response) {
                notificationsService.error("Error", "Could not load data");
            });
        };

        $scope.order = function (predicate) {
            $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
            $scope.predicate = predicate;
            $scope.currentPage = 1;
            fetchData();
        };

        $scope.toggleSelection = function (val) {
            var idx = $scope.selectedIds.indexOf(val);
            if (idx > -1) {
                $scope.selectedIds.splice(idx, 1);
            } else {
                $scope.selectedIds.push(val);
            }
        };

        $scope.isRowSelected = function (id) {
            return $scope.selectedIds.indexOf(id) > -1;
        };

        $scope.isAnythingSelected = function () {
            return $scope.selectedIds.length > 0;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 1) {
                $scope.currentPage--;
                fetchData();
            }
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.totalPages) {
                $scope.currentPage++;
                fetchData();
            }
        };

        $scope.setPage = function (pageNumber) {
            $scope.currentPage = pageNumber;
            fetchData();
        };

        $scope.search = function (searchFilter) {
            $scope.searchTerm = searchFilter;
            $scope.currentPage = 1;
            fetchData();
        };

        $scope.delete = function () {
            if (confirm("Are you sure you want to delete " + $scope.selectedIds.length + " calendar?")) {
                $scope.actionInProgress = true;

                //TODO: do the real deleting here
                //This should be done by calling the api controller with the apiHelper using $scope.selectedIds

                $scope.people = _.reject($scope.people, function (el) { return $scope.selectedIds.indexOf(el.id) > -1; });
                $scope.selectedIds = [];
                $scope.actionInProgress = false;
            }
        };

        $scope.openDetails = function (req) {
            
            var ds = dialogService.open({
                template: '../App_Plugins/WebRequests/backoffice/webrequests/inquiryRequestDetails.html',
                dialogData: { request : req },
                $scope: $scope,
                callback: function (data) {
                    console.log(data);
                },
                show: true
            });
        }

        fetchData();
    });