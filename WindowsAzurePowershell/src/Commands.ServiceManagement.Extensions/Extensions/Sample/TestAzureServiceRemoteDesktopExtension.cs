// ----------------------------------------------------------------------------------
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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.Extensions
{
    using System.Linq;
    using System.Management.Automation;
    using Management.Compute;
    using Model.PersistentVMModel;

    /// <summary>
    /// Test Windows Azure Service Remote Desktop Extension.
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Test, "AzureServiceRemoteDesktopExtension"), OutputType(typeof(RemoteDesktopExtensionContext))]
    public class TestAzureServiceRemoteDesktopExtensionCommand : GetAzureServiceRemoteDesktopExtensionCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "Service Name")]
        public override string ServiceName
        {
            get;
            set;
        }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, HelpMessage = "Deployment Slot: Production (default) or Staging")]
        [ValidateSet(DeploymentSlotType.Production, DeploymentSlotType.Staging, IgnoreCase = true)]
        public override string Slot
        {
            get;
            set;
        }

        protected override void OnProcessRecord()
        {
            base.OnProcessRecord();
        }
    }
}
