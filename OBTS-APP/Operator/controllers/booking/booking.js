

/**
 * CalendarCtrl - Controller for Calendar
 * Store data events for calendar
 */
function bookingCtrl($scope, $http, notify, routeService, bookingService) {

    
    $scope.initBookingDetails = function () {
        $scope.booking = bookingService.booking;
    }

}
