/**
 * busEditCtrl - Controller used to run modal view
 * used in Basic form view
 */
'use strict';
function busEditCtrl($scope, $modalInstance, $http, notify, dataService,busService) {
    
    
    notify.config({
        duration: '1500'
    });

    $scope.statusTypes = [{ KeyCode: true, Value: "Active" }, { KeyCode: true, Value: "Inactive" }]

    $scope.initBusEdit = function (scope) {
        this.formScope = scope;
        $scope.bus = dataService.AppData.bus;
        dataService.AppData.bus = {};
        $scope.bus.OperatorId = "F9688FDF-1BEF-4E32-B70C-4494856BB94D";

        
        busService.loadBrand().success(function (data) {
            $scope.brands = data
        }).error(function (err) {
             $scope.message = err.error_description;
         });
    
        busService.loadBusType().success(function (data) {
            $scope.bustypes = data
        }).error(function (err) {
             $scope.message = err.error_description;
         });
    
    }

    $scope.save = function () {
        
        if (this.formScope.busEdit.$valid) {
            if ($scope.bus.BusId) {
                
                busService.update($scope.bus).success(function (response) {
                    notify({ message: 'Bus information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving bus informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
            else {
                busService.insert($scope.bus).success(function (response) {
                    notify({ message: 'Bus information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving bus informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
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
