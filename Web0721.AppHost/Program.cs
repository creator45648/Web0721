var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Web_View>("web-view");

builder.Build().Run();
