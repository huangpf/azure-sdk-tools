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

namespace Microsoft.WindowsAzure.Management.ServiceManagement.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using Properties;
    using Utilities.Common;
    using WindowsAzure.ServiceManagement;

    /// <summary>
    /// Remove Windows Azure Service Domain Join Extension.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, DomainJoinExtensionNoun, DefaultParameterSetName = RemoveByRolesParameterSet), OutputType(typeof(ManagementOperationContext))]
    public class RemoveAzureServiceDomainJoinExtensionCommand : BaseAzureServiceDomainJoinExtensionCmdlet
    {
        public RemoveAzureServiceDomainJoinExtensionCommand()
            : base()
        {
        }

        public RemoveAzureServiceDomainJoinExtensionCommand(IServiceManagement channel)
            : base(channel)
        {
        }

        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = RemoveByRolesParameterSet, HelpMessage = ServiceNameHelpMessage)]
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = RemoveAllRolesParameterSet, HelpMessage = ServiceNameHelpMessage)]
        public override string ServiceName
        {
            get;
            set;
        }

        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = RemoveByRolesParameterSet, HelpMessage = SlotHelpMessage)]
        [Parameter(Position = 1, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = RemoveAllRolesParameterSet, HelpMessage = SlotHelpMessage)]
        [ValidateSet(DeploymentSlotType.Production, DeploymentSlotType.Staging, IgnoreCase = true)]
        public override string Slot
        {
            get;
            set;
        }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = RemoveByRolesParameterSet, HelpMessage = RoleHelpMessage)]
        public override string[] Role
        {
            get;
            set;
        }

        [Parameter(Position = 2, Mandatory = true, ParameterSetName = RemoveAllRolesParameterSet, HelpMessage = UninstallConfigurationHelpMessage)]
        public override SwitchParameter UninstallConfiguration
        {
            get;
            set;
        }

        protected override void ValidateParameters()
        {
            base.ValidateParameters();
            ValidateService();
            ValidateDeployment();
            ValidateRoles();
        }

        public void ExecuteCommand()
        {
            ValidateParameters();
            RemoveExtension();
        }

        protected override void OnProcessRecord()
        {
            ExecuteCommand();
        }
    }
}
