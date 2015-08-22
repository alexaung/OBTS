/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function busListCtrl($scope, $modal, $http) {
    
    var webapiurl = 'http://localhost:57448/api/';

    $scope.initBusList = function () {
        $http.get(webapiurl + 'buses').success(function (data, status, headers, config) {
            $scope.buses = data;
        }).error(function (data, status, headers, config) {
            $scope.buses = null;
        });
    }

    $scope.initBusEdit = function () {
        $http.get(webapiurl + 'codetables/title/brand').success(function (data, status, headers, config) {
            $scope.brands = data;
        }).error(function (data, status, headers, config) {
            $scope.brands = null;
        });
    }


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
                        },
                        {
                            files: ['css/plugins/iCheck/custom.css', 'js/plugins/iCheck/icheck.min.js']
                        }
                    ]);
                }
            }
        });
    };
    $scope.save = function () {
        var bus = {
                "BusId": "",
                "Company": "Mya Mar Lar",
                "Brand": $scope.txtBrand,
                "BrandDesc": $scope.txtModel,
                "BusType": 1,
                "BusTypeDesc": "Single",
                "RegistrationNo": $scope.txtRegNo,
                "PermitNumber": $scope.txtPermitNo,
                "PermitRenewDate": "2020-01-01T00:00:00",
                "InsurancePolicyNumber": "2222",
                "InsuranceCompany": "Pru",
                "InsuranceValidFrom": "2015-02-02T00:00:00",
                "InsuranceValidTo": "2020-02-02T00:00:00",
                "VechiclePhoneNo": "1231312",
                "DriverName": "Mg Mya",
                "Description": "Mya Mar Lar Bus 2",
                "Status": $scope.optStatus,
                "OperatorId": "F9688FDF-1BEF-4E32-B70C-4494856BB94D",
                "OperatorCompany": "Toe Aung Tours"
            }
        $http.post(webapiurl + 'bus', bus).
            success(function (data, status, headers, config) {
                alert("saved successfully!!")
            }).
            error(function (data, status, headers, config) {
                alert("error!!")
            });
    };


};
