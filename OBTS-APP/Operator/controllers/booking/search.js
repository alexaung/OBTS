

/**
 * CalendarCtrl - Controller for Calendar
 * Store data events for calendar
 */
function searchCtrl($scope, $http, $filter, $stateParams, $timeout, routeService) {

    
    $scope.initSearchResult = function () {
        $scope.FromCityId = $stateParams.FromCity;
        $scope.ToCityId = $stateParams.ToCity;
        $scope.DeptDate = $stateParams.DeptDate;
        $scope.ReturnDate = $stateParams.ReturnDate;
        routeService.searchRoutes($scope.FromCityId, $scope.ToCityId, $scope.DeptDate).success(function (data) {
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
            window.location.href = '#/booking/searchresult/' + $scope.routesearch.Source_CityId + '/' + $scope.routesearch.Destination_CityId + '/' + $filter('date')($scope.routesearch.DeptDate, 'dd-MMM-yyyy') + '/' + $filter('date')($scope.routesearch.ReturnDate, 'dd-MMM-yyyy');
        }
        else {
            $scope.searchForm.submitted=true;
        }
    }

    // seat onClick
    $scope.seatClicked = function (seat) {
        seat.status = !seat.status;
        
        $timeout(function () {
            $('.table').trigger('footable_redraw');
        }, 100);
        
    }

    $scope.prevday = function () {
        var d = new Date(Date.parse($scope.DeptDate));
        d.setDate(d.getDate() - 1);
        $scope.DeptDate = d;
    }
    $scope.nextday = function () {
        var d = new Date(Date.parse($scope.DeptDate));
        d.setDate(d.getDate() + 1);
        $scope.DeptDate = d;
    }
}
