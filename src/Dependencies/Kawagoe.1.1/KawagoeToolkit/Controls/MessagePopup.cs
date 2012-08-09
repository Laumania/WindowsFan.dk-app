// Copyright 2010 Andreas Saudemont (andreas.saudemont@gmail.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Kawagoe.Controls
{
    /// <summary>
    /// Represents a method that will handle a click event from a <see cref="MessagePopup"/> button.
    /// </summary>
    /// <param name="sender">The <see cref="MessagePopup"/> instance the button belongs to.</param>
    /// <param name="state">The state object associated with the button,
    /// as specified when the button has been added to the <see cref="MessagePopup"/>.</param>
    public delegate void MessagePopupButtonHandler(MessagePopup sender, object state);

    /// <summary>
    /// Provides a non-modal message popup with customizable buttons.
    /// </summary>
    /// <remarks>
    /// <para>A message popup has the following characteristics:</para>
    /// <list type="bullet">
    /// <item><description>It is non-modal and does not block the UI thread when opened.</description></item>
    /// <item><description>It can have any number of buttons, even none.</description></item>
    /// <item><description>It can be configured to react to Back key presses when opened.</description></item>
    /// </list>
    /// </remarks>
    public class MessagePopup
    {
        /// <summary>
        /// The default value for <see cref="MessagePopup.VibrationDuration"/>.
        /// </summary>
        public static readonly TimeSpan DefaultVibrationDuration = TimeSpan.FromMilliseconds(75);

        private const double SystemTrayHeight = 32;

        private readonly Popup _popup = new Popup();
        private readonly MessagePopupContent _content = new MessagePopupContent();
        private bool _hasCancelButton = false;
        private MessagePopupButtonHandler _cancelHandler;
        private object _cancelState = null;
        private IApplicationBar _appBarToShowOnClose;

        /// <summary>
        /// Initializes a new <see cref="MessagePopup"/> instance.
        /// </summary>
        public MessagePopup()
            : base()
        {
            VisualStateManager.GoToState(_content, _content.HiddenState.Name, false);
            _popup.Child = _content;
            VibrationDuration = DefaultVibrationDuration;
        }

        /// <summary>
        /// Indicates whether the message popup is currently open.
        /// </summary>
        public bool IsOpen
        {
            get;
            private set;
        }

        /// <summary>
        /// The title of the message popup (none if null or empty).
        /// </summary>
        public string Title
        {
            get
            {
                return _content.TitleTextBlock.Text;
            }
            set
            {
                _content.TitleTextBlock.Text = value;
            }
        }

        /// <summary>
        /// The text of the message popup (none if null or empty).
        /// </summary>
        public string Message
        {
            get
            {
                return _content.MessageTextBlock.Text;
            }
            set
            {
                _content.MessageTextBlock.Text = value;
            }
        }

        /// <summary>
        /// The duration of the vibration when the message popup is opened.
        /// If set to TimeSpan.Zero, the device won't vibrate when the message popup is opened.
        /// </summary>
        public TimeSpan VibrationDuration
        {
            get;
            set;
        }

        /// <summary>
        /// Adds a button to the message popup.
        /// </summary>
        /// <param name="label">The label of the button.</param>
        /// <param name="handler">The delegate method that will be called when the button is clicked (null allowed).</param>
        /// <param name="state">The state object sent to <paramref name="handler"/> when the button is clicked.</param>
        public void AddButton(string label, MessagePopupButtonHandler handler, object state)
        {
            int row = (_content.ButtonsGrid.Children.Count / 2);
            int column = (_content.ButtonsGrid.Children.Count % 2);
            while (_content.ButtonsGrid.RowDefinitions.Count <= row)
            {
                _content.ButtonsGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            Button button = new Button();
            button.Content = label;
            button.FontSize = _content.MessageTextBlock.FontSize;
            button.Click += (sender, e) => OnButtonClicked(handler, state);
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);
            _content.ButtonsGrid.Children.Add(button);
        }

        /// <summary>
        /// Adds a cancel button to the message popup.
        /// </summary>
        /// <remarks>
        /// A <see cref="MessagePopup"/> instance can have at most one cancel button.
        /// If one is present, its handler will be called when the user presses the Back key on the device
        /// while the message popup is open.
        /// If there is no cancel button, the message popup does not intercept Back key presses.
        /// </remarks>
        /// <param name="label">The label of the button.</param>
        /// <param name="handler">The delegate method that will be called when the button is clicked
        /// or when the user presses the Back key.</param>
        /// <param name="state">The state object sent to <paramref name="handler"/> when the button is clicked
        /// or when the user presses the Back key.</param>
        /// <exception cref="InvalidOperationException">If the message popup already has a cancel button.</exception>
        public void AddCancelButton(string label, MessagePopupButtonHandler handler, object state)
        {
            if (_hasCancelButton)
            {
                throw new InvalidOperationException();
            }
            _hasCancelButton = true;
            _cancelHandler = handler;
            _cancelState = state;
            AddButton(label, handler, state);
        }

        /// <summary>
        /// Opens the message popup.
        /// This method does nothing if the message popup is already opened.
        /// </summary>
        public void Open()
        {
            if (IsOpen)
            {
                return;
            }
            IsOpen = true;
            _appBarToShowOnClose = null;
            PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (frame != null)
            {
                PhoneApplicationPage currentPage = frame.Content as PhoneApplicationPage;
                if (currentPage != null)
                {
                    IApplicationBar appBar = currentPage.ApplicationBar;
                    if (appBar != null && appBar.IsVisible)
                    {
                        _appBarToShowOnClose = appBar;
                        appBar.IsVisible = false;
                    }

                    currentPage.BackKeyPress += OnBackKeyPress;
                }
            }

            AdjustLayout();
            
            _popup.IsOpen = true;
            VisualStateManager.GoToState(_content, _content.VisibleState.Name, true);

            if (VibrationDuration.TotalMilliseconds > 0)
            {
                VibrateController.Default.Start(VibrationDuration);
            }
        }

        /// <summary>
        /// Explicitely closes the message popup.
        /// This method does nothing if the message popup is not currently opened.
        /// </summary>
        /// <remarks>
        /// A message popup automatically closes when one of its button is clicked.
        /// </remarks>
        public void Close()
        {
            if (!IsOpen)
            {
                return;
            }
            IsOpen = false;
            if (_appBarToShowOnClose != null)
            {
                _appBarToShowOnClose.IsVisible = true;
            }
            PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (frame != null)
            {
                PhoneApplicationPage currentPage = frame.Content as PhoneApplicationPage;
                if (currentPage != null)
                {
                    currentPage.BackKeyPress -= OnBackKeyPress;
                }
            }
            _content.HiddenState.Storyboard.Completed += OnHiddenStateStoryboardCompleted;
            VisualStateManager.GoToState(_content, _content.HiddenState.Name, true);
        }

        /// <summary>
        /// Called when the Hidden state's storyboard completes.
        /// </summary>
        private void OnHiddenStateStoryboardCompleted(object sender, EventArgs e)
        {
            _content.HiddenState.Storyboard.Completed -= OnHiddenStateStoryboardCompleted;
            _popup.IsOpen = false;
        }

        /// <summary>
        /// Adjusts the layout of the message popup, taking into account the screen size and the system tray visibility.
        /// </summary>
        private void AdjustLayout()
        {
            if (SystemTray.IsVisible)
            {
                _popup.VerticalOffset = SystemTrayHeight;
            }
            _content.Width = Application.Current.Host.Content.ActualWidth;
            _content.Height = Application.Current.Host.Content.ActualHeight - _popup.VerticalOffset;
            _content.MessageGrid.MaxHeight = Application.Current.Host.Content.ActualHeight - _popup.VerticalOffset;
        }

        /// <summary>
        /// Called when a button of the message popup is clicked.
        /// </summary>
        private void OnButtonClicked(MessagePopupButtonHandler handler, object state)
        {
            InvokeHandler(handler, state);
            Close();
        }

        /// <summary>
        /// Called when the user presses the Back key.
        /// </summary>
        private void OnBackKeyPress(object sender, CancelEventArgs e)
        {
            if (_hasCancelButton)
            {
                e.Cancel = true;
                InvokeHandler(_cancelHandler, _cancelState);
            }
            Close();
        }

        /// <summary>
        /// Invokes the specified button handler.
        /// </summary>
        private void InvokeHandler(MessagePopupButtonHandler handler, object state)
        {
            if (handler != null)
            {
                try
                {
                    handler(this, state);
                }
                catch (Exception) { }
            }
        }
    }
}
