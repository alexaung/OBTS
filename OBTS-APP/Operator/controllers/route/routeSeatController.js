/**
 * routeSeatController - Controller used to run modal view
 * used in Basic form view
 */
function routeSeatController($scope, $modalInstance, $http, notify, routeService, busService, SweetAlert) {
    // Init layout
    $scope.seat = {};

    $scope.rows = [];
    $scope.cols = [];
 
    // Set reserved and selected
    //var reserved = ['A2', 'A3', 'C5', 'C6', 'C7', 'C8', 'J1', 'J2', 'J3', 'J4'];
    var alphabet = "ABCDEFGHIJKMNOPQRSTUVWXYZ".split("");
    var reserved = [];
    var selected = [];

    $scope.initRouteSeat = function (scope) {
        this.formScope = scope;
        routeService.loadRouteSeats(routeService.route.RouteId).success(function (data) {
            $scope.rows = [];
            if (data.length > 0)
            {
                var row = data[0].Row;
                var seats = [];
                for (var i = 0; i < data.length; i++) {
                    if (row != data[i].Row) {
                        $scope.rows.push(seats)
                        seats = [];
                        row = data[i].Row;
                    }
                    seats.push(data[i]);

                }
                $scope.rows.push(seats)
            }
                  
               
        }).error(function (err) {
            $scope.message = err.error_description;
        });

    }
   

   
    // seat onClick
    $scope.seatClicked = function(seat) {
        seat.Bookable = !seat.Bookable;
    }
 
    // get seat status
    $scope.getStatus = function(seatPos) {
        if(reserved.indexOf(seatPos) > -1) {
            return 'reserved';
        } else if(selected.indexOf(seatPos) > -1) {
            return 'selected';
        }
    }
 
    // clear selected
    $scope.clearSelected = function() {
        selected = [];
    }
 
    

    $scope.save = function () {

        var seats = [];
        for (var i = 0; i < $scope.rows.length; i++) {
            for (var j = 0; j < $scope.rows[i].length; j++) {

                seats.push($scope.rows[i][j]);
            }
        }

        if (seats.length>1) {
         
                routeService.bulkInsertSeats(seats).success(function (response) {
                    notify({ message: 'Route seat information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving route seat informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
        } 
    $scope.generate = function () {
        $scope.rows = [];
        busService.loadBusSeats(routeService.route.BusId).success(function (data) {
            if (data.length > 0) {
                var row = data[0].Row;
                var seats = [];
                for (var i = 0; i < data.length; i++) {
                    if (row != data[i].Row) {
                        $scope.rows.push(seats)
                        seats = [];
                        row = data[i].Row;
                    }
                    data[i].RouteId = routeService.route.RouteId;
                    seats.push(data[i]);

                }
                $scope.rows.push(seats)
            }


        }).error(function (err) {
            $scope.message = err.error_description;
        });
        //var k = 0;
        //for (var i = 0; i < $scope.seat.noOfRow; i++) {
        //    k = 1;
        //    var seats = [];
        //    for (var j = 0; j < $scope.seat.noOfColumn; j++) {
        //        seats.push({
        //            BusId: busService.bus.BusId,
        //            SeatNo: alphabet[i] + k,
        //            Bookable: true,
        //            Space: false,
        //            SpecialSeat: false,
        //            Status: 'status',
        //            UpperLower: 1,
        //            Row: i,
        //            Col: k++,
        //        });
        //    }
        //    $scope.rows.push(seats)
        //}
    }

    $scope.copyClicked = function () {
        //console.log("Selected Seat: " + seatPos);
        //$scope.rows = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
        //var alpha = new char[26];
        if ($scope.rows.length > 0) {
            SweetAlert.swal({
                title: "Are you sure?",
                text: "Existing seating plan will be overwritten if you wish to copy seating plan from the bus!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: true,
                closeOnCancel: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    $scope.generate();
                }
                else {
                    ///SweetAlert.swal("Cancelled", "Your imaginary file is safe :)", "error");
                }
            });
        } else {
            $scope.generate();
        }
    }

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

}