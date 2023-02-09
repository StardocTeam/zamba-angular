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
                id       : 'dashboards',
                title    : 'Dashboards',
                translate: 'NAV.DASHBOARDS',
                type     : 'item',
                icon     : 'dashboard',
                url      : '/apps/dashboards/analytics'
            },
            {
                id       : 'ClietImap',
                title    : 'Cliente IMAP',
                translate: 'NAV.CLIENTIMAP',
                type     : 'item',
                icon     : 'mail',
                url      : '/apps/IMAP/client'
            }
        ]
    }
];
