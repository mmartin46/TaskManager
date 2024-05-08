using System.Management.Automation;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class DiskRepository
    {
        private const string DiskCommand = "Get-Disk | Select-Object";
        private const string PartitionCommand = "Get-Partition";
        public async Task<DiskModel> GetDiskInfo()
        {
            using (PowerShell powershell = PowerShell.Create())
            {
                powershell.AddScript(DiskCommand);

                var results = await powershell.InvokeAsync();

                dynamic scriptProperties = results;

                string FriendlyName = scriptProperties.FriendlyName;
                string HealthStatus = scriptProperties.HealthStatus;
                string PartitionStyle = scriptProperties.PartitionStyle;
                string OperationalStatus = scriptProperties.OperationalStatus;
                double TotalSize = scriptProperties.TotalSize;
            }
            return new DiskModel();
        }

    }
}
