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

using System;

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.Preview.Network
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using AutoMapper;
    using Management.VirtualNetworks;
    using Management.VirtualNetworks.Models;
    using Model;
    using Utilities.Common;

    [Cmdlet(VerbsCommon.Get, ReservedIPConstants.CmdletNoun), OutputType(typeof(IEnumerable<ReservedIPContext>))]
    public class GetAzureReservedIPCmdlet : ServiceManagementBaseCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Reserved IP Name.")]
        [ValidateNotNullOrEmpty]
        public string Name
        {
            get;
            set;
        }

        public void ExecuteCommand()
        {
            if (Name != null)
            {
                ExecuteClientActionNewSM(null,
                    CommandRuntime.ToString(),
                    () => NetworkClient.Networks.GetReservedIP(Name),
                    (s, r) => new int[1].Select(i => ContextFactory<NetworkReservedIPGetResponse, ReservedIPContext>(r, s)));
            }
            else
            {
                ExecuteClientActionNewSM(null,
                    CommandRuntime.ToString(),
                    () => NetworkClient.Networks.ListReservedIPs(),
                    (s, r) => r.ReservedIPs.Select(p => ContextFactory<NetworkReservedIPListResponse.ReservedIP, ReservedIPContext>(p, s)));
            }
        }

        protected override void OnProcessRecord()
        {
            ServiceManagementPreviewProfile.Initialize();
            this.ExecuteCommand();
        }
    }
}