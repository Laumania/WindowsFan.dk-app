using System;
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

namespace WindowsFanDkApp.Common
{
    public class LocalStorageHelper
    {
        private static LocalStorageHelper _current;
        private IsolatedStorageSettings _settings;

        private const string LoginInfoKey = "LoginInfo";

        private LocalStorageHelper()
        {
            _settings = IsolatedStorageSettings.ApplicationSettings;
        }

        private T Get<T>(string key)
        {
            T value;
            if (_settings.TryGetValue(key, out value))
                return value;
            else
                return default(T);
        }

        private void Set<T>(string key, T value)
        {
            _settings[key] = value;
        }

        public LoginInfo LoginInfo
        {
            get
            {
                return Get<LoginInfo>(LoginInfoKey);
            }
            set
            {
                Set(LoginInfoKey, value);
            }
        }

        public static LocalStorageHelper Current
        {
            get
            {
                if (_current == null)
                    _current = new LocalStorageHelper();
                return _current;
            }
        }
    }

    public class LoginInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
