using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Spread.Form;

public partial class SpreadFormHelper<TEntity>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, short>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, short?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, int>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, int?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, long>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, long?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, float>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, float?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, double>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, double?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, decimal>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Number(Expression<Func<TEntity, decimal?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Percent(Expression<Func<TEntity, float>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Percent(Expression<Func<TEntity, float?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Percent(Expression<Func<TEntity, double>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Percent(Expression<Func<TEntity, double?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Percent(Expression<Func<TEntity, decimal>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Percent(Expression<Func<TEntity, decimal?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Date(Expression<Func<TEntity, DateOnly>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Date, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue Date(Expression<Func<TEntity, DateOnly?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.Date, rule);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue DateTime(Expression<Func<TEntity, DateTime>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue DateTime(Expression<Func<TEntity, DateTime?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue DateTime(Expression<Func<TEntity, DateTimeOffset>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SpreadFormValue DateTime(Expression<Func<TEntity, DateTimeOffset?>> selector, SpreadFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);

}
