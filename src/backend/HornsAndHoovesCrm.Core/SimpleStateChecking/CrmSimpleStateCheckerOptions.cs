using HornsAndHoovesCrm.Core.Colllections;

namespace HornsAndHoovesCrm.Core.SimpleStateChecking;

public class CrmSimpleStateCheckerOptions<TState>
    where TState : IHasSimpleStateCheckers<TState>
{
    public ITypeList<ISimpleStateChecker<TState>> GlobalStateCheckers { get; }

    public CrmSimpleStateCheckerOptions()
    {
        GlobalStateCheckers = new TypeList<ISimpleStateChecker<TState>>();
    }
}