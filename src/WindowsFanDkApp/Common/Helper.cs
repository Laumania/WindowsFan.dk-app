using System.IO.IsolatedStorage;
using WindowsFanDkApp.Api.Models;

namespace WindowsFanDkApp.Common
{
    public static class Helper
    {
        public static Settings GetSettings()
        {
            


            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
             
            return storage.Contains("settings") ? storage["settings"] as Settings : SetDefaultSettings();
        }

        public static void SaveSettings(Settings settings)
        {
            IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;

            storage["settings"] = settings;
        }

        public static Settings SetDefaultSettings()
        {
            Settings defaultSettings = new Settings();

            defaultSettings.FeedsIds.Enqueue(7);
            defaultSettings.FeedsIds.Enqueue(12);
            defaultSettings.FeedsIds.Enqueue(374);
            defaultSettings.FeedsIds.Enqueue(3);
            defaultSettings.FeedsIds.Enqueue(167);

            defaultSettings.Name = string.Empty;
            defaultSettings.Email = string.Empty;

            return defaultSettings;
        }
    }
}
