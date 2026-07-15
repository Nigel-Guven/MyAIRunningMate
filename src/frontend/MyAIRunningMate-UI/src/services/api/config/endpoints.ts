export const API_ENDPOINTS = {

    activityView: {
        activityAggregate: '/activity/aggregate',
    },

    analytics: {
        statistics: '/analytics/statistics',
    },

    bestEfforts: {
        allEfforts: '/best-efforts/all-efforts',
    },

    calendar: {
        display: '/calendar/display',
        trainingPlan: '/calendar/training-plan',
    },

    dashboard: {
        insights: '/dashboard/insights',
        fitnessProfile: '/dashboard/fitness-profile',
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

    weight: {
        latest: '/weight-stat/latest',
        history: '/weight-stat/history',
        logWeight: '/weight-stat/log-weight',
    },
};