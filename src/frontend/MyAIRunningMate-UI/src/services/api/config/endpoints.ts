export const API_ENDPOINTS = {

    activityView: {
        activityAggregate: '/activity/aggregate',
    },

    bestEfforts: {
        allEfforts: '/best_efforts/efforts',
        updateEffort: '/best_efforts/update',
    },

    calendar: {
        display: '/calendar/display',
    },

    events: {
        upcomingEvents: '/events/upcoming',
        primaryEvent: '/events/single',
    },
    
    ingestion: {
        upload: '/fitfile/upload',
    },

    session: {
        login: '/session/login',
        logout: '/session/logout',
    },

    strava: {
        connect: '/strava/connect',
        callback: '/strava/callback',
        status: '/strava/status',
    },

    weight: {
        latest: '/weight/latest',
        history: '/weight/history',
        logWeight: '/weight/log_weight',
    },
};