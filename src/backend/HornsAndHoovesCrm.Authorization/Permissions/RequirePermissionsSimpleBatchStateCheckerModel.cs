using HornsAndHoovesCrm.Core.SimpleStateChecking;

namespace HornsAndHoovesCrm.Authorization.Permissions;

public class RequirePermissionsSimpleBatchStateCheckerModel<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    public TState State { get; }

    public string[] Permissions { get; }

    public bool RequiresAll { get; }

    public RequirePermissionsSimpleBatchStateCheckerModel(TState state, string[] permissions, bool requiresAll = true)
    {
        State = state;
        Permissions = permissions;
        RequiresAll = requiresAll;
    }
}