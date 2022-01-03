namespace BlazorApp1.Actors.Counter;

using System.Threading.Tasks;
using Proto;


public class CounterActor : IActor
{
    CounterState State { get; set; } = new CounterState(0);

    Task ChangeState(IContext c, Func<CounterState, CounterState> func)
    {
        State = func(State);
        c.System.EventStream.Publish(State);
        return Task.CompletedTask;
    }


    public Task ReceiveAsync(IContext c) => c.Message switch
    {
        Increase => ChangeState(c, s => s with
        {
            Count = s.Count + 1
        }),
        ViewInitialized => ChangeState(c, s => s),
        _ => Task.CompletedTask
    };
}
