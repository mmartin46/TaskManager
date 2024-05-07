using Microsoft.AspNetCore.SignalR;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Hubs
{
    public class MemoryStatsHub : Hub
    {
        public async Task SendMemoryStatsUpdate(List<MemoryModel> memoryStats)
        {
            await Clients.All.SendAsync("UpdateMemoryStats", memoryStats);
        }
    }
}
