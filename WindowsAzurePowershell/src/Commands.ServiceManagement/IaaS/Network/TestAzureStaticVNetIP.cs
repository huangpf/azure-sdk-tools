﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Management.Automation;
    using System.Net;
    using System.Xml.Serialization;
    using Management.VirtualNetworks;
    using Management.VirtualNetworks.Models;
    using Model;
    using Properties;
    using Utilities.Common;

    [Cmdlet(VerbsDiagnostic.Test, "AzureStaticVNetIP"), OutputType(typeof(VirtualNetworkStaticIPAvailabilityContext))]
    public class TestAzureStaticVNetIPCommand : ServiceManagementBaseCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "The virtual network name.")]
        [ValidateNotNullOrEmpty]
        public string VNetName
        {
            get;
            set;
        }

        [Parameter(Position = 1, Mandatory = false, HelpMessage = "The static IP address.")]
        [ValidateNotNullOrEmpty]
        public string IPAddress
        {
            get;
            set;
        }

        protected override void OnProcessRecord()
        {
            ServiceManagementProfile.Initialize();
            ExecuteClientActionNewSM(
                null,
                this.CommandRuntime.ToString(),
                () => this.NetworkClient.StaticIPs.Check(VNetName, IPAddress),
                (operation, response) => ContextFactory<NetworkStaticIPAvailabilityResponse, VirtualNetworkStaticIPAvailabilityContext>(response, operation));
        }
    }
}