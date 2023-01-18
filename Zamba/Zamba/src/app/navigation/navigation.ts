import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'applications',
        title    : 'Applications',
        translate: 'NAV.APPLICATIONS',
        type     : 'group',
        icon     : 'apps',
        children : [
            {
                id       : 'todo',
                title    : 'Tasks',
                translate: 'NAV.TODO',
                type     : 'item',
                icon     : 'tasks',
                url      : '/apps/todo/all'
             },
            // {
            //     id       : 'dashboards',
            //     title    : 'Dashboards',
            //     translate: 'NAV.DASHBOARDS',
            //     type     : 'collapsable',
            //     icon     : 'dashboard',
            //     children : [
            //         {
            //             id   : 'analytics',
            //             title: 'Analytics',
            //             translate: 'NAV.ANALYTICS',
            //             type : 'item',
            //             url  : '/apps/dashboards/analytics'
            //         },
            //         {
            //             id   : 'project',
            //             title: 'Project',
            //             translate: 'NAV.PROJECTS',
            //             type : 'item',
            //             url  : '/apps/dashboards/project'
            //         }
            //     ]
            // },
            // {
            //     id       : 'calendar',
            //     title    : 'Calendar',
            //     translate: 'NAV.CALENDAR',
            //     type     : 'item',
            //     icon     : 'today',
            //     url      : '/apps/calendar'
            //  },
            //  {
            //     id       : 'academy',
            //     title    : 'Academy',
            //     translate: 'NAV.ACADEMY',
            //     type     : 'item',
            //     icon     : 'school',
            //     url      : '/apps/academy'
            // },
            //  {
            //      id       : 'mail',
            //      title    : 'Mail',
            //      translate: 'NAV.MAIL.TITLE',
            //      type     : 'item',
            //      icon     : 'email',
            //      url      : '/apps/mail',
            //      badge    : {
            //          title    : '25',
            //          translate: 'NAV.MAIL.BADGE',
            //          bg       : '#F44336',
            //          fg       : '#FFFFFF'
            //      }
            //  },
            //  {
            //     id       : 'flow',
            //     title    : 'Diagramas',
            //     translate: 'NAV.FLOW',
            //     type     : 'item',
            //     icon     : 'flow',
            //     url      : '/apps/flow'
            // },
            // {
            //     id       : 'chat',
            //     title    : 'Chat',
            //     translate: 'NAV.CHAT',
            //     type     : 'item',
            //     icon     : 'chat',
            //     url      : '/apps/chat',
            //     badge    : {
            //         title: '13',
            //         bg   : '#09d261',
            //         fg   : '#FFFFFF'
            //     }
            // }
        ]
    },
    // {
    //     id      : 'pages',
    //     title   : 'Servicios',
    //     type    : 'group',
    //     icon    : 'pages',
    //     children: [
    //         {
    //             id      : 'authentication',
    //             title   : 'Exchange Link',
    //             type    : 'collapsable',
    //             icon    : 'lock',
    //             badge   : {
    //                 title: '10',
    //                 bg   : '#525e8a',
    //                 fg   : '#FFFFFF'
    //             },
    //             children: [
    //                 {
    //                     id   : 'login',
    //                     title: 'Configuracion',
    //                     type : 'item',
    //                     url  : '/pages/auth/login'
    //                 },
    //             ]
    //         },
            
    //         {
    //             id   : 'profile',
    //             title: 'Profile',
    //             type : 'item',
    //             icon : 'person',
    //             url  : '/pages/profile'
    //         },
    //         {
    //             id      : 'search',
    //             title   : 'Search',
    //             type    : 'collapsable',
    //             icon    : 'search',
    //             children: [
    //                 {
    //                     id   : 'search-classic',
    //                     title: 'Classic',
    //                     type : 'item',
    //                     url  : '/pages/search/classic'
    //                 },
    //                 {
    //                     id   : 'search-modern',
    //                     title: 'Modern',
    //                     type : 'item',
    //                     url  : '/pages/search/modern'
    //                 }
    //             ]
    //         },
    //         {
    //             id   : 'faq',
    //             title: 'Faq',
    //             type : 'item',
    //             icon : 'help',
    //             url  : '/pages/faq'
    //         },
    //         {
    //             id   : 'knowledge-base',
    //             title: 'Knowledge Base',
    //             type : 'item',
    //             icon : 'import_contacts',
    //             url  : '/pages/knowledge-base'
    //         }
    //     ]
    // },
    // {
    //     id      : 'documentation',
    //     title   : 'Documentation',
    //     icon    : 'import_contacts',
    //     type    : 'group',
    //     children: [
    //         {
    //             id   : 'changelog',
    //             title: 'Changelog',
    //             type : 'item',
    //             icon : 'update',
    //             url  : '/documentation/changelog',
    //             badge: {
    //                 title: '8.0.0',
    //                 bg   : '#EC0C8E',
    //                 fg   : '#FFFFFF'
    //             }
    //         },
    //         {
    //             id      : 'getting-started',
    //             title   : 'Getting Started',
    //             type    : 'collapsable',
    //             icon    : 'import_contacts',
    //             children: [
    //                 {
    //                     id   : 'introduction',
    //                     title: 'Introduction',
    //                     type : 'item',
    //                     url  : '/documentation/getting-started/introduction'
    //                 },
    //                 {
    //                     id   : 'installation',
    //                     title: 'Installation',
    //                     type : 'item',
    //                     url  : '/documentation/getting-started/installation'
    //                 }
    //             ]
    //         },
    //         {
    //             id      : 'working-with-fuse',
    //             title   : 'Working with Fuse',
    //             type    : 'collapsable',
    //             icon    : 'import_contacts',
    //             children: [
    //                 {
    //                     id   : 'server',
    //                     title: 'Server',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/server'
    //                 },
    //                 {
    //                     id   : 'production',
    //                     title: 'Production',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/production'
    //                 },
    //                 {
    //                     id   : 'directory-structure',
    //                     title: 'Directory Structure',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/directory-structure'
    //                 },
    //                 {
    //                     id   : 'updating-fuse',
    //                     title: 'Updating Fuse',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/updating-fuse'
    //                 },
    //                 {
    //                     id   : 'multi-language',
    //                     title: 'Multi Language',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/multi-language'
    //                 },
    //                 {
    //                     id   : 'material-theming',
    //                     title: 'Material Theming',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/material-theming'
    //                 },
    //                 {
    //                     id   : 'theme-layouts',
    //                     title: 'Theme Layouts',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/theme-layouts'
    //                 },
    //                 {
    //                     id   : 'page-layouts',
    //                     title: 'Page Layouts',
    //                     type : 'item',
    //                     url  : '/documentation/working-with-fuse/page-layouts'
    //                 }
    //             ]
    //         },
    //         {
    //             id      : 'components',
    //             title   : 'Components',
    //             type    : 'collapsable',
    //             icon    : 'import_contacts',
    //             children: [
    //                 {
    //                     id   : 'countdown',
    //                     title: 'Countdown',
    //                     type : 'item',
    //                     url  : '/documentation/components/countdown'
    //                 },
    //                 {
    //                     id   : 'highlight',
    //                     title: 'Highlight',
    //                     type : 'item',
    //                     url  : '/documentation/components/highlight'
    //                 },
    //                 {
    //                     id   : 'material-color-picker',
    //                     title: 'Material Color Picker',
    //                     type : 'item',
    //                     url  : '/documentation/components/material-color-picker'
    //                 },
    //                 {
    //                     id   : 'navigation',
    //                     title: 'Navigation',
    //                     type : 'item',
    //                     url  : '/documentation/components/navigation'
    //                 },
    //                 {
    //                     id   : 'progress-bar',
    //                     title: 'Progress Bar',
    //                     type : 'item',
    //                     url  : '/documentation/components/progress-bar'
    //                 },
    //                 {
    //                     id   : 'search-bar',
    //                     title: 'Search Bar',
    //                     type : 'item',
    //                     url  : '/documentation/components/search-bar'
    //                 },
    //                 {
    //                     id   : 'sidebar',
    //                     title: 'Sidebar',
    //                     type : 'item',
    //                     url  : '/documentation/components/sidebar'
    //                 },
    //                 {
    //                     id   : 'shortcuts',
    //                     title: 'Shortcuts',
    //                     type : 'item',
    //                     url  : '/documentation/components/shortcuts'
    //                 },
    //                 {
    //                     id   : 'widget',
    //                     title: 'Widget',
    //                     type : 'item',
    //                     url  : '/documentation/components/widget'
    //                 }
    //             ]
    //         },
    //         {
    //             id      : '3rd-party-components',
    //             title   : '3rd Party Components',
    //             type    : 'collapsable',
    //             icon    : 'import_contacts',
    //             children: [
    //                 {
    //                     id      : 'datatables',
    //                     title   : 'Datatables',
    //                     type    : 'collapsable',
    //                     children: [
    //                         {
    //                             id   : 'ngxdatatable',
    //                             title: 'ngx-datatable',
    //                             type : 'item',
    //                             url  : '/documentation/components-third-party/datatables/ngx-datatable'
    //                         }
    //                     ]
    //                 },
    //                 {
    //                     id   : 'google-maps',
    //                     title: 'Google Maps',
    //                     type : 'item',
    //                     url  : '/documentation/components-third-party/google-maps'
    //                 }
    //             ]
    //         },
    //         {
    //             id      : 'directives',
    //             title   : 'Directives',
    //             type    : 'collapsable',
    //             icon    : 'import_contacts',
    //             children: [
    //                 {
    //                     id   : 'fuse-if-on-dom',
    //                     title: 'fuseIfOnDom',
    //                     type : 'item',
    //                     url  : '/documentation/directives/fuse-if-on-dom'
    //                 },
    //                 {
    //                     id   : 'fuse-inner-scroll',
    //                     title: 'fuseInnerScroll',
    //                     type : 'item',
    //                     url  : '/documentation/directives/fuse-inner-scroll'
    //                 },
    //                 {
    //                     id   : 'fuse-mat-sidenav',
    //                     title: 'fuseMatSidenav',
    //                     type : 'item',
    //                     url  : '/documentation/directives/fuse-mat-sidenav'
    //                 },
    //                 {
    //                     id   : 'fuse-perfect-scrollbar',
    //                     title: 'fusePerfectScrollbar',
    //                     type : 'item',
    //                     url  : '/documentation/directives/fuse-perfect-scrollbar'
    //                 }
    //             ]
    //         },
    //         {
    //             id      : 'services',
    //             title   : 'Services',
    //             type    : 'collapsable',
    //             icon    : 'import_contacts',
    //             children: [
    //                 {
    //                     id   : 'fuse-config',
    //                     title: 'Fuse Config',
    //                     type : 'item',
    //                     url  : '/documentation/services/fuse-config'
    //                 },
    //                 {
    //                     id   : 'fuse-splash-screen',
    //                     title: 'Fuse Splash Screen',
    //                     type : 'item',
    //                     url  : '/documentation/services/fuse-splash-screen'
    //                 }
    //             ]
    //         }
    //     ]
    // }
];
