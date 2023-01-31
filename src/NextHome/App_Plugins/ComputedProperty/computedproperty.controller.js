angular.module("umbraco")
    .controller("ComputedProperty",
        function ($scope, $http, $rootScope, umbRequestHelper, editorState) {

            $scope.displayvalue = 0;

            if ($scope.model.config.formula != null && $scope.model.config.formula != '') {
                var fieldsArr = $scope.model.config.formula.split(/[-+*/]+/);
                for (var i = 0; i < fieldsArr.length; i++) {
                    var property = editorState.current.properties.find(function (prop) {
                        return prop.alias == fieldsArr[i].trim()
                    });
                    window[fieldsArr[i].trim()] = parseFloat(property.value);
                    inputFld = $('[for=' + fieldsArr[i].trim() + ']').parent().find('input.umb-editor')[0];
                    $(inputFld).change(fieldsArr[i].trim(), function (event) {
                        window[event.data] = parseFloat($(this).val());
                        calculateValue($scope.model.config.formula);
                    });
                }
                calculateValue($scope.model.config.formula);
            }

            function calculateValue(formula) {
                var result = parseFloat(eval(formula));
                $scope.displayvalue = Math.round(result * 100)/100;
            }
        });