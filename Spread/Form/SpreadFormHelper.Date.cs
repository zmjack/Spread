using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Spread.Form;

public partial class SpreadFormHelper<TEntity>
{
    private static SpreadDate InnerDate<T>(Expression<Func<TEntity, T>> selector, SpreadRule? rule = null)
    {
        var pair = GetIndexPair(selector);
        return new SpreadDate
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Rule = rule ?? GetDefaultRule(selector),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadDate Date(Expression<Func<TEntity, DateOnly>> selector, SpreadRule? rule = null) => InnerDate(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadDate Date(Expression<Func<TEntity, DateOnly?>> selector, SpreadRule? rule = null) => InnerDate(selector, rule);
}
