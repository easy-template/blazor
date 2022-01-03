using BlazorApp1.Actors.Counter;
using Microsoft.AspNetCore.Components;
using Proto;

namespace BlazorApp1;

public partial class ActorService
{
    [Inject]
    private RootContext RootContext { get; set; }

    protected override Task OnInitializedAsync()
    {
        var props = Props.FromProducer(() => new CounterActor());
        var pid = RootContext.SpawnNamed(props, "CounterActor");

        return base.OnInitializedAsync();
    }
}
