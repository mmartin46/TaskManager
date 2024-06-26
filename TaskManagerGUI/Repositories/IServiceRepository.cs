﻿using TaskManagerGUI.Data;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Repositories
{
    public interface IServiceRepository
    {
        Task ConfigureService(string serviceName, string state);
        Task<List<ServiceModel>> GetServices(string name = "N/A");
        Task<List<Services>> Get();
    }
}