using System.Management.Automation;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class EventRepository
    {
        private const string _command = "Get-EventLog -LogName System -EntryType Error | Select-Object -First 10 *";

        public async Task<List<EventModel?>> GetEventLogsAsync()
        {
            List<EventModel> logs = new List<EventModel>();


            return logs;
        }
    }
}
