
angular.module('umbraco')
    .controller('HomeProject.Search',
    function ($http, $scope, notificationsService, searchService) {
            $scope.errorStr = null;
            $scope.model = {
                searchTerm: "a",
            }
            
            $scope.results = [];

            function init() {
                $scope.search(); //Run defaut search to get list of projects
            }

            function ShowError(title, msg) {
                $scope.errorStr = {
                    title: title,
                    message: msg
                }
            }

            $scope.search = function () {
                $scope.errorStr = null;
                searchService.searchContent({ term: $scope.model.searchTerm }).then(function (results) {
                    $scope.results = results;
                });

            }

            init();
        });