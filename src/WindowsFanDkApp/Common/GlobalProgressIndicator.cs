using System.ComponentModel;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WindowsFanDkApp.Common
{
    public class GlobalProgressIndicator : INotifyPropertyChanged
    {
        private static GlobalProgressIndicator _in;
        private int _loadingCount;
        private ProgressIndicator _indicator;

        private GlobalProgressIndicator()
        {
        }

        public static GlobalProgressIndicator Current
        {
            get
            {
                if (_in == null)
                {
                    _in = new GlobalProgressIndicator();
                }

                return _in;
            }
        }

        public bool IsDataManagerLoading { get; set; }

        public bool ActualIsLoading
        {
            get { return IsLoading || IsDataManagerLoading; }
        }

        public bool IsLoading
        {
            get { return _loadingCount > 0; }
            set
            {
                bool loading = IsLoading;
                if (value)
                {
                    ++_loadingCount;
                }
                else
                {
                    --_loadingCount;
                }

                NotifyValueChanged();
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void Initialize(PhoneApplicationFrame frame)
        {
            _indicator = new ProgressIndicator();
            frame.Navigated += OnRootFrameNavigated;
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            // Use in Mango to share a single progress indicator instance.
            object ee = e.Content;
            var pp = ee as PhoneApplicationPage;
            if (pp != null)
            {
                pp.SetValue(SystemTray.ProgressIndicatorProperty, _indicator);
            }
        }

        private void OnDataManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ("IsLoading" == e.PropertyName)
            {
                NotifyValueChanged();
            }
        }

        private void NotifyValueChanged()
        {
            if (_indicator != null)
            {
                _indicator.IsIndeterminate = _loadingCount > 0 || IsDataManagerLoading;

                // for now, just make sure it's always visible.
                if (_indicator.IsVisible == false)
                {
                    _indicator.IsVisible = true;
                }
            }
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
