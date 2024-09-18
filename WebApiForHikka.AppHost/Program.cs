var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApiForHikka_WebApi>("webapiforhikka-webapi");

builder.Build().Run();
