export const API_ENDPOINTS = {

    activityView: {
        activityAggregate: '/activity/aggregate',
    },

    bestEfforts: {
        allEfforts: '/best_efforts/all_efforts',
        updateEffort: '/best_efforts/update',
    },

    calendar: {
        display: '/calendar/display',
        trainingPlan: '/calendar/training-plan',
    },

    dashboard: {
        volume: '/dashboard/volume',
        insights: '/dashboard/insights',
    },

    events: {
        upcomingEvents: '/events/upcoming',
        primaryEvent: '/events/primary',
    },
    
    ingestion: {
        upload: '/ingestion/upload',
    },

    nexus: {
        generate: '/nexus/generate',
        finalize: '/nexus/finalize',
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