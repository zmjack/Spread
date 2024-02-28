using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Mung.Form;

public partial class MungFormHelper<TEntity>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, short>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, short?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, int>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, int?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, long>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, long?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, float>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, float?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, double>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, double?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, decimal>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Number(Expression<Func<TEntity, decimal?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Number, rule);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Percent(Expression<Func<TEntity, float>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Percent(Expression<Func<TEntity, float?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Percent(Expression<Func<TEntity, double>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Percent(Expression<Func<TEntity, double?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Percent(Expression<Func<TEntity, decimal>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Percent(Expression<Func<TEntity, decimal?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Percent, rule);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Date(Expression<Func<TEntity, DateOnly>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Date, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue Date(Expression<Func<TEntity, DateOnly?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Date, rule);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue DateTime(Expression<Func<TEntity, DateTime>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue DateTime(Expression<Func<TEntity, DateTime?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue DateTime(Expression<Func<TEntity, DateTimeOffset>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MungFormValue DateTime(Expression<Func<TEntity, DateTimeOffset?>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.DateTime, rule);

}
