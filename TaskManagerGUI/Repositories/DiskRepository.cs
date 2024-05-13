using System.Management.Automation;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class DiskRepository
    {
        private const string DiskCommand = "Get-WmiObject -Class Win32_Processor";
        private const string PartitionCommand = "Get-Partition";
        public async Task<DiskModel> GetDiskInfo()
        {
            DiskModel ?disk = null;

            using (var shell = PowerShell.Create())
            {
                shell.AddCommand(PartitionCommand);

                var results = await shell.InvokeAsync();

                foreach (var result in results) 
                { 
                    
                }
            }
            return disk;
        }

    }
}
