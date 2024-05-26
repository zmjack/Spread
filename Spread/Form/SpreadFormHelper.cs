using NStandard;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Spread.Form;

public partial class SpreadFormHelper<TEntity>
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

    private static SpreadRule GetDefaultRule<T>(Expression<Func<TEntity, T>> selector)
    {
        var member = (selector.Body as MemberExpression)?.Member as PropertyInfo ?? throw new ArgumentException("Only MemberExpression is supported.", nameof(selector));

        var propertyType = member.PropertyType;
        string[] attributes = [.. from x in member.CustomAttributes select x.AttributeType.FullName];

        var required = attributes.Any(name => new[]
        {
            "System.Runtime.CompilerServices.RequiredMemberAttribute",
            "System.ComponentModel.DataAnnotations.RequiredAttribute",
        }.Contains(name) || (propertyType.IsValueType && !propertyType.IsNullable()));

        return new SpreadRule
        {
            Required = required,
        };
    }

    private struct DataIndexLabelPair
    {
        public string DataIndex { get; set; }
        public string Label { get; set; }
    }

    private static DataIndexLabelPair GetIndexPair<T>(Expression<Func<TEntity, T>> selector)
    {
        var path = GetPath(selector);
        var dataIndex = GetDataIndex(path);
        var label = GetDisplayName(selector);
        return new DataIndexLabelPair
        {
            DataIndex = dataIndex,
            Label = label,
        };
    }

    public SpreadLabel Label(string content)
    {
        return new SpreadLabel
        {
            Content = content,
        };
    }

    public SpreadHide Hide<T>(Expression<Func<TEntity, T>> selector)
    {
        var pair = GetIndexPair(selector);
        return new SpreadHide
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
        };
    }

    public SpreadText Text<T>(Expression<Func<TEntity, T>> selector, SpreadRule? rule = null)
    {
        var pair = GetIndexPair(selector);
        return new SpreadText
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Rule = rule ?? GetDefaultRule(selector),
        };
    }

    public SpreadImage Image<T>(Expression<Func<TEntity, T>> selector, string url, SpreadRule? rule = null)
    {
        var pair = GetIndexPair(selector);
        return new SpreadImage
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Url = url,
            Rule = rule ?? GetDefaultRule(selector),
        };
    }

    public SpreadOption Option<T>(Expression<Func<TEntity, T>> selector, SpreadOptionValue[] options, SpreadRule? rule = null)
    {
        var pair = GetIndexPair(selector);
        return new SpreadOption
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Rule = rule ?? GetDefaultRule(selector),
            Options = options,
        };
    }

    public SpreadFormTable Table<T>(Expression<Func<TEntity, IEnumerable<T>>> selector, Func<SpreadFormHelper<T>, ISpreadColumn[]> columnSelector, SpreadFormTableSetting setting = SpreadFormTableSetting.None)
    {
        var pair = GetIndexPair(selector);

        var helper = new SpreadFormHelper<T>();
        var columns = columnSelector(helper);
        var columnOptions = new Dictionary<string, SpreadOptionValue[]>
        (
            from c in columns.OfType<SpreadOption>()
            select KeyValuePair.Create(c.DataIndex, c.Options)
        );

        return new SpreadFormTable
        {
            DataIndex = pair.DataIndex,
            Label = pair.Label,
            Columns = columns,
            ColumnOptions = columnOptions,
            Setting = setting,
        };
    }

}
