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

namespace Microsoft.WindowsAzure.Management.ServiceManagement.Extensions
{
    using System;
    using System.Linq;
    using System.Management.Automation;
    using System.Security.Cryptography.X509Certificates;
    using DomainJoin;
    using WindowsAzure.ServiceManagement;

    /// <summary>
    /// New Windows Azure Service Domain Join Extension.
    /// </summary>
    [Cmdlet(VerbsCommon.New, DomainJoinExtensionConfigNoun, DefaultParameterSetName = DomainParameterSet), OutputType(typeof(ExtensionConfigurationInput))]
    public class NewAzureServiceDomainJoinExtensionConfigCommand : BaseAzureServiceDomainJoinExtensionCmdlet
    {
        public NewAzureServiceDomainJoinExtensionConfigCommand()
            : base()
        {
        }

        public NewAzureServiceDomainJoinExtensionConfigCommand(IServiceManagement channel)
            : base(channel)
        {
        }

        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet, HelpMessage = RoleHelpMessage)]
        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet, HelpMessage = RoleHelpMessage)]
        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupParameterSet, HelpMessage = RoleHelpMessage)]
        [Parameter(Position = 2, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupThumbprintParameterSet, HelpMessage = RoleHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override string[] Role { get; set; }

        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet, HelpMessage = X509CertificateHelpMessage)]
        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupParameterSet, HelpMessage = X509CertificateHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override X509Certificate2 X509Certificate { get; set; }

        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainThumbprintParameterSet, HelpMessage = CertificateThumbprintHelpMessage)]
        [Parameter(Position = 3, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = WorkgroupThumbprintParameterSet, HelpMessage = CertificateThumbprintHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override string CertificateThumbprint { get; set; }

        [Parameter(Position = 4, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet, HelpMessage = ThumbprintAlgorithmHelpMessage)]
        [Parameter(Position = 4, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet, HelpMessage = ThumbprintAlgorithmHelpMessage)]
        [Parameter(Position = 4, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupParameterSet, HelpMessage = ThumbprintAlgorithmHelpMessage)]
        [Parameter(Position = 4, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupThumbprintParameterSet, HelpMessage = ThumbprintAlgorithmHelpMessage)]
        [ValidateNotNullOrEmpty]
        public override string ThumbprintAlgorithm { get; set; }

        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = DomainThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string DomainName
        {
            get
            {
                return base.DomainName;
            }
            set
            {
                base.DomainName = value;
            }
        }

        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = WorkgroupParameterSet)]
        [Parameter(Position = 5, ValueFromPipelineByPropertyName = true, Mandatory = true, ParameterSetName = WorkgroupThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string WorkGroupName
        {
            get
            {
                return base.WorkGroupName;
            }
            set
            {
                base.WorkGroupName = value;
            }
        }

        [Parameter(Position = 6, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 6, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 6, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupParameterSet)]
        [Parameter(Position = 6, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string NewName
        {
            get
            {
                return base.NewName;
            }
            set
            {
                base.NewName = value;
            }
        }

        [Parameter(Position = 7, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 7, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 7, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupParameterSet)]
        [Parameter(Position = 7, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupThumbprintParameterSet)]
        public override SwitchParameter Restart
        {
            get
            {
                return base.Restart;
            }
            set
            {
                base.Restart = value;
            }
        }

        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupParameterSet)]
        [Parameter(Position = 8, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override PSCredential Credential
        {
            get
            {
                return base.Credential;
            }
            set
            {
                base.Credential = value;
            }
        }

        [Parameter(Position = 9, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 9, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [Parameter(Position = 9, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupParameterSet)]
        [Parameter(Position = 9, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = WorkgroupThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override PSCredential LocalCredential
        {
            get
            {
                return base.LocalCredential;
            }
            set
            {
                base.LocalCredential = value;
            }
        }

        [Parameter(Position = 10, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 10, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override PSCredential UnjoinDomainCredential
        {
            get
            {
                return base.UnjoinDomainCredential;
            }
            set
            {
                base.UnjoinDomainCredential = value;
            }
        }

        [Parameter(Position = 11, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 11, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override JoinOptions[] Options
        {
            get
            {
                return base.Options;
            }
            set
            {
                base.Options = value;
            }
        }

        [Parameter(Position = 12, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 12, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string OUPath
        {
            get
            {
                return base.OUPath;
            }
            set
            {
                base.OUPath = value;
            }
        }

        [Parameter(Position = 13, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 13, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        [ValidateNotNullOrEmpty]
        public override string Server
        {
            get
            {
                return base.Server;
            }
            set
            {
                base.Server = value;
            }
        }

        [Parameter(Position = 14, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainParameterSet)]
        [Parameter(Position = 14, ValueFromPipelineByPropertyName = true, Mandatory = false, ParameterSetName = DomainThumbprintParameterSet)]
        public override SwitchParameter Unsecure
        {
            get
            {
                return base.Unsecure;
            }
            set
            {
                base.Unsecure = value;
            }
        }

        protected override void ValidateParameters()
        {
            base.ValidateParameters();
            ValidateThumbprint(false);
            ValidateConfiguration();
        }

        public void ExecuteCommand()
        {
            ValidateParameters();
            WriteObject(new ExtensionConfigurationInput
            {
                CertificateThumbprint = CertificateThumbprint,
                ThumbprintAlgorithm = ThumbprintAlgorithm,
                ProviderNameSpace = ExtensionNameSpace,
                Type = ExtensionType,
                PublicConfiguration = PublicConfiguration,
                PrivateConfiguration = PrivateConfiguration,
                X509Certificate = X509Certificate,
                Roles = new ExtensionRoleList(Role != null && Role.Any() ? Role.Select(r => new ExtensionRole(r)) : Enumerable.Repeat(new ExtensionRole(), 1))
            });
        }

        protected override void OnProcessRecord()
        {
            ExecuteCommand();
        }
    }
}
