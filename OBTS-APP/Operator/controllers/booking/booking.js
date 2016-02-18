

/**
 * CalendarCtrl - Controller for Calendar
 * Store data events for calendar
 */
function bookingCtrl($scope, $http, notify, routeService, bookingService) {

    $scope.SeatState = {
        Available: 0,
        UnConfirm: 2,
        Selected: 8
    }
    $scope.BookingState = {
        Confirmed:0,
        UnConfirm:1,
        Cancelled:3
    }
    
    $scope.initBookingDetails = function () {
        $scope.booking = bookingService.booking;
    }
    $scope.save = function () {

        if (this.bookingdetail.$valid) {
            $scope.booking.BookingStatus = $scope.BookingState.Confirmed;
            if ($scope.booking.BookingId) {

                bookingService.update($scope.booking).success(function (response) {
                    notify({ message: 'Booking information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                   
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving booking informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
            else {
                bookingService.insert($scope.booking).success(function (response) {
                    notify({ message: 'Booking information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving booking informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
        }
        else {
            this.bookingdetail.submitted = true;
        }

    };

}
