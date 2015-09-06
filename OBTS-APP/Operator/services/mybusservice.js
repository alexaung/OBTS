'use strict';
function busService($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var busServiceFactory = {};
    busServiceFactory.loadBrand = function () {
        return $http.get(serviceBase + 'codetables/title/brand').success(function (data, status, headers, config) {
            return data;
        }).error(function (data, status, headers, config) {
            return null;
        });

    };
    busServiceFactory.loadBusType = function () {
        return $http.get(serviceBase + 'codetables/title/bustype').success(function (data, status, headers, config) {
            return data;
        }).error(function (data, status, headers, config) {
            return null;
        });
    };
    
    busServiceFactory.update = function (bus) {
        return $http.put(serviceBase + 'buses/' + bus.BusId, bus).
                success(function (response, status, headers, config) {
                    return response;
                }).
                error(function (error, status, headers, config) {
                    return error;
                });
    };
    busServiceFactory.insert = function (bus) {
        return $http.post(serviceBase + 'buses', bus).
                success(function (response, status, headers, config) {
                    return response;
                    
                }).
                error(function (error, status, headers, config) {
                    return error;
                });
    };
    return busServiceFactory;
}