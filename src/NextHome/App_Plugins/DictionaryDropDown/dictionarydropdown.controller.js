angular.module("umbraco")
    .controller("DictionaryDropDown",
        function ($scope, $http, $rootScope, umbRequestHelper, editorState) {

            $scope.key = editorState.current.key;

            if ($scope.model.config.dictionaryKey != null && $scope.model.config.dictionaryKey != '') {
                getDictionaryItems($scope.model.config.dictionaryKey);
            }

            if ($scope.model.config.parentProperty != null && $scope.model.config.parentProperty != '') {
                $scope.$on($scope.model.config.parentProperty.toLowerCase() + '_Changed', function (event, args) {
                    if (args == null || args == '')
                        $scope.dictionaryItems = [];
                    else
                        getDictionaryItems(args.Key);
                });
            }

            function getDictionaryItems(dictionaryKey) {

                umbRequestHelper.resourcePromise(
                    $http.get('backoffice/NH/DictionaryDropDown/GetDictionaryItems?dictionaryKey=' + dictionaryKey + '&parentContentId=' + editorState.current.parentId),
                    "Failed to retrieve all Person data").then(function (data) {
                        $scope.dictionaryItems = data;
                        $scope.dictionaryDD_changed()
                    });
            }

            $scope.dictionaryDD_changed = function () {
                $rootScope.$broadcast($scope.model.alias.toLowerCase() + '_Changed', $scope.model.value);
            }
        });