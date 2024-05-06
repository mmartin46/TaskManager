using TaskManagerGUI.Models;
using System;
using System.Management.Automation;
using System.Collections.Generic;

namespace TaskManagerGUI
{
    public class ProcessRepository
    {
        private const string commandName = "Get-Process | Where-Object { $_.CPU -ne $null } | Select-Object -Property Id, ProcessName, CPU";
        private List<ProcessModel> processList;

        private List<ProcessModel> ExecuteScript()
        {
            List<ProcessModel> processes = new List<ProcessModel>();
            using (PowerShell powershell = PowerShell.Create())
            {
                var results = powershell.AddScript(commandName).Invoke();
                foreach (var result in results)
                {
                    dynamic scriptProperties = result;
                    int id = scriptProperties.Id;
                    string processName = scriptProperties.ProcessName;
                    double cpu = scriptProperties.CPU;

                    processes.Add
                    (
                        new ProcessModel
                        {
                            Id = id,
                            ProcessName = processName,
                            CPU = cpu
                        }
                    );
                }
            }
            return processes;
        }

        public List<ProcessModel> GetProcessesByName()
        {
            processList = ExecuteScript();

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
