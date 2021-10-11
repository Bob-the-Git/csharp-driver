//
//      Copyright (C) DataStax Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//

using System;
using System.Diagnostics;
using System.IO;
using Cassandra.IntegrationTests.TestClusterManagement;
using NUnit.Framework;

namespace Cassandra.IntegrationTests
{
    [SetUpFixture]
    public class CommonFixtureSetup
    {
        [OneTimeSetUp]
        public void SetupTestSuite()
        {
            Diagnostics.CassandraTraceSwitch.Level = TraceLevel.Warning;
            if (Environment.GetEnvironmentVariable("TEST_TRACE")?.ToUpper() != "OFF")
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Console.Error));
            }
            Trace.TraceInformation("Starting Test Run ...");
        }

        [OneTimeTearDown]
        public void TearDownTestSuite()
        {
            // this method is executed once after all the fixtures have completed execution
            TestClusterManager.TryRemove();
            SimulacronManager.DefaultInstance.Stop();
            TestCloudClusterManager.TryRemove();
            Trace.Flush();
        }
    }
}
