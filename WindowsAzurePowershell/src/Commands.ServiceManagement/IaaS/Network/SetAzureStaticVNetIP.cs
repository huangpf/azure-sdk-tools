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
    using System.Linq;
    using System.Management.Automation;
    using Model;
    using Model.PersistentVMModel;

    [Cmdlet(VerbsCommon.Set, StaticVNetIPNoun), OutputType(typeof(IPersistentVM))]
    public class SetAzureStaticVNetIPCommand : VirtualMachineConfigurationCmdletBase
    {
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The Static Customer IP Address.")]
        [ValidateNotNullOrEmpty]
        public string IPAddress
        {
            get;
            set;
        }

        internal void ExecuteCommand()
        {
            var vmRole = VM.GetInstance();
            var networkConfiguration = vmRole.ConfigurationSets.OfType<NetworkConfigurationSet>().SingleOrDefault();
            if (networkConfiguration == null)
            {
                networkConfiguration = new NetworkConfigurationSet();
                vmRole.ConfigurationSets.Add(networkConfiguration);
            }

            networkConfiguration.StaticVirtualNetworkIPAddress = IPAddress;
            WriteObject(VM, true);
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();
                ExecuteCommand();
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.CloseError, null));
            }
        }
    }
}