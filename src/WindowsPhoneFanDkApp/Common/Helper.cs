using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using WindowsPhoneFanDkApp.Api.Models;

namespace WindowsPhoneFanDkApp.Common
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
