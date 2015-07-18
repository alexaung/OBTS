/**
 * INSPINIA - Responsive Admin Theme V.10
 *
 */
function config($translateProvider) {

    $translateProvider
    	.translations('mm', {

		            // Define all menu elements
		            DASHBOARD: 'ဒတ္ရွ္ဘုတ္',
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
		            MENUTEST: 'ေခး',

		            // Define some custom text
		            WELCOME: 'မဂၤလာပါ Amelia',
		            MESSAGEINFO: 'You have 42 messages and 6 notifications.',
		            SEARCH: 'Search for something...',

        })
        .translations('en', {

            // Define all menu elements
            DASHBOARD: 'Dashboard',
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
            MENUTEST: 'ေခး',

            // Define some custom text
            WELCOME: 'Welcome Amelia',
            MESSAGEINFO: 'You have 42 messages and 6 notifications.',
            SEARCH: 'Search for something...',

        })
        .translations('es', {

            // Define all menu elements
            DASHBOARD: 'Salpicadero',
            GRAPHS: 'Gráficos',
            MAILBOX: 'El correo',
            WIDGETS: 'Widgets',
            METRICS: 'Métrica',
            FORMS: 'Formas',
            APPVIEWS: 'Vistas app',
            OTHERPAGES: 'Otras páginas',
            UIELEMENTS: 'UI elements',
            MISCELLANEOUS: 'Misceláneo',
            GRIDOPTIONS: 'Cuadrícula',
            TABLES: 'Tablas',
            COMMERCE: 'E-comercio',
            GALLERY: 'Galería',
            MENULEVELS: 'Niveles de menú',
            ANIMATIONS: 'Animaciones',
            LANDING: 'Página de destino',
            LAYOUTS: 'Esquemas',
            MENUTEST: 'ေခး',
            // Define some custom text
            WELCOME: 'Bienvenido Amelia',
            MESSAGEINFO: 'Usted tiene 42 mensajes y 6 notificaciones.',
            SEARCH: 'Busca algo ...',
        });

    $translateProvider.preferredLanguage('en');

}

angular
    .module('inspinia')
    .config(config)
