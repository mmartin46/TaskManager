using System.Management.Automation;
using System.ServiceProcess;
using TaskManagerGUI.Data;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private ServiceDatabaseContext _dbContext;
        private const string command = "Get-Service";
        private readonly ILogger<ServiceRepository> _logger;

        public ServiceRepository(ServiceDatabaseContext dbContext, ILogger<ServiceRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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

        private async Task UpdateService(string name, string state)
        {
            var record = _dbContext.Services.FirstOrDefault(service => service.DisplayName.Equals(name));

            if (record != null)
            {
                if (state.Equals("Running"))
                {
                    record.Status = "Stopped";
                }
                else if (state.Equals("Stopped"))
                {
                    record.Status = "Running";
                }

                _dbContext.Entry(record).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task ToggleService(ServiceController service, string state)
        {
            if (service.Status == ServiceControllerStatus.Stopped)
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            else if (service.Status == ServiceControllerStatus.Running)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }

            await UpdateService(service.DisplayName, state);
        }

        public async Task ConfigureService(string serviceName, string state)
        {
            ServiceController service = new ServiceController(serviceName, state);
            try
            {
                await ToggleService(service, state);
            } catch (Exception ex)
            {
                _logger.LogError("Problem toggling service {0} : {1}", serviceName, ex);
            }
            
        }

    }
}
