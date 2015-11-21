'use strict';
function busService($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var busServiceFactory = {};
    busServiceFactory.bus = {
        "BusId": "",
        "Brand": "",
        "BusType": "",
        "RegistrationNo": "",
        "PermitNumber": "",
        "PermitRenewDate": "",
        "InsurancePolicyNumber": "",
        "InsuranceCompany": "",
        "InsuranceValidFrom": "",
        "InsuranceValidTo": "",
        "VechiclePhoneNo": "",
        "DriverName": "",
        "Description": "",
        "Status": "",
        "OperatorId": ""
    }

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

    busServiceFactory.loadBusList = function () {
        return $http.get(serviceBase + 'buses').success(function (response, status, headers, config) {
                    return response;
                }).error(function (error, status, headers, config) {
                    return error;
                });
    };


    busServiceFactory.loadBusSeats = function (busid) {
        return $http.get(serviceBase + 'bus/'+busid+'/seats').success(function (response, status, headers, config) {
            return response;
        }).error(function (error, status, headers, config) {
            return error;
        });
    };

    busServiceFactory.bulkInsertSeats = function (seats) {
        return $http.post(serviceBase + 'seats/BulkInsertSeats' , seats).
                success(function (response, status, headers, config) {
                    return response;
                }).
                error(function (error, status, headers, config) {
                    return error;
                });
    };

    return busServiceFactory;
}