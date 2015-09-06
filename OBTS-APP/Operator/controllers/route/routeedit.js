/**
 * busEditCtrl - Controller used to run modal view
 * used in Basic form view
 */
'use strict';
function busEditCtrl($scope, $modalInstance, $http, notify, DataService) {
    
    var webapiurl = 'http://localhost:57448/api/';
    notify.config({
        duration: '1500'
    });

    $scope.statusTypes = [{ KeyCode: true, Value: "Active" }, { KeyCode: true, Value: "Inactive" }]

    $scope.initBusEdit = function (scope) {
        this.formScope = scope;
        $scope.bus = DataService.AppData.bus;
        DataService.AppData.bus = {};
        $scope.bus.OperatorId = "F9688FDF-1BEF-4E32-B70C-4494856BB94D"
        $http.get(webapiurl + 'codetables/title/brand').success(function (data, status, headers, config) {
            $scope.brands = data;
            //for (var d = 0, len = data.length; d < len; d += 1) {
            //    if (data[d].KeyCode == $scope.busDto.Brand) {
            //        $scope.busDto.Brand = data[d];
            //    }
            //}
        }).error(function (data, status, headers, config) {
            $scope.brands = null;
        });

        $http.get(webapiurl + 'codetables/title/bustype').success(function (data, status, headers, config) {
            $scope.bustypes = data;
            //for (var d = 0, len = data.length; d < len; d += 1) {
            //    if (data[d].KeyCode == $scope.busDto.BusType) {
            //        $scope.busDto.BusType = data[d];
            //    }
            //}
            //$scope.bus = $scope.busDto;
        }).error(function (data, status, headers, config) {
            $scope.bustypes = null;
        });
    }

    $scope.save = function () {
        
        if (this.formScope.busEdit.$valid) {
            if ($scope.bus.BusId) {
                $http.put(webapiurl + 'buses/' + $scope.bus.BusId, $scope.bus).
                success(function (data, status, headers, config) {
                    notify({ message: 'Bus information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (data, status, headers, config) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving bus informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
            else {
                $http.post(webapiurl + 'buses', $scope.bus).
                success(function (data, status, headers, config) {
                    notify({ message: 'Bus information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (data, status, headers, config) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving bus information. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
        }
        else {
            this.formScope.busEdit.submitted = true;
        }
        
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};
