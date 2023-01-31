﻿angular.module('umbraco').directive('resultline', function (localizationService) {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App_Plugins/RankOne/directives/resultline/resultline.directive.html',
        scope: {
            resultline: '=',
            result: '='
        },
        link: function (scope) {
            scope.text = localizationService.localize(scope.result.Alias + '_' + scope.resultline.Alias, scope.resultline.Tokens);

            if (scope.resultline.Type === "success") {
                scope.style = "info";
            }

            if (scope.resultline.Type === "error") {
                scope.style = "error";
            }

            if (scope.resultline.Type === "warning") {
                scope.style = "warning";
            }

            if (scope.resultline.Type === "hint") {
                scope.style = "pointer";
            }
        }
    }
});