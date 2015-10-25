'use strict';
function routeService($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var opID = "F9688FDF-1BEF-4E32-B70C-4494856BB94D";
    var routeServiceFactory = {};
    routeServiceFactory.route = {
        "RouteId": "",
        "BusId": "",
        "Company": "",
        "Brand": "",
        "BrandDesc": "",
        "BusType": "",
        "BusTypeDesc": "",
        "VechiclePhoneNo": "",
        "Source_CityId": "",
        "SourceCity": "",
        "Destination_CityId": "",
        "DestinationCity": "",
        "Recurrsive": "",
        "RouteDate": "",
        "DepartureTime": "",
        "ArrivalTime": "",
        "RouteFare": ""
    }

    routeServiceFactory.loadBuses = function () {
        return $http.get(serviceBase + 'buses/operator/' + opID).success(function (data, status, headers, config) {
            return data;
        }).error(function (data, status, headers, config) {
            return null;
        });

    };
    routeServiceFactory.loadCities = function () {
        return $http.get(serviceBase + 'cities').success(function (data, status, headers, config) {
            return data;
        }).error(function (data, status, headers, config) {
            return null;
        });
    };


    routeServiceFactory.update = function (route) {
        return $http.put(serviceBase + 'routes/' + route.RouteId, route).
                success(function (response, status, headers, config) {
                    return response;
                }).
                error(function (error, status, headers, config) {
                    return error;
                });
    };

    routeServiceFactory.insert = function (route) {
        return $http.post(serviceBase + 'routes', route).
                success(function (response, status, headers, config) {
                    return response;

                }).
                error(function (error, status, headers, config) {
                    return error;
                });
    };

    routeServiceFactory.loadRouteList = function () {
        return $http.get(serviceBase + 'routes').success(function (response, status, headers, config) {
            return response;
        }).error(function (error, status, headers, config) {
            return error;
        });
    };
    return routeServiceFactory;
}