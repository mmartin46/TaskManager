using System.Diagnostics;
using System.Management.Automation;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class MemoryRepository : IMemoryRepository
    {
        private const string _command = "Get-Counter '\\Memory\\Available MBytes'";

        public async Task<MemoryModel> GetMemoryModelAsync()
        {
            MemoryModel memoryModel = null;
            using (PerformanceCounter performanceCounter =
                    new PerformanceCounter("Memory", "Available MBytes"))
            {
                double availableMemory = performanceCounter.NextValue();

                memoryModel = new MemoryModel()
                {
                    TimeStamp = DateTime.Now,
                    MegaBytes = (double)availableMemory
                };
            }

            return memoryModel;
        }

    }
}
