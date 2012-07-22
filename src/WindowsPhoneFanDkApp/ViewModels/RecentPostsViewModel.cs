using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using AgFx;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using WindowsPhoneFanDkApp.Common;
using WindowsPhoneFanDkApp.Data;
using WindowsPhoneFanDkApp.Api.Models;

namespace WindowsPhoneFanDkApp.ViewModels
{
    public class RecentPostsViewModel : ViewModelBase
    {
        private RecentPosts _recentPosts;

        public RecentPostsViewModel()
        {
            _recentPosts = DataManager.Current.Load<RecentPosts>(-1); // We have no identifiers for this object.
            _recentPosts.PropertyChanged += RecentPostsOnPropertyChanged;
        }

        private void RecentPostsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Status = _recentPosts.Status.ToString();
            Posts = _recentPosts.Posts;
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        private readonly ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set 
            { 
                if (_posts != null)
                {
                    _posts.Clear();
                    foreach (var report in value)
                    {
                        _posts.Add(report);
                    }
                }
                RaisePropertyChanged("Posts");
            }
        }
    }
}
