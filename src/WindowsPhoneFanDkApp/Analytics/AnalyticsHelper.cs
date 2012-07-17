using MC.Phone.Analytics;

namespace WindowsPhoneFanDkApp.Analytics
{
    public class AnalyticsHelper
    {
        private static AnalyticsTracker _instance;
        private static AnalyticsTracker Current
        {
            get
            {
                if (_instance == null)
                    _instance = new AnalyticsTracker();
                return _instance;
            }
        }

        public static void TrackEvent(string name, TrackingCategory category, TrackingAction action)
        {
            Current.Track(category.ToString(), name, action.ToString());
        }

        public static void TrackEvent(string name, TrackingCategory category)
        {
            Current.Track(category.ToString(), name);
        }

        public static void TrackPageView(string pageName)
        {
            Current.TrackPage(pageName);
        }
    }

    public enum TrackingCategory
    {
        None //Todo: Implement more when we need it
    }

    public enum TrackingAction
    {
        None //Todo: Implement more when we need it
    }
}
