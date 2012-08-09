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
using System.Windows.Threading;
using Kawagoe.Threading;
using Microsoft.Phone.Controls;

namespace KawagoeCatalog
{
    public partial class OneShotDispatcherTimerPage : PhoneApplicationPage
    {
        private static readonly TimeSpan OneShotTimerDuration = TimeSpan.FromSeconds(3);
        private static readonly TimeSpan CounterTimerInterval = TimeSpan.FromMilliseconds(90);

        private readonly OneShotDispatcherTimer _oneShotTimer;
        private DispatcherTimer _counterTimer = null;
        private DateTime _counterOrigin;

        public OneShotDispatcherTimerPage()
        {
            InitializeComponent();

            _oneShotTimer = new OneShotDispatcherTimer();
            _oneShotTimer.Duration = OneShotTimerDuration;
            _oneShotTimer.Fired += OnOneShotTimerFired;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateButtons();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            StopOneShotTimer();
            StopCounterTimer();
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            StartOneShotTimer();
        }

        private void OnStopButtonClick(object sender, RoutedEventArgs e)
        {
            StopOneShotTimer();
        }

        private void StartOneShotTimer()
        {
            if (!_oneShotTimer.IsStarted)
            {
                StatusTextBlock.Text = "Running...";
                _oneShotTimer.Start();
                StartCounterTimer();
            }
            UpdateButtons();
        }

        private void StopOneShotTimer()
        {
            if (_oneShotTimer.IsStarted)
            {
                _oneShotTimer.Stop();
                StatusTextBlock.Text = "Stopped";
                StopCounterTimer();
            }
            UpdateButtons();
        }

        private void OnOneShotTimerFired(object sender, EventArgs e)
        {
            StatusTextBlock.Text = "Fired";
            StopCounterTimer();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            StartButton.IsEnabled = !_oneShotTimer.IsStarted;
            StopButton.IsEnabled = _oneShotTimer.IsStarted;
        }

        private void StartCounterTimer()
        {
            if (_counterTimer != null)
            {
                return;
            }
            _counterOrigin = DateTime.Now;
            _counterTimer = new DispatcherTimer();
            _counterTimer.Interval = CounterTimerInterval;
            _counterTimer.Tick += OnCounterTimerTick;
            _counterTimer.Start();
        }

        private void StopCounterTimer()
        {
            if (_counterTimer == null)
            {
                return;
            }
            _counterTimer.Stop();
            _counterTimer = null;
            CounterTextBlock.Text = "";
        }

        private void OnCounterTimerTick(object sender, EventArgs e)
        {
            if (sender != _counterTimer)
            {
                return;
            }
            TimeSpan counterSpan = DateTime.Now.Subtract(_counterOrigin);
            CounterTextBlock.Text = string.Format("{0}.{1} s", (int)counterSpan.TotalSeconds, (int)(counterSpan.Milliseconds / 100));
        }
    }
}