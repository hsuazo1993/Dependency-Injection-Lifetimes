using Dependency_Injection_Lifetimes;
using Microsoft.Extensions.DependencyInjection;

#region Transient
Console.WriteLine("Transient (many light bulbs per room):");
Console.BackgroundColor = ConsoleColor.White;
Console.ForegroundColor = ConsoleColor.Green;
UseTransientLightBulb("Living Room");
Console.ForegroundColor = ConsoleColor.Blue;
UseTransientLightBulb();

Console.BackgroundColor = ConsoleColor.Magenta;
Console.ForegroundColor = ConsoleColor.White;
UseTransientLightBulb("Kitchen");
Console.ForegroundColor = ConsoleColor.Yellow;
UseTransientLightBulb();
Console.ResetColor();
#endregion

#region Scoped
Console.WriteLine("\nScoped (one light bulb per room):");
Console.BackgroundColor = ConsoleColor.White;
Console.ForegroundColor = ConsoleColor.Green;
UseScopedLightBulb("Bedroom 1");
Console.BackgroundColor = ConsoleColor.Magenta;
Console.ForegroundColor = ConsoleColor.Blue;
UseScopedLightBulb("Bedroom 2");
Console.ResetColor();
#endregion

#region Singleton
Console.WriteLine("\nSingleton (one light bulb for the whole house):");
UseSingletonLightBulb();
#endregion

void UseTransientLightBulb(string roomName = "")
{
    var services = new ServiceCollection();
    services.AddTransient<LightBulb>();
    using var serviceProvider = services.BuildServiceProvider();
    UseLightBulb(serviceProvider, roomName);
}

void UseScopedLightBulb(string roomName)
{
    var services = new ServiceCollection();
    services.AddScoped<LightBulb>();
    using var serviceProvider = services.BuildServiceProvider();
    using (var scope = serviceProvider.CreateScope())
    {
        UseLightBulb(scope.ServiceProvider, roomName);
        UseLightBulb(scope.ServiceProvider, "");
    }
}

void UseSingletonLightBulb()
{
    var services = new ServiceCollection();
    services.AddSingleton<LightBulb>();
    using var singletonServiceProvider = services.BuildServiceProvider();

    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Green;
    UseLightBulb(singletonServiceProvider, "Hallway"); 
    Console.BackgroundColor = ConsoleColor.Magenta;
    UseLightBulb(singletonServiceProvider, "Bathroom");
    Console.ResetColor();
}

void UseLightBulb(IServiceProvider provider, string roomName)
{
    var lightBulb = provider.GetRequiredService<LightBulb>();
    if (!string.IsNullOrEmpty(roomName))
    {
        Console.WriteLine($"In {roomName}:");
    }
    lightBulb.TurnOn();
    lightBulb.TurnOff();
}
