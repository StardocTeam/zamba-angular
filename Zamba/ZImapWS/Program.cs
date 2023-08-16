using ZImapWS;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(config => { 
    config.ServiceName = "ZImapWS";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
