/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function busListCtrl($scope, $modal, $http, DataService) {
    
    var webapiurl = 'http://localhost:57448/api/';
    $scope.busDto = DataService.Data.busDto;
    $scope.bus = {};

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
            for (var d = 0, len = data.length; d < len; d += 1) {
                if (data[d].KeyCode == $scope.busDto.Brand) {
                    $scope.busDto.Brand = data[d];
                }
            }
        }).error(function (data, status, headers, config) {
            $scope.brands = null;
        });

        $http.get(webapiurl + 'codetables/title/bustype').success(function (data, status, headers, config) {
            $scope.bustypes = data;
            for (var d = 0, len = data.length; d < len; d += 1) {
                if (data[d].KeyCode == $scope.busDto.BusType) {
                    $scope.busDto.BusType = data[d];
                }
            }
            $scope.bus = $scope.busDto;
        }).error(function (data, status, headers, config) {
            $scope.bustypes = null;
        });
        
             
        
    }


    $scope.open = function (bus) {
        DataService.Data.busDto = bus;
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
             "BusId": "1694AF70-C424-47F9-9CE5-2BD4955CB1BD",
                "Brand": $scope.bus.Brand.KeyCode,
                "BusType": $scope.bus.BusType.KeyCode,
                "RegistrationNo": $scope.bus.RegNo,
                "PermitNumber": $scope.bus.PermitNo,
                "PermitRenewDate": $scope.bus.PermitRenewDate,
                "InsurancePolicyNumber": $scope.bus.InsurancePolicyNo,
                "InsuranceCompany": $scope.bus.InsuranceCompany,
                "InsuranceValidFrom": $scope.bus.InsuranceValidFrom,
                "InsuranceValidTo": $scope.bus.InsuranceValidTo,
                "VechiclePhoneNo": $scope.bus.VechiclePhoneNo,
                "DriverName": $scope.bus.DriverName,
                "Description": $scope.bus.Description,
                "Status": $scope.optStatus,
                "OperatorId": "F9688FDF-1BEF-4E32-B70C-4494856BB94D"
         }
         
        $http.post(webapiurl + 'buses', bus).
            success(function (data, status, headers, config) {
                alert("saved successfully!!")
            }).
            error(function (data, status, headers, config) {
                alert("error!!")
            });
    };


};
