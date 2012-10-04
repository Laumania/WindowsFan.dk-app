using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WindowsFanDkApp.Analytics;
using WindowsFanDkApp.Api.Models;
using WindowsFanDkApp.Common;

namespace WindowsFanDkApp.Views
{
    public partial class PostCommentView : PhoneApplicationPage
    {
        private readonly Post post = null;
        public PostCommentView()
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);

            //Get post from applicationService
            post = PhoneApplicationService.Current.State["selectedPost"] as Post;

            //Set post as datacontext
            this.DataContext = post;

            //verify that we found a valid post. Else go back to post window
            if(post == null)
                NavigationService.GoBack();

            //enable UIelements
            UIState(true);


            //Get name and email from settings
            var loginInfo = LocalStorageHelper.Current.LoginInfo;
            if (loginInfo != null)
            {
                txtEmail.Text = loginInfo.Email;
                txtName.Text = loginInfo.Name;    
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AnalyticsHelper.TrackPageView("PostCommentView/" + post.Slug);    
        }

        private void btnSubmitComment_Click(object sender, RoutedEventArgs e)
        {
            if (IsStringNullEmptyOrWhiteSpace(txtContent.Text) || IsStringNullEmptyOrWhiteSpace(txtName.Text))
            {
                ShowMessage("Alle felter skal udfyldes.");
                return;
            }
            if (!isEmail(txtEmail.Text))
            {
                ShowMessage("Indtast valid email.");
                return;
            }


            //add email and name to settings
            var loginInfo = new LoginInfo {Email = txtEmail.Text, Name = txtName.Text};

            //Save settings
            LocalStorageHelper.Current.LoginInfo = loginInfo;


            //lock up UIelements so no dobbeltclick / dobbelpost can occur.
            UIState(false);
            
            post.Comments.Add(new Comment(){Content = txtContent.Text + "\r\n", Date = DateTime.Now, Name = txtName.Text});

            ProgressBarLoading(true);

            //create request url
            string postUrl = "http://www.windowsphonefan.dk/?json=respond.submit_comment&post_id=" + post.Id + "&name=" + txtName.Text + "&email=" + txtEmail.Text + "&content=" + txtContent.Text;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(postUrl);
            request.BeginGetResponse(requestResponse, request);
        }

        void requestResponse(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            
            ProgressBarLoading(false);
            if (request != null)
            {
                try
                {
                    WebResponse response = request.EndGetResponse(result);
                    using(StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string res = reader.ReadToEnd();

                        if (res.ToLower().Contains("error"))
                            throw new Exception("Failed to comment on post. " + res);
                        else
                            Deployment.Current.Dispatcher.BeginInvoke(() =>
                                                                          {
                                                                              //update post. Less calls to website
                                                                              PhoneApplicationService.Current.State["selectedPost"] = post;
                                                                              
                                                                              //go back to post page.
                                                                              NavigationService.GoBack();
                                                                          });
                    }
                }
                catch (Exception e)
                {
                    ShowMessage("Det lykkedes ikke at oprette din kommentar. Prøve igen senere.");
                    Debug.WriteLine(e);
                }
            }
        }


        #region Helpers
        //validate email string.
        public bool isEmail(string inputEmail)
        {
            inputEmail = IsStringNullEmptyOrWhiteSpace(inputEmail) ? string.Empty : inputEmail;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private bool IsStringNullEmptyOrWhiteSpace(string input)
        {
            return (string.IsNullOrEmpty(input) && string.IsNullOrWhiteSpace(input));
        }

        private void ShowMessage(string message)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(message);
                UIState(true);
            });
        }

        //for showing loading bar
        private void ProgressBarLoading(bool isLoading)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                                                          {
                                                              GlobalProgressIndicator.Current.IsLoading = isLoading;
                                                          });
        }

        private void UIState(bool enabled)
        {
            txtContent.IsEnabled = enabled;
            txtName.IsEnabled = enabled;
            txtEmail.IsEnabled = enabled;
            btnSubmitComment.IsEnabled = enabled;
        }
        #endregion
    }
}