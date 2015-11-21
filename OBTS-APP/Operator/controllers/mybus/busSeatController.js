/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function busSeatController($scope, $modalInstance, $http, notify, busService, SweetAlert) {
    // Init layout
    $scope.seat = {};

    $scope.rows = [];
    $scope.cols = [];
 
    // Set reserved and selected
    //var reserved = ['A2', 'A3', 'C5', 'C6', 'C7', 'C8', 'J1', 'J2', 'J3', 'J4'];
    var alphabet = "ABCDEFGHIJKMNOPQRSTUVWXYZ".split("");
    var reserved = [];
    var selected = [];

    $scope.initBusSeat = function (scope) {
        this.formScope = scope;
        busService.loadBusSeats(busService.bus.BusId).success(function (data) {
            $scope.rows = [];
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
               
        }).error(function (err) {
            $scope.message = err.error_description;
        });

    }

    $scope.generateClicked = function () {
        //console.log("Selected Seat: " + seatPos);
        //$scope.rows = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
        //var alpha = new char[26];
        if ($scope.rows.length > 0)
        {
            SweetAlert.swal({
                title: "Are you sure?",
                text: "Existing seating plan will be deleted if you wish to regenerate!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, regenerate it!",
                cancelButtonText: "No, cancel please!",
                closeOnConfirm: true,
                closeOnCancel: true
            },
            function (isConfirm) {
                if (isConfirm) {
                    $scope.rows = [];
                    var k = 0;
                    for (var i = 0; i < $scope.seat.noOfRow; i++) {
                        k = 1;
                        var seats = [];
                        for (var j = 0; j < $scope.seat.noOfColumn; j++) {
                            seats.push({
                                BusId: busService.bus.BusId,
                                SeatNo: alphabet[i] + k,
                                Bookable: true,
                                Space: false,
                                SpecialSeat: false,
                                Status: 'status',
                                UpperLower: 1,
                                Row: i,
                                Col: k++,
                            });
                        }
                        $scope.rows.push(seats)
                    }
                } else {
                    ///SweetAlert.swal("Cancelled", "Your imaginary file is safe :)", "error");
                }
            });
        }
        

        
        
        //$scope.cols = [1, 2, 3, 4, 5, 6, 7, 8];
        //reserved = ['A2', 'A3', 'C5', 'C6', 'C7', 'C8', 'J1', 'J2', 'J3', 'J4'];
       

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
 
    // show selected
    $scope.showSelected = function() {
        if(selected.length > 0) {
            alert("Selected Seats: \n" + selected);
        } else {
            alert("No seats selected!");
        }
    }

    $scope.save = function () {

        var seats = [];
        for (var i = 0; i < $scope.rows.length; i++) {
            for (var j = 0; j < $scope.rows[i].length; j++) {

                seats.push($scope.rows[i][j]);
            }
        }

        if (seats.length>1) {
         
                busService.bulkInsertSeats(seats).success(function (response) {
                    notify({ message: 'Bus seat information is saved successfuly.', classes: 'alert-success', templateUrl: 'views/common/notify.html' });
                    $modalInstance.close();
                }).
                error(function (error) {
                    notify.config({
                        duration: '7000'
                    });
                    notify({ message: 'There is having some error in saving bus seat informatin. Please contact system admin if the problem persists.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
                });
            }
        } 

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

}