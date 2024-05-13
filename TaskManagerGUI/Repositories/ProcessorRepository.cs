using System.Management.Automation;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class ProcessorRepository
    {
        private const string CommandName = "Get-WmiObject -Class Win32_Processor";
        public async Task<ProcessorModel> GetProcessorInfo()
        {
            ProcessorModel ?processorModel = null;
            using (PowerShell powershell = PowerShell.Create())
            {
                powershell.AddScript(CommandName);
                var results = await powershell.InvokeAsync();

                foreach (var result in results)
                {
                    dynamic scriptProps = result;
                    processorModel = new ProcessorModel()
                    {
                        Caption = scriptProps.Caption,
                        Name = scriptProps.Name,
                        SocketDesignation = scriptProps.SocketDesignation,
                        Manufacturer = scriptProps.Manufacturer,
                        MaxClockSpeed = scriptProps.MaxClockSpeed
                    };
                }
            }
            return processorModel;
        }
    }
}
