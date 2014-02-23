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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS.Extensions
{
    using Model;
    using Model.PersistentVMModel;
    using Properties;
    using System;
    using System.Linq;
    using System.Management.Automation;

    [Cmdlet(
        VerbsCommon.Set,
        VirtualMachineExtensionNoun,
        DefaultParameterSetName = SetByExtensionParamSetName),
    OutputType(
        typeof(IPersistentVM))]
    public class SetAzureVMExtensionCommand : VirtualMachineExtensionCmdletBase
    {
        protected const string SetByExtensionParamSetName = "SetByExtensionName";
        protected const string SetByReferenceParamSetName = "SetByReferenceName";
        protected const string SetByExtensionAndConfigFileParamSetName = "SetByExtensionNameAndConfigFile";
        protected const string SetByReferenceAndConfigFileParamSetName = "SetByReferenceNameAndConfigFile";

        [Parameter(
            ParameterSetName = SetByExtensionParamSetName,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Name.")]
        [Parameter(
            ParameterSetName = SetByExtensionAndConfigFileParamSetName,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Name.")]
        [ValidateNotNullOrEmpty]
        public override string ExtensionName { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionParamSetName,
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Publisher.")]
        [Parameter(
            ParameterSetName = SetByExtensionAndConfigFileParamSetName,
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Publisher.")]
        [ValidateNotNullOrEmpty]
        public override string Publisher { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionParamSetName,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Version.")]
        [Parameter(
            ParameterSetName = SetByExtensionAndConfigFileParamSetName,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Version.")]
        [ValidateNotNullOrEmpty]
        public override string Version { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionParamSetName,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Reference Name.")]
        [Parameter(
            ParameterSetName = SetByReferenceParamSetName,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Reference Name.")]
        [Parameter(
            ParameterSetName = SetByExtensionAndConfigFileParamSetName,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Reference Name.")]
        [Parameter(
            ParameterSetName = SetByReferenceAndConfigFileParamSetName,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Reference Name.")]
        [ValidateNotNullOrEmpty]
        public override string ReferenceName { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionParamSetName,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Public Configuration.")]
        [Parameter(
            ParameterSetName = SetByReferenceParamSetName,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Public Configuration.")]
        [ValidateNotNullOrEmpty]
        public override string PublicConfiguration { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionAndConfigFileParamSetName,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Public Configuration.")]
        [Parameter(
            ParameterSetName = SetByReferenceAndConfigFileParamSetName,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Public Configuration.")]
        [ValidateNotNullOrEmpty]
        public override string PublicConfigPath { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionParamSetName,
            Position = 6,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Private Configuration.")]
        [Parameter(
            ParameterSetName = SetByReferenceParamSetName,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Private Configuration.")]
        [ValidateNotNullOrEmpty]
        public override string PrivateConfiguration { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionAndConfigFileParamSetName,
            Position = 6,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Private Configuration.")]
        [Parameter(
            ParameterSetName = SetByReferenceAndConfigFileParamSetName,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Private Configuration.")]
        [ValidateNotNullOrEmpty]
        public override string PrivateConfigPath { get; set; }

        [Parameter(
            ParameterSetName = SetByExtensionParamSetName,
            Position = 7,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "To Set the Extension State to 'Disable'.")]
        [Parameter(
            ParameterSetName = SetByReferenceParamSetName,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "To Set the Extension State to 'Disable'.")]
        [Parameter(
            ParameterSetName = SetByExtensionAndConfigFileParamSetName,
            Position = 7,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "To Set the Extension State to 'Disable'.")]
        [Parameter(
            ParameterSetName = SetByReferenceAndConfigFileParamSetName,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "To Set the Extension State to 'Disable'.")]
        public override SwitchParameter Disable { get; set; }

        internal void ExecuteCommand()
        {
            ValidateParameters();
            RemovePredicateExtensions();
            AddResourceExtension();
            WriteObject(VM);
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            ExecuteCommand();
        }
    }
}
