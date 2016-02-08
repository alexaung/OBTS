'use strict';
function bookingService($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var opID = "F9688FDF-1BEF-4E32-B70C-4494856BB94D";
    var bookingServiceFactory = {};
    bookingServiceFactory.booking = {
        "BookingId": "",
        "BookingRefId": "",
        "UserId": "",
        "BookingOn": "",
        "MainContact": "",
        "Email": "",
        "ContactNo": "",
        "Cupon": "",
        "Discount": "",
        "CreatedUtc": "",
        "ModifiedUtc": "",
        "CreatedBy": "",
        "ModifiedBy": ""
    }

    bookingServiceFactory.update = function (booking) {
        return $http.put(serviceBase + 'Bookings/' + booking.BookingId, booking).
                success(function (response, status, headers, config) {
                    return response;
                }).
                error(function (error, status, headers, config) {
                    return error;
                });
    };

    bookingServiceFactory.insert = function (booking) {
        return $http.post(serviceBase + 'Bookings', booking).
                success(function (response, status, headers, config) {
                    return response;

                }).
                error(function (error, status, headers, config) {
                    return error;
                });
    };
   
    return bookingServiceFactory;
}