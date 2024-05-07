using Microsoft.AspNetCore.SignalR;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Hubs
{
    public class ProcessHub : Hub
    {
        public async Task SendProcessesManageUpdate(List<ProcessModel> processModels)
        {
            await Clients.All.SendAsync("UpdateProcesses", processModels);
        }
    }
}
