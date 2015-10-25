/**
 * busListCtrl - Controller used to run modal view
 * used in Basic form view
 */
function busSeatController($scope, $modalInstance, $http, notify, busService) {
    // Init layout
    $scope.seat = {};

    $scope.rows = [];
    $scope.cols = [];
 
    // Set reserved and selected
    //var reserved = ['A2', 'A3', 'C5', 'C6', 'C7', 'C8', 'J1', 'J2', 'J3', 'J4'];
    var alphabet = "ABCDEFGHIJKMNOPQRSTUVWXYZ".split("");
    var reserved = [];
    var selected = [];
 
    $scope.generateClicked = function () {
        //console.log("Selected Seat: " + seatPos);
        //$scope.rows = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
        //var alpha = new char[26];
        var k = 0;
        for (var i = 0; i < $scope.seat.noOfRow; i++) {
            $scope.rows[i] = alphabet[i];//(char)(65 + (k++))
        }
        k = 1;
        for (var i = 0; i < $scope.seat.noOfColumn; i++) {
            $scope.cols[i] = k++;//(char)(65 + (k++))
        }
        //$scope.cols = [1, 2, 3, 4, 5, 6, 7, 8];
        //reserved = ['A2', 'A3', 'C5', 'C6', 'C7', 'C8', 'J1', 'J2', 'J3', 'J4'];
    }
    // seat onClick
    $scope.seatClicked = function(seatPos) {
        console.log("Selected Seat: " + seatPos);
        var index = selected.indexOf(seatPos);
        if(index != -1) {
            // seat already selected, remove
            selected.splice(index, 1)
        } else {
            // new seat, push
            selected.push(seatPos);
        }
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

}