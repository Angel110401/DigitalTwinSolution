using DigitalTwin.Web.Infrastructure;
using DigitalTwin.Web.Services;
using DigitalTwin.Web.Domain.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Infra & Domain services
builder.Services.AddSingleton<IEventBus, EventBus>();
builder.Services.AddSingleton<IMachineRepository, InMemoryMachineRepository>();
builder.Services.AddSingleton<DigitalTwin.Web.Domain.Machines.MachineFactory>();

// Control & Simulation services
builder.Services.AddSingleton<MachineControllerService>();
builder.Services.AddHostedService<MachineSimulatorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Machines}/{action=Index}/{id?}");

app.Run();
