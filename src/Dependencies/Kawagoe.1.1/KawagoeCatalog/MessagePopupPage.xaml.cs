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
using System.Windows;
using Kawagoe.Controls;
using Microsoft.Devices;
using Microsoft.Phone.Controls;

namespace KawagoeCatalog
{
    public partial class MessagePopupPage : PhoneApplicationPage
    {
        public MessagePopupPage()
        {
            InitializeComponent();
        }

        private void ShowSimplePopup(object sender, RoutedEventArgs e)
        {
            MessagePopup popup = new MessagePopup();
            popup.Title = "simple popup";
            popup.Message =
@"This is a simple message popup.
When it opens:
- the content of the page below is darkened and becomes unresponsive to touch events;
- the application bar is hidden;
- the device vibrates.
It is dismissed by tapping the ok button or by pressing the Back key.";
            popup.AddCancelButton("ok", null, null);
            popup.Open();
        }

        private void ShowOkCancelPopup(object sender, RoutedEventArgs e)
        {
            MessagePopup popup = new MessagePopup();
            popup.Title = "ok/cancel popup";
            popup.Message =
@"This is a message popup with an ok button and a cancel button.
It is dismissed by tapping one of its buttons or by pressing the Back key.
The device vibrates when the cancel button is tapped and when the Back key is pressed.";
            popup.AddButton("ok", null, null);
            popup.AddCancelButton("cancel", (s, state) => { VibrateController.Default.Start(TimeSpan.FromMilliseconds(100)); }, null);
            popup.Open();
        }

        private void Show3ButtonsPopup(object sender, RoutedEventArgs e)
        {
            MessagePopup popup = new MessagePopup();
            popup.Title = "3 buttons popup";
            popup.Message =
@"This is a message popup with three buttons.
It is dismissed by tapping one of its buttons or by pressing the Back key.
The device vibrates when the cancel button is tapped and when the Back key is pressed.";
            popup.AddButton("ok", null, null);
            popup.AddCancelButton("cancel", (s, state) => { VibrateController.Default.Start(TimeSpan.FromMilliseconds(100)); }, null);
            popup.AddButton("help", null, null);
            popup.Open();
        }

        private void ShowNoButtonPopup(object sender, RoutedEventArgs e)
        {
            MessagePopup popup = new MessagePopup();
            popup.Title = "no button popup";
            popup.Message =
@"This is a message popup with no button at all.
Pressing the Back key (which is the only way to get out of here as user interaction is disabled below the popup) will close the MessagePopup page and go back to the previous one.";
            popup.Open();
        }

        private void ShowScrollingPopup(object sender, RoutedEventArgs e)
        {
            MessagePopup popup = new MessagePopup();
            popup.Title = "scrolling popup";
            popup.Message =
@"This is a message popup with a long and meaningless message that requires scrolling. Let's go:
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer placerat tempor nibh et auctor. Sed ipsum turpis, gravida vel consectetur nec, commodo non erat. Etiam iaculis bibendum tellus a egestas. Suspendisse euismod euismod tortor ut condimentum. Cras suscipit nulla et erat vulputate sed suscipit turpis ultrices. Nam lacinia ipsum feugiat magna malesuada in sodales mi tincidunt. Nam tincidunt, erat eget dictum molestie, elit odio faucibus magna, at dignissim massa nibh quis nunc. Vivamus mi leo, fringilla placerat tempus a, luctus eget nulla. Proin tristique felis eu elit aliquet pulvinar vestibulum mi rhoncus. Vestibulum tincidunt dictum erat sed congue. Pellentesque consequat, sapien non tempor rutrum, lacus neque consequat elit, vel venenatis justo purus ac lectus. Etiam rhoncus blandit sapien et viverra. Quisque malesuada cursus ante, adipiscing ornare metus pretium ut. Maecenas eget tortor tempor leo ultricies tempor eget eget massa. Nulla sollicitudin, est a vehicula lacinia, purus lacus molestie velit, a faucibus nulla orci et ligula. Mauris hendrerit vehicula quam sed imperdiet.
Quisque quis eros tortor, quis rhoncus est. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nunc facilisis ligula vitae odio cursus pellentesque. Praesent consectetur sem id magna sagittis lacinia. Cras ultrices posuere laoreet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut nisi est, bibendum eget euismod varius, malesuada nec nulla. Pellentesque imperdiet lectus eget justo posuere vel lacinia massa semper. Ut vitae ligula metus. Quisque lacinia dictum diam nec vulputate. Nam aliquet dictum nunc, condimentum laoreet lorem ornare sit amet. Suspendisse potenti. Etiam quis ante ut nunc laoreet malesuada. Nam id fermentum ante.";
            popup.AddCancelButton("ok", null, null);
            popup.Open();
        }

        private void ShowNoVibrationPopup(object sender, RoutedEventArgs e)
        {
            MessagePopup popup = new MessagePopup();
            popup.Title = "no vibration popup";
            popup.Message = @"The device does not vibrate when this message popup opens.";
            popup.AddCancelButton("ok", null, null);
            popup.VibrationDuration = TimeSpan.Zero;
            popup.Open();
        }
    }
}