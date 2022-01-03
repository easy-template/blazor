using BlazorApp1.Actors.Counter;
using Microsoft.AspNetCore.Components;
using Proto;
using Boost.Proto.Actor.DependencyInjection;

namespace BlazorApp1;

public partial class ActorService
{
    [Inject]
    private RootContext RootContext { get; set; }

    [Inject]
    private IPropsFactory<CounterActor> CounterActorFactory { get; set; }

    protected override Task OnInitializedAsync()
    {
        RootContext.SpawnNamed(CounterActorFactory.Create(), "CounterActor");

        return base.OnInitializedAsync();
    }
}
