/**
 * INSPINIA - Responsive Admin Theme
 *
 */
function config($translateProvider) {

    $translateProvider
    	.translations('mm', {

		            // Define all menu elements
    	    DASHBOARD: 'ဒတ္ရွ္ဘုတ္',
    	    MYBUSES: 'ႈါ႔ကား...',
    	    ROUTES: 'ဃရီးစဥ္',
    	    BOOKING: 'ဘြတ္ကင္း',
            REPORTS: 'ရီပို႔',
		            GRAPHS: 'ဂရပ္ဖ္',
		            MAILBOX: 'အီးေမးလ္',
		            WIDGETS: 'Widgets',
		            METRICS: 'Metrics',
		            FORMS: 'ေဖာင္',
		            APPVIEWS: 'App views',
		            OTHERPAGES: 'Other pages',
		            UIELEMENTS: 'UI elements',
		            MISCELLANEOUS: 'Miscellaneous',
		            GRIDOPTIONS: 'Grid options',
		            TABLES: 'Tables',
		            COMMERCE: 'E-commerce',
		            GALLERY: 'Gallery',
		            MENULEVELS: 'Menu levels',
		            ANIMATIONS: 'Animations',
		            LANDING: 'Landing page',
		            LAYOUTS: 'Layouts',

		            // Define some custom text
		            WELCOME: 'Welcome Amelia',
		            MESSAGEINFO: 'You have 42 messages and 6 notifications.',
		            SEARCH: 'Search for something...',

        })
        .translations('en', {

            // Define all menu elements
            DASHBOARD: 'Dashboard',
            MYBUSES: 'My Bus',
            ROUTES: 'Routes',
            BOOKING: 'Booking',
            REPORTS: 'Reports',
            GRAPHS: 'Graphs',
            MAILBOX: 'Mailbox',
            WIDGETS: 'Widgets',
            METRICS: 'Metrics',
            FORMS: 'Forms',
            APPVIEWS: 'App views',
            OTHERPAGES: 'Other pages',
            UIELEMENTS: 'UI elements',
            MISCELLANEOUS: 'Miscellaneous',
            GRIDOPTIONS: 'Grid options',
            TABLES: 'Tables',
            COMMERCE: 'E-commerce',
            GALLERY: 'Gallery',
            MENULEVELS: 'Menu levels',
            ANIMATIONS: 'Animations',
            LANDING: 'Landing page',
            LAYOUTS: 'Layouts',

            // Define some custom text
            WELCOME: 'Welcome Amelia',
            MESSAGEINFO: 'You have 42 messages and 6 notifications.',
            SEARCH: 'Search for something...',

        });

    $translateProvider.preferredLanguage('en');

}

angular
    .module('inspinia')
    .config(config)
