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
		    WELCOME: 'Welcome Amelia',
		    MESSAGEINFO: 'You have 42 messages and 6 notifications.',
		    SEARCH: 'Search',
		    CALENDAR: 'Calendar',
		    BOOKINGLIST: 'Booking List',
		    NUMBEROFSEAT: 'No. of seats',

    	    // Define bus information
		    BUSBRAND: "အမွတ္တ႔ဆိပ္",
		    BUSMODEL: "အမ်ဳိးအစား",
		    BUSREGNO: "ကားနံပါတ္",
		    BUSPERMITNO: "ပါမစ္",
		    BUSPERMITRENEWDATE: "Permit Renew Date",
		    BUSTYPE: "Bus Type",
		    INSURANCEPOLICYNUMBER: "အာမခံ ၚလဏီနံပါတ္ပေ",
		    INSURANCECOMPANY: "Insurance Company",
		    INSURANCEVALIDFROM: "Insurance Valid From",
		    INSURANCEVALIDTO: "Insurance Valid To",
		    VECHICLEPHONENO: "Vehicle Phone",
		    DRIVERNAME: "Driver Name",
		    DESCRIPTION: "Description",
		    FACILTIES: "Facilties",
		    STATUS: "အေျနေန",

    	    // Define route information
		    FROM: "From",
		    TO: "To",
		    ROUTEDATE: "Deperture Date",
		    DEPARTURETIME: "Deperture Time",
		    ARRIVALTIME: "Arrival Time",
		    ROUTEFARE: "Fare",
		    RECURSIVE: "ပံုမွန္ခရီးစဥ္",

    	    //Search Form
		    DEPARTDATE: "ထြက္ခြာမည္႔ေန႔",
		    RETURNDATE: "Return",
		    NOOFPAX: "No. of pax",

            OPERATOR:"အောပရေတာ"

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
            WELCOME: 'Welcome Amelia',
            MESSAGEINFO: 'You have 42 messages and 6 notifications.',
            SEARCH: 'Search',
            CALENDAR: 'Calendar',
            BOOKINGLIST: 'Booking List',
            NUMBEROFSEAT: 'No. of seats',


            
            // Define bus information
            BUSBRAND: "Brand",
            BUSMODEL: "Model",
            BUSREGNO: "Reg No.",
            BUSPERMITNO: "Permit No",
            BUSPERMITRENEWDATE: "Permit Renew Date",
            BUSTYPE: "Bus Type",
            INSURANCEPOLICYNUMBER: "Insurance Policy No",
            INSURANCECOMPANY: "Insurance Company",
            INSURANCEVALIDFROM: "Insurance Valid From",
            INSURANCEVALIDTO: "Insurance Valid To",
            VECHICLEPHONENO: "Vehicle Phone",
            DRIVERNAME: "Driver Name",
            DESCRIPTION: "Description",
            FACILTIES: "Facilties",
            STATUS: "Status",

            // Define route information
            FROM: "From",
            TO: "To",
            ROUTEDATE: "Deperture Date",
            DEPARTURETIME: "Deperture Time",
            ARRIVALTIME: "Arrival Time",
            ROUTEFARE: "Fare",
            RECURSIVE: "Recursive",

            //Search Form
            DEPARTDATE: "Depart",
            RETURNDATE: "Return",
            NOOFPAX: "No. of pax",
            OPERATOR:"Operator"
        });

    $translateProvider.preferredLanguage('en');

}

angular
    .module('inspinia')
    .config(config)
