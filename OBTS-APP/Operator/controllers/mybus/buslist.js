/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function busListCtrl($scope, $modal, $http, notify, busService) {
    
    

    $scope.initBusList = function () {

        busService.loadBusList().success(function (data) {
            $scope.buses = data
        }).error(function (err) {
            $scope.buses = null;
        });

    }

    $scope.open = function () {
        var modalInstance = $modal.open({
            templateUrl: 'views/mybus/bus_edit.html',
            controller: busEditCtrl,
            windowClass: "animated bounceInUp",
            size: "lg",
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'datePicker',
                            files: ['css/plugins/datapicker/angular-datapicker.css', 'js/plugins/datapicker/angular-datepicker.js']
                        },
                        {
                            files: ['css/plugins/iCheck/custom.css', 'js/plugins/iCheck/icheck.min.js']
                        },
                        {
                            name: 'cgNotify',
                            files: ['css/plugins/angular-notify/angular-notify.min.css', 'js/plugins/angular-notify/angular-notify.min.js']
                        }
                    ]);
                }
            }
        });
        
        modalInstance.result.then(function (selectedItem) {
             $scope.initBusList();
        }, function () {
           

        });
    };
    $scope.openEdit = function (bus) {
        busService.bus = bus;
        $scope.open();
    };
    

};
