using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Spread.Form;

public partial class SpreadFormHelper<TEntity>
{
    private static SpreadDateTime InnerDateTime<T>(Expression<Func<TEntity, T>> selector, SpreadRule? rule = null)
    {
        var pair = GetIndexPair(selector);
        return new SpreadDateTime
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Rule = rule ?? GetDefaultRule(selector),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadDateTime DateTime(Expression<Func<TEntity, DateTime>> selector, SpreadRule? rule = null) => InnerDateTime(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadDateTime DateTime(Expression<Func<TEntity, DateTime?>> selector, SpreadRule? rule = null) => InnerDateTime(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadDateTime DateTime(Expression<Func<TEntity, DateTimeOffset>> selector, SpreadRule? rule = null) => InnerDateTime(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadDateTime DateTime(Expression<Func<TEntity, DateTimeOffset?>> selector, SpreadRule? rule = null) => InnerDateTime(selector, rule);
}
