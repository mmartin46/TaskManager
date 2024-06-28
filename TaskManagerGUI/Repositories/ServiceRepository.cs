using System.Management.Automation;
using TaskManagerGUI.Data;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private ServiceDatabaseContext _dbContext;
        private const string command = "Get-Service";

        public ServiceRepository(ServiceDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<ServiceModel>> GetServices(string name = "N/A")
        {
            var allServices = await Get();
            var services = new List<ServiceModel>();
            foreach (var service in allServices)
            {
                if (name == null || (name != null && service.DisplayName.StartsWith(name)))
                services.Add(
                    new ServiceModel
                    {
                        Status = service.Status,
                        DisplayName = service.DisplayName
                    }
                );
            }

            return services;
        }


        public async Task<List<Services>> Get()
        {
            var services = _dbContext.Services;
            using (var powerShell = PowerShell.Create())
            {
                var rawServices = powerShell.AddScript(command).Invoke();
                foreach (var result in rawServices)
                {
                    dynamic service = result;

                    string status = service.Status.ToString() ?? "Unknown";
                    string displayName = service.DisplayName.ToString() ?? "Unknown";

                    await services.AddAsync(new Services
                    {
                        Status = status,
                        DisplayName = displayName
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
            return [.. services];
        }

        public void ConfigureService(string serviceName, string state)
        {
            string GetCommand(string command) =>
                command switch
                {
                    "Start" => $"Get-Service -DisplayName \"{serviceName}\" | Stop-Service -whatif",
                    "Stop" => $"Get-Service -DisplayName \"{serviceName}\" | Start-Service -whatif",
                    _ => $"Get-Service -DisplayName \"{serviceName}\" | Stop-Service -whatif"
                };

            try
            {
                using (var powerShell = PowerShell.Create())
                {
                    _ = powerShell.AddScript(GetCommand(state)).Invoke();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't execute process", ex);
            }

        }

    }
}
