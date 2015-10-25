

/**
 * CalendarCtrl - Controller for Calendar
 * Store data events for calendar
 */
function calendarCtrl($scope, $http, routeService) {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    $scope.events = []
  
       
   

    $scope.initCalendar= function () {
        routeService.loadRouteList().success(function (data) {
            $scope.routes = data;
            angular.forEach($scope.routes, function (route) {
                var event = null
                angular.forEach($scope.events, function (e) {
                    if (e.title == route.SourceCity + " - " + route.DestinationCity && e.start == route.RouteDate) {
                        e.routes.push(route);
                        event = e;
                    }

                })
                if (!event) {
                    event = { title: route.SourceCity + " - " + route.DestinationCity, start: route.RouteDate, routes:[] }
                    event.routes.push(route);
                    $scope.events.push(event);
                }
                
        })

        
        });
    }

 


    /* message on eventClick */
    $scope.alertOnEventClick = function (event, allDay, jsEvent, view) {
        $scope.selectedEvent = event;
    };
    /* message on Drop */
    $scope.alertOnDrop = function (event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view) {
        $scope.alertMessage = (event.title + ': Droped to make dayDelta ' + dayDelta);
    };
    /* message on Resize */
    $scope.alertOnResize = function (event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view) {
        $scope.alertMessage = (event.title + ': Resized to make dayDelta ' + minuteDelta);
    };

    /* config object */
    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: true,
            header: {
                left: 'prev,next',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize
        }
    };

    /* Event sources array */
    $scope.eventSources = [$scope.events];
}
