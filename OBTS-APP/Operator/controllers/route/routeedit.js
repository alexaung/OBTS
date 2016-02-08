/**
 * routeEditCtrl - Controller used to run modal view
 * used in Basic form view
 */
'use strict';
function routeEditCtrl($scope, $modalInstance, $http, notify, routeService) {


    notify.config({
        duration: '1500'
    });

    $scope.statusTypes = [{ KeyCode: true, Value: "Active" }, { KeyCode: true, Value: "Inactive" }]
    
    $scope.initRouteEdit = function (scope) {
        this.formScope = scope;
        this.formScope.recursiveWeekly = false;
        $scope.route = routeService.route;
        $scope.route.DepartureDateTime = new Date("01-Jan-1900 " + $scope.route.DepartureTime)
        $scope.route.ArrivalDateTime = new Date("01-Jan-1900 " + $scope.route.ArrivalTime)
        
        routeService.route = {};
        $scope.route.OperatorId = "F9688FDF-1BEF-4E32-B70C-4494856BB94D";


        routeService.loadBuses().success(function (response) {
            $scope.buses = response
        }).error(function (err) {
            $scope.message = err.error_description;
        });

        routeService.loadCities().success(function (response) {
            $scope.cities = response
        }).error(function (err) {
            $scope.message = err.error_description;
        });


    }

    $scope.save = function () {

        if (this.formScope.routeEdit.$valid) {
            $scope.route.DepartureTime = $scope.route.DepartureDateTime.getHours() + ":" + $scope.route.DepartureDateTime.getMinutes() + ":" + $scope.route.DepartureDateTime.getSeconds();
            $scope.route.ArrivalTime = $scope.route.ArrivalDateTime.getHours() + ":" + $scope.route.ArrivalDateTime.getMinutes() + ":" + $scope.route.ArrivalDateTime.getSeconds();
            if ($scope.route.RouteId) {    
                routeService.update($scope.route).success(function (response) {
                    notify({ message: 'Route information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving route informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
            else {
                $scope.route.RouteId = "73a2436e-ee50-4254-b649-6eaa4e56cd3f";
                routeService.insert($scope.route).success(function (response) {
                    notify({ message: 'Route information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving route informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
        }
        else {
            this.formScope.routeEdit.submitted = true;
        }

    };

    $scope.recursiveTypeChange = function () {
        
        this.formScope.recursiveWeekly = this.formScope.recursiveType == 'Weekly';
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};
