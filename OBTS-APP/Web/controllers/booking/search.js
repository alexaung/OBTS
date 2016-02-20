

/**
 * CalendarCtrl - Controller for Calendar
 * Store data events for calendar
 */
function searchCtrl($scope, $http, $filter, $stateParams, $timeout, notify, routeService, bookingService) {
    $scope.SeatState = {
        Available: 0,
        UnConfirm: 2,
        Selected: 8
    }
   
    
    $scope.initSearchResult = function () {
        $scope.FromCityId = $stateParams.FromCity;
        $scope.ToCityId = $stateParams.ToCity;
        $scope.DeptDate = $stateParams.DeptDate;
        $scope.ReturnDate = $stateParams.ReturnDate;
        $scope.IsReturn = $stateParams.IsReturn.toLowerCase() == "true";
        bookingService.booking.ReturnBookingDetail = undefined;
        routeService.searchRoutes($scope.IsReturn ? $scope.ToCityId : $scope.FromCityId,
                                  $scope.IsReturn ? $scope.FromCityId : $scope.ToCityId,
                                  $scope.IsReturn ? $scope.ReturnDate : $scope.DeptDate).success(function (data) {
                                      $scope.routes = data
            
                                      if ($scope.routes.length > 0) {
                                          for (var i = 0; i < $scope.routes.length; i++) {
                                              var route = $scope.routes[i];
                                              route.rows = [];
                                              if (route.seats.length > 0) {
                                                  var row = route.seats[0].Row;
                                                  var seats = [];
                                                  for (var j = 0; j < route.seats.length; j++) {
                                                      if (row != route.seats[j].Row) {
                                                          route.rows.push(seats)
                                                          seats = [];
                                                          row = route.seats[j].Row;
                                                      }
                                                      seats.push(route.seats[j]);
                                                  }
                                                  route.rows.push(seats)
                                              }
                                          }
                                      }
            
                                  }).error(function (err) {
                                      $scope.routes = null;
                                  });

        routeService.loadCities().success(function (response) {
            $scope.cities = response;
            $scope.FromCity = $filter('filter')($scope.cities, { CityId: $scope.FromCityId })[0];
            $scope.ToCity = $filter('filter')($scope.cities, { CityId: $scope.ToCityId })[0];
        }).error(function (err) {
            $scope.message = err.error_description;
        });
        
        
    }

    $scope.initSearch = function () {
       
        $scope.annimation = false;
        routeService.loadCities().success(function (response) {
            $scope.cities = response;

        }).error(function (err) {
            $scope.message = err.error_description;
        });
    }

    
    $scope.reTweet = function () {
        $scope.annimation = !$scope.annimation;
        var sourcecity = $scope.routesearch.Source_CityId;
        $scope.routesearch.Source_CityId = $scope.routesearch.Destination_CityId;
        $scope.routesearch.Destination_CityId = sourcecity;
        
        $('#btnTweet').removeClass().addClass('fa fa-retweet rotateIn animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass();
            $(this).addClass('fa fa-retweet');
        });

        $('#fromCityBox').removeClass().addClass('col-md-3 fadeInRight animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass();
            $(this).addClass('col-md-3');
        });

        $('#toCityBox').removeClass().addClass('col-md-3 fadeInLeft animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass();
            $(this).addClass('col-md-3');
        });
   
    }

    $scope.search = function () {
        //this.formScope.searchForm.submitted = true;
        if ($scope.searchForm.$valid) {
            //  window.location.href = '/booking/searchresult';
            window.location.href = '#/booking/searchresult/' + $scope.routesearch.Source_CityId + '/' + $scope.routesearch.Destination_CityId + '/' + $filter('date')($scope.routesearch.DeptDate, 'dd-MMM-yyyy') + '/' + $filter('date')($scope.routesearch.ReturnDate, 'dd-MMM-yyyy') + '/false';
        }
        else {
            $scope.searchForm.submitted=true;
        }
    }

    // seat onClick
    $scope.seatClicked = function (seat) {
        seat.State = seat.State != $scope.SeatState.Selected ? $scope.SeatState.Selected : $scope.SeatState.Available;
        
        $scope.selectedroute.totalSelectedSeats = 0;
        for (var j = 0; j < $scope.selectedroute.rows.length; j++) {
            for (var k = 0; k < $scope.selectedroute.rows[j].length; k++) {
                $scope.selectedroute.totalSelectedSeats += $scope.selectedroute.rows[j][k].State == $scope.SeatState.Selected ? 1 : 0;
            }
        }
        $timeout(function () {
            $('.table').trigger('footable_redraw');
        }, 100);
        
    }

    $scope.viewSeats = function (route) {
        
        $scope.selectedroute = $scope.selectedroute!==undefined ? undefined : route;
    }

    $scope.continue = function () {
        if ($scope.selectedroute.totalSelectedSeats == undefined || $scope.selectedroute.totalSelectedSeats <= 0) {
            notify({ message: 'Please select a seat to continue.', classes: 'alert-warning', templateUrl: 'views/common/notify.html' });
            return;
        }
        if ($scope.IsReturn)
        {
            bookingService.booking.ReturnBookingDetail= $scope.bookingDetails();
        }
        else
        {
            bookingService.booking.DepartBookingDetail = $scope.bookingDetails();;
        }
        if ($scope.ReturnDate !== "undefined" && !$scope.IsReturn)
            window.location.href = '#/booking/searchresult/' + $scope.FromCityId + '/' + $scope.ToCityId + '/' + $filter('date')($scope.DeptDate, 'dd-MMM-yyyy') + '/' + $filter('date')($scope.ReturnDate, 'dd-MMM-yyyy') + '/true';
        else
            window.location.href = '#/booking/bookingdetails'
    }
    $scope.bookingDetails = function () {

        var bk = {}
        bk.RouteId = $scope.selectedroute.RouteId;
        bk.DepartureCityDesc = $scope.selectedroute.SourceCity;
        bk.ArrivalCityDesc = $scope.selectedroute.DestinationCity;
        bk.RouteDate = $scope.selectedroute.RouteDate;
        bk.DepartureTime = $scope.selectedroute.DepartureTime;
        bk.RegistrationNo = $scope.selectedroute.RegistrationNo;
        bk.totalSelectedSeats = $scope.selectedroute.totalSelectedSeats;
        bk.RouteFare = $scope.selectedroute.RouteFare;
        bk.bookingPassengers = $filter('filter')($scope.selectedroute.seats, { State: $scope.SeatState.Selected });
        return bk;
       
    }
    

    $scope.prevday = function () {
        var returnDate = new Date(Date.parse($scope.ReturnDate));
        var deptDate = new Date(Date.parse($scope.DeptDate));

        if ($scope.IsReturn) {
            returnDate.setDate(returnDate.getDate() - 1);

        }
        else {
            deptDate.setDate(deptDate.getDate() - 1);
        }
        if (returnDate <= deptDate)
            notify({ message: 'Return date cannot be less than departure date.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
        else {
            $scope.ReturnDate = returnDate;
            $scope.DeptDate = deptDate;
            window.location.href = '#/booking/searchresult/' + $scope.FromCityId + '/' + $scope.ToCityId + '/' + $filter('date')($scope.DeptDate, 'dd-MMM-yyyy') + '/' + $filter('date')($scope.ReturnDate, 'dd-MMM-yyyy') + '/' + $scope.IsReturn;
        }
    }
    $scope.nextday = function () {
        var returnDate = new Date(Date.parse($scope.ReturnDate));
        var deptDate = new Date(Date.parse($scope.DeptDate));

        if ($scope.IsReturn) {
            returnDate.setDate(returnDate.getDate() + 1);
            
        }
        else {
            deptDate.setDate(deptDate.getDate() + 1);
        }
        if (returnDate <= deptDate)
            notify({ message: 'Return date cannot be less than departure date.', classes: 'alert-danger', templateUrl: 'views/common/notify.html' });
        else
        {
            $scope.ReturnDate = returnDate;
            $scope.DeptDate = deptDate;
            window.location.href = '#/booking/searchresult/' + $scope.FromCityId + '/' + $scope.ToCityId + '/' + $filter('date')($scope.DeptDate, 'dd-MMM-yyyy') + '/' + $filter('date')($scope.ReturnDate, 'dd-MMM-yyyy') + '/' + $scope.IsReturn;
        }
            
    }
}
