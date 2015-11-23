

/**
 * CalendarCtrl - Controller for Calendar
 * Store data events for calendar
 */
function searchCtrl($scope, $http, routeService) {

    
    $scope.initSearch = function () {
        $scope.annimation = false;
        routeService.loadCities().success(function (response) {
            $scope.cities = response;
            $(".chosen-select").chosen()
        }).error(function (err) {
            $scope.message = err.error_description;
        });
        $(".chosen-select").chosen()
    }

    /**
     * states - Data used in Advanced Form view for Chosen plugin
     */
    $scope.states = [
        { id: 1, name: 'Alabama' },
        { id: 2, name: 'Alaska' }
    ];

    $scope.persons = [
        {
            id: '1',
            firstName: 'Monica',
            lastName: 'Smith'
        },
        {
            id: '2',
            firstName: 'Sandra',
            lastName: 'Jackson'
        },
        {
            id: '3',
            firstName: 'John',
            lastName: 'Underwood'
        },
        {
            id: '4',
            firstName: 'Chris',
            lastName: 'Johnatan'
        },
        {
            id: '5',
            firstName: 'Kim',
            lastName: 'Rosowski'
        }
    ];
    $scope.reTweet = function () {
        $scope.annimation = !$scope.annimation;
        var sourcecity = $scope.search.Source_CityId;
        $scope.search.Source_CityId = $scope.search.Destination_CityId;
        $scope.search.Destination_CityId = sourcecity;
        /*
        $('#fromCityBox').removeAttr('class').attr('class', '');
        $('#toCityBox').removeAttr('class').attr('class', '');
        $('#fromCityBox').addClass('col-md-3');
        $('#fromCityBox').addClass('animated');
        
        $('#fromCityBox').addClass($scope.annimation ? 'fadeInLeft' : 'fadeInRight');
        $('#toCityBox').addClass('col-md-3');
        $('#toCityBox').addClass('animated');
        $('#toCityBox').addClass($scope.annimation ? 'fadeInRight' : 'fadeInLeft');
        */
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
   
}
