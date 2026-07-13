using Microsoft.EntityFrameworkCore;
using ResturantManagmentSystemApp.Components;
using ResturantManagmentSystemApp;
using ResturantManagmentSystemApp.Services;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

try
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connString))
    {
        throw new Exception("Connection String 'DefaultConnection' is missing in appsettings.json!");
    }

    // Razor Components
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    // ✅ FIX 1: SignalR Hub timeout settings — connection zyada der tak zinda rahegi
    builder.Services.AddSignalR(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromMinutes(10);    // 10 min idle pe timeout
        options.HandshakeTimeout = TimeSpan.FromMinutes(2);          // handshake 2 min
        options.KeepAliveInterval = TimeSpan.FromSeconds(15);        // har 15 sec ping
        options.MaximumReceiveMessageSize = 1024 * 1024;             // 1MB message size
        options.EnableDetailedErrors = true;
    });

    // ✅ FIX 2: Circuit options — Blazor circuit zyada der tak memory mein rakho
    builder.Services.AddServerSideBlazor(options =>
    {
        options.DetailedErrors = true;
        options.DisconnectedCircuitMaxRetained = 100;
        options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(10);
        options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(2);
    });

    // PostgreSQL
    builder.Services.AddDbContextFactory<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<OrderService>();

    var app = builder.Build();

    // Pipeline
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    Console.WriteLine("--- Pizza Palace RMS is running! ---");
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine("FATAL ERROR: " + ex.Message);
    throw;
}
