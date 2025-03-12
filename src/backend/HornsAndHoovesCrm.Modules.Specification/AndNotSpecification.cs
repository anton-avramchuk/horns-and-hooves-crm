using System.Linq.Expressions;
using HornsAndHoovesCrm.Modules.Specification.Abstractions;
using HornsAndHoovesCrm.Modules.Specification.Extensions;

namespace HornsAndHoovesCrm.Modules.Specification;

/// <summary>
/// Represents the combined specification which indicates that the first specification
/// can be satisifed by the given object whereas the second one cannot.
/// </summary>
/// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
public class AndNotSpecification<T> : CompositeSpecification<T>
{
    /// <summary>
    /// Constructs a new instance of <see cref="AndNotSpecification{T}"/> class.
    /// </summary>
    /// <param name="left">The first specification.</param>
    /// <param name="right">The second specification.</param>
    public AndNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
    {
    }

    /// <summary>
    /// Gets the LINQ expression which represents the current specification.
    /// </summary>
    /// <returns>The LINQ expression.</returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        var rightExpression = Right.ToExpression();

        var bodyNot = Expression.Not(rightExpression.Body);
        var bodyNotExpression = Expression.Lambda<Func<T, bool>>(bodyNot, rightExpression.Parameters);

        return Left.ToExpression().And(bodyNotExpression);
    }
}

/// <summary>
/// Represents the parameter rebinder used for rebinding the parameters
/// for the given expressions. This is part of the solution which solves
/// the expression parameter problem when going to Entity Framework.
/// For more information about this solution please refer to http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx.
/// </summary>
internal class ParameterRebinder : ExpressionVisitor
{
    private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

    internal ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
    {
        _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }

    internal static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
        Expression exp)
    {
        return new ParameterRebinder(map).Visit(exp);
    }

    protected override Expression VisitParameter(ParameterExpression p)
    {
        if (_map.TryGetValue(p, out var replacement))
        {
            p = replacement;
        }

        return base.VisitParameter(p);
    }
}