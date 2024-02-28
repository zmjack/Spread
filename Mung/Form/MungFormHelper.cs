using NStandard;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Mung.Form;

public partial class MungFormHelper<TEntity>(TEntity source)
{
    private static string GetDisplayName<TRet>(Expression<Func<TEntity, TRet>> selector)
    {
        var expr = selector.Body;

        if (expr is MemberExpression member)
        {
            var display = member.Member.GetCustomAttribute<DisplayAttribute>();
            return display?.Name ?? member.Member.Name;
        }
        else if (expr is UnaryExpression unary)
        {
            if (unary.NodeType == ExpressionType.Convert)
            {
                var _member = (unary.Operand as MemberExpression)!;
                var display = _member.Member.GetCustomAttribute<DisplayAttribute>();
                return display?.Name ?? _member.Member.Name;
            }
            else throw new NotSupportedException();
        }
        else throw new NotSupportedException();
    }

    private static string[] GetPath<TRet>(Expression<Func<TEntity, TRet>> selector)
    {
        var units = new LinkedList<string>();
        for (var expr = selector.Body; expr is not null;)
        {
            if (expr is MemberExpression member)
            {
                units.AddFirst(member.Member.Name);
                expr = member.Expression;
            }
            else if (expr is UnaryExpression unary)
            {
                if (unary.NodeType == ExpressionType.Convert)
                {
                    var _member = (unary.Operand as MemberExpression)!;
                    units.AddFirst(_member.Member.Name);
                    expr = _member.Expression;
                }
                else throw new NotSupportedException();
            }
            else if (expr is ParameterExpression param) break;
            else throw new NotSupportedException();
        }
        return [.. units];
    }

    private static string GetDataIndex(string[] path)
    {
        return (from x in path select StringEx.CamelCase(x)).Join(".");
    }

    public TEntity Source { get; set; } = source;

    private static MungFormRule GetDefaultRule<T>(Expression<Func<TEntity, T>> selector)
    {
        var member = (selector.Body as MemberExpression)?.Member as PropertyInfo ?? throw new ArgumentException("Only MemberExpression is supported.", nameof(selector));

        var propertyType = member.PropertyType;
        string[] attributes = [.. from x in member.CustomAttributes select x.AttributeType.FullName];

        var required = attributes.Any(name => new[]
        {
            "System.Runtime.CompilerServices.RequiredMemberAttribute",
            "System.ComponentModel.DataAnnotations.RequiredAttribute",
        }.Contains(name) || (propertyType.IsValueType && !propertyType.IsNullable()));

        return new MungFormRule
        {
            Required = required,
        };
    }

    private static MungFormValue InnerValue<T>(Expression<Func<TEntity, T>> selector, string type, MungFormRule? rule = null)
    {
        rule ??= GetDefaultRule(selector);

        var path = GetPath(selector);
        var dataIndex = GetDataIndex(path);
        var label = GetDisplayName(selector);

        return new MungFormValue
        {
            Type = type,
            DataIndex = dataIndex,
            Label = label,
            Rule = rule,
        };
    }

    public MungFormValue Hide<T>(Expression<Func<TEntity, T>> selector) => InnerValue(selector, UIComponent.Hide, null);
    public MungFormValue Text(Expression<Func<TEntity, string>> selector, MungFormRule? rule = null) => InnerValue(selector, UIComponent.Text, rule);
    public MungFormImage Image(Expression<Func<TEntity, string>> selector, string url, MungFormRule? rule = null)
    {
        rule ??= GetDefaultRule(selector);

        var path = GetPath(selector);
        var dataIndex = GetDataIndex(path);
        var label = GetDisplayName(selector);

        return new MungFormImage
        {
            Type = UIComponent.Image,
            DataIndex = dataIndex,
            Label = label,
            Url = url,
            Rule = rule,
        };
    }

    public MungFormOption Option<T>(Expression<Func<TEntity, T>> selector, KeyValuePair<string, string>[] options, MungFormRule? rule = null)
    {
        rule ??= GetDefaultRule(selector);

        var path = GetPath(selector);
        var dataIndex = GetDataIndex(path);
        var label = GetDisplayName(selector);

        return new MungFormOption
        {
            Type = UIComponent.Option,
            DataIndex = dataIndex,
            Label = label,
            Rule = rule,
            Options = options,
        };
    }

    public MungFormOption Option<T>(Expression<Func<TEntity, T>> selector, string optionsUrl, MungFormRule? rule = null)
    {
        rule ??= GetDefaultRule(selector);

        var path = GetPath(selector);
        var dataIndex = GetDataIndex(path);
        var label = GetDisplayName(selector);

        return new MungFormOption
        {
            Type = UIComponent.Option,
            DataIndex = dataIndex,
            Label = label,
            Rule = rule,
            OptionsUrl = optionsUrl,
        };
    }

    public MungFormTable Table<T>(Expression<Func<TEntity, IEnumerable<T>>> selector, Func<MungFormHelper<T>, IMungFormColumn[]> columnSelector)
    {
        var path = GetPath(selector);
        var dataIndex = GetDataIndex(path);
        var label = GetDisplayName(selector);

        var helper = new MungFormHelper<T>(default);
        var columns = columnSelector(helper);
        var columnOptions = new Dictionary<string, KeyValuePair<string, string>[]>
        (
            from c in columns.OfType<MungFormOption>()
            select KeyValuePair.Create(c.DataIndex, c.Options)
        );

        return new MungFormTable
        {
            Type = UIComponent.Table,
            DataIndex = dataIndex,
            Label = label,
            Columns = columns,
            ColumnOptions = columnOptions,
        };
    }

}
