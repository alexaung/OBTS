/**
 * routeListCtrl - Controller used to run route list
 * used in Basic form view
 */
function routeListCtrl($scope, $modal, $http, notify, routeService) {



    $scope.initRouteList = function () {

        routeService.loadRouteList().success(function (data) {
            $scope.routes = data
        }).error(function (err) {
            $scope.routes = null;
        });

    }

    $scope.open = function () {
        var modalInstance = $modal.open({
            templateUrl: 'views/routes/route_edit.html',
            controller: routeEditCtrl,
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
                        },
                        {
                            files: ['css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css']
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
        routeService.route = route;
        $scope.open();
    };


};
