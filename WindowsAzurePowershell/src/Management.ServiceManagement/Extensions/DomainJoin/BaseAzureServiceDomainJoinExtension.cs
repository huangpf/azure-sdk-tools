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
    using System.Management.Automation;
    using System.Security;
    using System.Text;
    using System.Xml.Linq;
    using DomainJoin;
    using Utilities.Websites.Services;
    using WindowsAzure.ServiceManagement;

    public abstract class BaseAzureServiceDomainJoinExtensionCmdlet : BaseAzureServiceExtensionCmdlet
    {
        protected const string DomainJoinExtensionNamespace = "Microsoft.Windows.Azure.Extensions";
        protected const string DomainJoinExtensionType = "DomainJoin";
        
        protected const string DomainJoinExtensionNoun = "AzureServiceDomainJoinExtension";
        protected const string DomainJoinExtensionConfigNoun = "AzureServiceDomainJoinExtensionConfig";

        protected const string DomainParameterSet = "DomainName";
        protected const string DomainThumbprintParameterSet = "DomainNameThumbprint";
        protected const string WorkgroupParameterSet = "WorkGroupName";
        protected const string WorkgroupThumbprintParameterSet = "WorkGroupNameThumbprint";

        protected PublicConfig PublicConfig { get; private set; }
        protected PrivateConfig PrivateConfig { get; private set; }

        public virtual string DomainName
        {
            get
            {
                return PublicConfig.Name.type == NameType.Domain ? PublicConfig.Name.Value : null;
            }
            set
            {
                PublicConfig.Name = new Name
                {
                    Value = value,
                    type = NameType.Domain
                };
            }
        }

        public virtual string WorkGroupName
        {
            get
            {
                return PublicConfig.Name.type == NameType.Workgroup ? PublicConfig.Name.Value : null;
            }
            set
            {
                PublicConfig.Name = new Name
                {
                    Value = value,
                    type = NameType.Workgroup
                };
            }
        }

        public virtual string NewName
        {
            get
            {
                return PublicConfig.NewName;
            }
            set
            {
                PublicConfig.NewName = value;
            }
        }

        public virtual JoinOptions[] Options
        {
            get
            {
                return PublicConfig.Options == null ? null : PublicConfig.Options.Option;
            }
            set
            {
                if (PublicConfig.Options == null)
                {
                    PublicConfig.Options = new PublicConfigOptions();
                }
                PublicConfig.Options.Option = value;
            }
        }

        public virtual string OUPath
        {
            get
            {
                return PublicConfig.OUPath;
            }
            set
            {
                PublicConfig.OUPath = value;
            }
        }

        public virtual SwitchParameter Restart
        {
            get
            {
                return PublicConfig.RestartSpecified ? PublicConfig.Restart : false;
            }
            set
            {
                PublicConfig.RestartSpecified = true;
                PublicConfig.Restart = value;
            }
        }

        public virtual string Server
        {
            get
            {
                return PublicConfig.Server;
            }
            set
            {
                PublicConfig.Server = value;
            }
        }

        public virtual SwitchParameter Unsecure
        {
            get
            {
                return PublicConfig.UnsecureSpecified ? PublicConfig.Unsecure : false;
            }
            set
            {
                PublicConfig.UnsecureSpecified = true;
                PublicConfig.Unsecure = value;
            }
        }

        public virtual PSCredential Credential
        {
            get
            {
                return new PSCredential(PublicConfig.User, GetSecurePassword(PrivateConfig.Password));
            }
            set
            {
                PublicConfig.User = value.UserName;
                PrivateConfig.Password = value.Password.ConvertToUnsecureString();
            }
        }

        public virtual PSCredential LocalCredential
        {
            get
            {
                return new PSCredential(PublicConfig.User, GetSecurePassword(PrivateConfig.LocalPassword));
            }
            set
            {
                PublicConfig.LocalUser = value.UserName;
                PrivateConfig.LocalPassword = value.Password.ConvertToUnsecureString();
            }
        }

        public virtual PSCredential UnjoinDomainCredential
        {
            get
            {
                return new PSCredential(PublicConfig.User, GetSecurePassword(PrivateConfig.UnjoinDomainPassword));
            }
            set
            {
                PublicConfig.UnjoinDomainUser = value.UserName;
                PrivateConfig.UnjoinDomainPassword = value.Password.ConvertToUnsecureString();
            }
        }

        public BaseAzureServiceDomainJoinExtensionCmdlet()
            : base()
        {
            Initialize();
        }

        public BaseAzureServiceDomainJoinExtensionCmdlet(IServiceManagement channel) 
            : base(channel)
        {
            Initialize();
        }

        protected void Initialize()
        {
            ExtensionNameSpace = DomainJoinExtensionNamespace;
            ExtensionType = DomainJoinExtensionType;
            PublicConfig = new PublicConfig();
            PrivateConfig = new PrivateConfig();
        }

        protected override void ValidateConfiguration()
        {
            PublicConfiguration = Serialize(PublicConfig);
            PrivateConfiguration = Serialize(PrivateConfig);
        }

        protected override ExtensionContext GetContext(Operation op, ExtensionRole role, HostedServiceExtension ext)
        {
            var config = Deserialize(ext.PublicConfiguration, typeof(PublicConfig)) as PublicConfig;
            return new DomainJoinExtensionContext
            {
                OperationId = op.OperationTrackingId,
                OperationDescription = CommandRuntime.ToString(),
                OperationStatus = op.Status,
                Extension = ext.Type,
                ProviderNameSpace = ext.ProviderNameSpace,
                Id = ext.Id,
                Role = role,
                DomainName = config.Name.type == NameType.Domain ? config.Name.Value : null,
                WorkGroupName = config.Name.type == NameType.Domain ? config.Name.Value : null,
                Server = config.Server,
                OUPath = config.OUPath,
                Unsecure = config.Unsecure,
                Options = config.Options == null ? null : config.Options.Option,
                User = config.User,
                LocalUser = config.LocalUser,
                UnjoinDomainUser = config.UnjoinDomainUser,
                NewName = config.NewName,
                Restart = config.Restart
            };
        }
    }
}
