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
    using System.Collections.Generic;
    using System.Linq;
    using System.Management.Automation;
    using AutoMapper;
    using Helpers;
    using Management.Compute;
    using Management.Compute.Models;
    using Model;
    using Properties;
    using Storage;
    using Utilities.Common;

    [Cmdlet(VerbsData.Update, "AzureVM"), OutputType(typeof(ManagementOperationContext))]
    public class UpdateAzureVMCommand : IaaSDeploymentManagementCmdletBase
    {
        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The name of the Virtual Machine to update.")]
        [ValidateNotNullOrEmpty]
        public string Name
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, HelpMessage = "Virtual Machine to update.")]
        [ValidateNotNullOrEmpty]
        [Alias("InputObject")]
        public PersistentVM VM
        {
            get;
            set;
        }

        internal void ExecuteCommandNewSM()
        {
            ServiceManagementProfile.Initialize();

            base.ExecuteCommand();

            WindowsAzureSubscription currentSubscription = CurrentSubscription;
            if (CurrentDeploymentNewSM == null)
            {
                return;
            }

            // Auto generate disk names based off of default storage account 
            foreach (var datadisk in this.VM.DataVirtualHardDisks)
            {
                if (datadisk.MediaLink == null && string.IsNullOrEmpty(datadisk.DiskName))
                {
                    CloudStorageAccount currentStorage = currentSubscription.GetCloudStorageAccount();
                    if (currentStorage == null)
                    {
                        throw new ArgumentException(Resources.CurrentStorageAccountIsNotAccessible);
                    }

                    DateTime dateTimeCreated = DateTime.Now;
                    string diskPartName = VM.RoleName;

                    if (datadisk.DiskLabel != null)
                    {
                        diskPartName += "-" + datadisk.DiskLabel;
                    }

                    string vhdname = string.Format("{0}-{1}-{2}-{3}-{4}-{5}.vhd", ServiceName, diskPartName, dateTimeCreated.Year, dateTimeCreated.Month, dateTimeCreated.Day, dateTimeCreated.Millisecond);
                    string blobEndpoint = currentStorage.BlobEndpoint.AbsoluteUri;

                    if (blobEndpoint.EndsWith("/") == false)
                    {
                        blobEndpoint += "/";
                    }

                    datadisk.MediaLink = new Uri(blobEndpoint + "vhds/" + vhdname);
                }

                if (VM.DataVirtualHardDisks.Count > 1)
                {
                    // To avoid duplicate disk names
                    System.Threading.Thread.Sleep(1);
                }
            }

            var parameters = new VirtualMachineUpdateParameters
            {
                AvailabilitySetName = VM.AvailabilitySetName,
                Label = VM.Label,
                OSVirtualHardDisk = Mapper.Map<OSVirtualHardDisk>(VM.OSVirtualHardDisk),
                RoleName = VM.RoleName,
                RoleSize = string.IsNullOrEmpty(VM.RoleSize) ? null :
                           (VirtualMachineRoleSize?)Enum.Parse(typeof(VirtualMachineRoleSize), VM.RoleSize, true),
                ProvisionGuestAgent = VM.ProvisionGuestAgent,
                ResourceExtensionReferences = Mapper.Map<List<ResourceExtensionReference>>(VM.ResourceExtensionReferences)
            };

            if (VM.DataVirtualHardDisks != null)
            {
                VM.DataVirtualHardDisks.ForEach(c =>
                {
                    var dataDisk = Mapper.Map<DataVirtualHardDisk>(c);
                    dataDisk.LogicalUnitNumber = dataDisk.LogicalUnitNumber;
                    parameters.DataVirtualHardDisks.Add(dataDisk);
                });
            }

            if (VM.ConfigurationSets != null)
            {
                PersistentVMHelper.MapConfigurationSets(VM.ConfigurationSets).ForEach(c => parameters.ConfigurationSets.Add(c));
            }

            ExecuteClientActionNewSM(
                parameters,
                CommandRuntime.ToString(),
                () => this.ComputeClient.VirtualMachines.Update(this.ServiceName, CurrentDeploymentNewSM.Name, this.Name, parameters));
        }
        
        internal override void ExecuteCommand()
        {
            this.ExecuteCommandNewSM();
        }
    }
}
