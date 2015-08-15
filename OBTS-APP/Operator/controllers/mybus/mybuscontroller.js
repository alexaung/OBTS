/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function busListCtrl($scope, $modal, $http) {
    $http.get("http://localhost:50758/api/buses").success(function (data, status, headers, config) {
        $scope.buses = data;
    }).error(function (data, status, headers, config) {
        alert("error");
    });
    $scope.open = function () {

        var modalInstance = $modal.open({
            templateUrl: 'views/mybus/bus_edit.html',
            controller: ModalInstanceCtrl,
            windowClass: "animated bounceInUp",
            resolve: {
                loadPlugin: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        {
                            name: 'datePicker',
                            files: ['css/plugins/datapicker/angular-datapicker.css', 'js/plugins/datapicker/angular-datepicker.js']
                        }
                    ]);
                }
            }
        });
    };

    $scope.open4 = function () {
        var modalInstance = $modal.open({
            templateUrl: 'views/modal_example2.html',
            controller: ModalInstanceCtrl,
            windowClass: "animated flipInY"
        });
    };


};
