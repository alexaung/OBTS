/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function routeListCtrl($scope, $modal, $http, notify, DataService, footable) {
    
    
    
    

    $scope.initBusList = function () {
        $http.get(DataService.AppData.webapiurl + 'routes').success(function (data, status, headers, config) {
            $scope.routes = data;
        }).error(function (data, status, headers, config) {
            $scope.routes = null;
        });
   
    }

    $scope.open = function () {
        var modalInstance = $modal.open({
            templateUrl: 'views/routes/route_edit.html',
            controller: routeEditCtrl,
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
             $scope.initRouteList();
        }, function () {
           

        });
    };
    $scope.openEdit = function (route) {
        DataService.AppData.bus = route;
        $scope.open();
    };
    

};
