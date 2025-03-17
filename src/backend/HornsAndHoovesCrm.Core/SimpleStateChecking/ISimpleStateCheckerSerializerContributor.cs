using System.Text.Json.Nodes;

namespace HornsAndHoovesCrm.Core.SimpleStateChecking;

public interface ISimpleStateCheckerSerializerContributor
{
    public string? SerializeToJson<TState>(ISimpleStateChecker<TState> checker)
        where TState : IHasSimpleStateCheckers<TState>;

    public ISimpleStateChecker<TState>? Deserialize<TState>(JsonObject jsonObject, TState state)
        where TState : IHasSimpleStateCheckers<TState>;
}