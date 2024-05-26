using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Spread.Form;

public partial class SpreadFormHelper<TEntity>
{
    private static SpreadPercent InnerPercent<T>(Expression<Func<TEntity, T>> selector, SpreadRule? rule = null)
    {
        var pair = GetIndexPair(selector);
        return new SpreadPercent
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Rule = rule ?? GetDefaultRule(selector),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadPercent Percent(Expression<Func<TEntity, float>> selector, SpreadRule? rule = null) => InnerPercent(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadPercent Percent(Expression<Func<TEntity, float?>> selector, SpreadRule? rule = null) => InnerPercent(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadPercent Percent(Expression<Func<TEntity, double>> selector, SpreadRule? rule = null) => InnerPercent(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadPercent Percent(Expression<Func<TEntity, double?>> selector, SpreadRule? rule = null) => InnerPercent(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadPercent Percent(Expression<Func<TEntity, decimal>> selector, SpreadRule? rule = null) => InnerPercent(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadPercent Percent(Expression<Func<TEntity, decimal?>> selector, SpreadRule? rule = null) => InnerPercent(selector, rule);
}
