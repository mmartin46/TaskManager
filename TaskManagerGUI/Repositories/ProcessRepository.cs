using TaskManagerGUI.Models;
using System;
using System.Management.Automation;
using System.Collections.Generic;

namespace TaskManagerGUI.Repositories
{
    public class ProcessRepository : IProcessRepository
    {
        private const string CommandName = "Get-Process | Where-Object { $_.CPU -ne $null } | Select-Object -Property Id, ProcessName, CPU";
        private List<ProcessModel>? processList = null;

        private async Task<List<ProcessModel>> ExecuteScript()
        {
            List<ProcessModel> processes = new List<ProcessModel>();
            using (PowerShell powershell = PowerShell.Create())
            {

                powershell.AddScript(CommandName);

                var results = await powershell.InvokeAsync();


                foreach (var result in results)
                {
                    dynamic scriptProperties = result;

                    int id = scriptProperties.Id;
                    string processName = scriptProperties.ProcessName;
                    var cpu = scriptProperties.CPU;
                    cpu = Convert.ToDouble(((PSObject)cpu).BaseObject);

                    processes.Add
                    (
                        new ProcessModel
                        {
                            Id = id,
                            ProcessName = processName,
                            CPU = (double)cpu
                        }
                    );
                }
            }
            return processes;
        }

        // Returns all processes based on their declared name with a sum of CPU(s)
        public async Task<List<ProcessModel>> GetProcessesByNameAsync()
        {
            processList = await ExecuteScript();

            var groupProcessesByName = processList.GroupBy(p => p.ProcessName)
                                                  .Select(group => new ProcessModel
                                                  {
                                                      ProcessName = group.Key,
                                                      CPU = group.Sum(p => p.CPU)
                                                  })
                                                  .ToList();

            return groupProcessesByName;
        }

    }
}
