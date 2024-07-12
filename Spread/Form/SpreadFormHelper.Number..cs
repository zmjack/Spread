using Spread.Form.Columns;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Spread.Form;

public partial class SpreadFormHelper<TEntity>
{
    private static SpreadNumber InnerNumber<T>(Expression<Func<TEntity, T>> selector, SpreadRule? rule = null)
    {
        var pair = GetIndexPair(selector);
        return new SpreadNumber
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Rule = rule ?? GetDefaultRule(selector),
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, short>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, short?>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, int>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, int?>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, long>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, long?>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, float>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, float?>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, double>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, double?>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, decimal>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadNumber Number(Expression<Func<TEntity, decimal?>> selector, SpreadRule? rule = null) => InnerNumber(selector, rule);
}
