using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
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
            ShellTile activeLiveTile = ShellTile.ActiveTiles.FirstOrDefault();
            if (null != activeLiveTile)
            {
                var recentposts = DataManager.Current.Load<RecentPosts>(-1);
                recentposts.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == "Posts")
                        {
                            var latestPost = recentposts.Posts.FirstOrDefault();
                            if (latestPost != null)
                            {
                                var bitmap = new BitmapImage();
                                bitmap.CreateOptions = BitmapCreateOptions.None;
                                bitmap.UriSource = new Uri(latestPost.Thumbnail, UriKind.Absolute);
                                //bitmap.ImageFailed += (o, eventArgs) => NotifyComplete();
                                //bitmap.ImageOpened += (o, eventArgs) =>
                                //    {
                                        Grid grid = new Grid();
                                        Image img = new Image();
                                        img.Source = bitmap;

                                        // add Image to Grid
                                        grid.Children.Add(img);

                                        TextBlock text = new TextBlock()
                                        {
                                            FontSize = 11,
                                            Foreground = new SolidColorBrush(Colors.Black),
                                            TextWrapping = TextWrapping.Wrap,
                                        };

                                        text.Text = "Test";
                                        grid.Children.Add(text);


                                        // this is our final image containing custom text and image
                                        WriteableBitmap wbmp = new WriteableBitmap(173, 173);

                                        // now render everything - this image can be used as background for tile
                                        wbmp.Render(grid, null);
                                        wbmp.Invalidate();

                                        using (var stream = IsolatedStorageFile.GetUserStoreForApplication().CreateFile("/Shared/ShellContent/WindowsFanDkLiveTile.png"))
                                        {
                                            wbmp.SaveJpeg(stream, 173, 173, 0, 100);
                                        }

                                        var data = new StandardTileData();
                                        data.BackBackgroundImage = new Uri("isostore:/Shared/ShellContent/tile.png", UriKind.Absolute);
                                        //data.Title = "updated image";

                                        activeLiveTile.Update(data);
                                        
                                    //};
                                
                                //// creata a new data for tile
                                //var data = new StandardTileData();
                                //// tile foreground data
                                ////data.Title = latestPost.TitlePlain;
                                ////data.BackgroundImage = new Uri("/Content/CommentsIcon.png", UriKind.Relative);
                                ////data.BackgroundImage = new Uri(latestPost.Thumbnail, UriKind.Absolute);
                                ////data.Count = 1;
                                //// to make tile flip add data to background also
                                ////data.BackTitle = "Secret text here";

                                ////Todo: http://windcape.posterous.com/how-to-generate-a-custom-live-tile-directly-o
                                //data.BackBackgroundImage = new Uri(latestPost.Thumbnail, UriKind.Absolute);
                                //data.BackContent = latestPost.TitlePlain;
                                //// update tile
                                //tile.Update(data);    
                            }
                            NotifyComplete();    
                        }
                    };
            }

#if DEBUG
	ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(5));
	System.Diagnostics.Debug.WriteLine("Periodic task is started again: " + task.Name);
#endif
        }
    }
}