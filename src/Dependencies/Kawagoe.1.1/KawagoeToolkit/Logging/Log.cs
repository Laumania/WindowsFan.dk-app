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
using System.Net;
using System.Text;

namespace Kawagoe.Logging
{
    /// <summary>
    /// The entry-point class for logging.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// The logger currently in use.
        /// If <c>null</c>, logs are ignored.
        /// </summary>
        public static ILogger CurrentLogger
        {
            get;
            set;
        }

        /// <summary>
        /// Logs the specified error message.
        /// </summary>
        /// <param name="messageFormat">The message to log, interpreted as a composite format string
        /// (see <a href="http://msdn.microsoft.com/en-us/library/system.string.format.aspx">String.Format</a>).</param>
        /// <param name="args">The arguments of the format string.</param>
        public static void Error(string messageFormat, params object[] args)
        {
            if (CurrentLogger == null || string.IsNullOrEmpty(messageFormat))
            {
                return;
            }
            DoLog(LogLevel.Error, DateTime.UtcNow, messageFormat, args);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="error">The exception to log.</param>
        /// <param name="messageFormat">The accompanying message to log, interpreted as a composite format string
        /// (see <a href="http://msdn.microsoft.com/en-us/library/system.string.format.aspx">String.Format</a>).</param>
        /// <param name="args">The arguments of the format string.</param>
        public static void Exception(Exception error, string messageFormat, params object[] args)
        {
            DateTime timestamp = DateTime.UtcNow;
            if (CurrentLogger == null || error == null)
            {
                return;
            }
            StringBuilder exceptionMessage = new StringBuilder();
            try
            {
                exceptionMessage.Append(error.GetType().Name);
                if (!string.IsNullOrEmpty(error.Message))
                {
                    exceptionMessage.AppendFormat(": {0}", error.Message);
                }
                if (error is WebException)
                {
                    WebException webError = (WebException)error;
                    if (webError.Response != null && webError.Response is HttpWebResponse)
                    {
                        HttpWebResponse httpWebResponse = (HttpWebResponse)webError.Response;
                        exceptionMessage.AppendFormat(" (HTTP status {0})", httpWebResponse.StatusCode);
                    }
                }
            }
            catch (Exception) { }
            if (!string.IsNullOrEmpty(messageFormat))
            {
                DoLog(LogLevel.Error, timestamp, messageFormat, args);
            }
            DoLog(LogLevel.Error, timestamp, exceptionMessage.ToString());
        }

        /// <summary>
        /// Logs the specified error message.
        /// </summary>
        /// <param name="messageFormat">The message to log, interpreted as a composite format string
        /// (see <a href="http://msdn.microsoft.com/en-us/library/system.string.format.aspx">String.Format</a>).</param>
        /// <param name="args">The arguments of the format string.</param>
        public static void Debug(string messageFormat, params object[] args)
        {
            if (CurrentLogger == null || string.IsNullOrEmpty(messageFormat))
            {
                return;
            }
            DoLog(LogLevel.Debug, DateTime.UtcNow, messageFormat, args);
        }

        private static void DoLog(LogLevel level, DateTime timestamp, string messageFormat, params object[] args)
        {
            ILogger logger = CurrentLogger;
            if (logger == null)
            {
                return;
            }
            string message;
            try
            {
                message = string.Format(messageFormat, args);
            }
            catch (Exception)
            {
                message = messageFormat;
            }
            try
            {
                logger.Log(level, timestamp, message);
            }
            catch (Exception) { }
        }
    }
}
