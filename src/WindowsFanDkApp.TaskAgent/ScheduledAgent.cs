using System;
using System.Linq;
using System.Windows;
using AgFx;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Api.Models;

namespace WindowsFanDkApp.TaskAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            // get application tile
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (null != tile)
            {
                var recentposts = DataManager.Current.Load<RecentPosts>(-1);
                recentposts.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == "Posts")
                        {
                            var latestPost = recentposts.Posts.FirstOrDefault();
                            if (latestPost != null)
                            {
                                // creata a new data for tile
                                var data = new StandardTileData();
                                // tile foreground data
                                //data.Title = latestPost.TitlePlain;
                                //data.BackgroundImage = new Uri("/Content/CommentsIcon.png", UriKind.Relative);
                                //data.BackgroundImage = new Uri(latestPost.Thumbnail, UriKind.Absolute);
                                //data.Count = 1;
                                // to make tile flip add data to background also
                                //data.BackTitle = "Secret text here";

                                //Todo: http://windcape.posterous.com/how-to-generate-a-custom-live-tile-directly-o
                                data.BackBackgroundImage = new Uri(latestPost.Thumbnail, UriKind.Absolute);
                                data.BackContent = latestPost.TitlePlain;
                                // update tile
                                tile.Update(data);    
                            }
                            NotifyComplete();
                        }
                    };
            }

#if DEBUG
	ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(30));
	System.Diagnostics.Debug.WriteLine("Periodic task is started again: " + task.Name);
#endif
        }
    }
}