# Billy's Console DI

## Simple lightweight Di for console applications

### Setup

```csharp
// Instantiate the DiServiceCollection
var services = new DiServiceCollection();

//Register your services
services.RegisterSingleton<ISayHello, Greeter>();
services.RegisterSingleton<App>();

// Build the container
var container = services.BuildContainer();

// Retrieve your desired configuration
var app = container.GetService<App>();

// Use as desired
await app.Startup();
```

### Transient scopes also supported

```csharp
services.RegisterTransient<ISayHello, Greeter>();
```

### Use a startup class
To use a startup class register your class as you see above.

In your startup class inject the necessary dependencies and call them inside of your startup method

```charp
private ISayHello _greeter;

public App(ISayHello greeter)
{
        _greeter = greeter;
}
public async Task Startup()
{
        Console.WriteLine("Here I go..."
    _greeter.SayHello();
}
```

make sure in your program.cs class, inside the main method you're calling your startup method
```csharp
await app.Startup();
```

