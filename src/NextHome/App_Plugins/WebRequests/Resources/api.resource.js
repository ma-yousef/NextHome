angular.module("umbraco.resources")
    .factory("apiHelper", function ($http) {
        return {
            getallRequests: function () {
                return $http.get(Umbraco.Sys.ServerVariables.WebRequests.WRApiUrl + "GetAllRquests");
            },

            getPagedRequests: function (itemsPerPage, pageNumber, sortColumn, sortOrder, searchTerm) {
                if (sortColumn == undefined)
                    sortColumn = "";
                if (sortOrder == undefined)
                    sortOrder = "";
                return $http.get(Umbraco.Sys.ServerVariables.WebRequests.WRApiUrl + "GetPagedRequests?itemsPerPage=" + itemsPerPage + "&pageNumber=" + pageNumber + "&sortColumn=" + sortColumn + "&sortOrder=" + sortOrder + "&searchTerm=" + searchTerm);
            },

            getPagedApplicants: function (itemsPerPage, pageNumber, sortColumn, sortOrder, searchTerm) {
                if (sortColumn == undefined)
                    sortColumn = "";
                if (sortOrder == undefined)
                    sortOrder = "";
                return $http.get(Umbraco.Sys.ServerVariables.WebRequests.WRApiUrl + "GetPagedApplicants?itemsPerPage=" + itemsPerPage + "&pageNumber=" + pageNumber + "&sortColumn=" + sortColumn + "&sortOrder=" + sortOrder + "&searchTerm=" + searchTerm);
            }
        };
    });