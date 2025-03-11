namespace HornsAndHoovesCrm.Core.SimpleStateChecking;

public interface ISimpleStateCheckerManager<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    Task<bool> IsEnabledAsync(TState state);

    Task<SimpleStateCheckerResult<TState>> IsEnabledAsync(TState[] states);
}

public interface IHasSimpleStateCheckers<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    List<ISimpleStateChecker<TState>> StateCheckers { get; }
}