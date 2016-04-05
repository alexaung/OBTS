'use strict';
function authInterceptorService($q, $location, localStorageService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'amx 4d53bce03ec34c0a911182d4c228ee6c:ReOHzbo+HkQQJNjqSOikVf7sSrOqBlqlHYtiLxVQecY=:76bf692b345d491f9520683a5b673abe:1459850915&Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
};