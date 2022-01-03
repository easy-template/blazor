using BlazorApp1;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proto;
using Boost.Proto.Actor.DependencyInjection;
using BlazorApp1.Actors.Counter;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton(sp => new ActorSystem().Root);
builder.Services.AddProtoActor(_ => _, _ => _);
builder.Services.AddSingleton<ProtoActorServiceStart>((sp, root) =>
{
    root.SpawnNamed(sp.GetService<IPropsFactory<CounterActor>>().Create(), "CounterActor");
});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
