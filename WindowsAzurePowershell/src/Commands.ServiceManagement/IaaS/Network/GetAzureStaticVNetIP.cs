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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS.PersistentVMs
{
    using System.Linq;
    using System.Management.Automation;
    using Model;
    using Model.PersistentVMModel;

    [Cmdlet(VerbsCommon.Get, StaticVNetIPNoun), OutputType(typeof(VirtualNetworkStaticIPContext))]
    public class GetAzureStaticVNetIPNounCommand : VirtualMachineConfigurationCmdletBase
    {
        internal void ExecuteCommand()
        {
            var vmRole = VM.GetInstance();
            var networkConfiguration = vmRole.ConfigurationSets.OfType<NetworkConfigurationSet>().SingleOrDefault();
            if (networkConfiguration == null)
            {
                WriteObject(null);
            }
            else
            {
                WriteObject(
                    new VirtualNetworkStaticIPContext
                    {
                        IPAddress = networkConfiguration.StaticVirtualNetworkIPAddress
                    },
                    true);
            }
        }

        protected override void ProcessRecord()
        {
            ExecuteCommand();
        }
    }
}