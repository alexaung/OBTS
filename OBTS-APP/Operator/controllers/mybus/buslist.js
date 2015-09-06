/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function busListCtrl($scope, $modal, $http, notify, dataService) {
    
    //var webapiurl = 'http://localhost:57448/api/';
    
    

    $scope.initBusList = function () {
        $http.get(dataService.AppData.webapiurl + 'buses').success(function (data, status, headers, config) {
            $scope.buses = data;
        }).error(function (data, status, headers, config) {
            $scope.buses = null;
        });
   
    }

    $scope.open = function () {
        var modalInstance = $modal.open({
            templateUrl: 'views/mybus/bus_edit.html',
            controller: busEditCtrl,
            windowClass: "animated bounceInUp",
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
        dataService.AppData.bus = bus;
        $scope.open();
    };
    

};
