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
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Kawagoe.Logging
{
    /// <summary>
    /// Implements a logger that sends log messages to the Output window of the debugger.
    /// </summary>
    public class DebuggerLogger : ILogger
    {
        /// <summary>
        /// Initializes a new <see cref="DebuggerLogger"/> instance.
        /// </summary>
        public DebuggerLogger()
        {
        }

        /// <summary>
        /// Implements <see cref="ILogger.Log"/>.
        /// </summary>
        public void Log(LogLevel level, DateTime timestamp, string message)
        {
            if (!Debugger.IsAttached)
            {
                return;
            }
            StringBuilder completeMessage = new StringBuilder();
            try
            {
                completeMessage.Append(timestamp.ToUniversalTime().ToString("yyMMdd-HHmmss.fff", CultureInfo.InvariantCulture));
                completeMessage.Append(" ");
            }
            catch (Exception) { }
            if (level == LogLevel.Error)
            {
                completeMessage.Append("*** ");
            }
            completeMessage.Append(message);
            Debug.WriteLine(completeMessage.ToString());
        }
    }
}
