

/**
 * CalendarCtrl - Controller for Calendar
 * Store data events for calendar
 */
function searchCtrl($scope, $http, routeService) {

    $scope.initSearch = function () {
        routeService.loadCities().success(function (response) {
            $scope.cities = response
        }).error(function (err) {
            $scope.message = err.error_description;
        });
    }

    $scope.reTweet = function () {
        var sourcecity = search.Source_CityId;
        search.Source_CityId = search.Destination_CityId;
        search.Destination_CityId = sourcecity;
    }
   
}
