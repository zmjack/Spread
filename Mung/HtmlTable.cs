using NStandard;
using NStandard.Flows;
using System.Text;

namespace Mung;

public class HtmlTable
{
    public MungSheet Sheet { get; }
    public string Class { get; set; }

    public Dictionary<string, string> Props;
    public Dictionary<string, string> Style;

    public static readonly Dictionary<string, string> PropsDefault = new()
    {
        ["width"] = "100%",
    };
    public static readonly Dictionary<string, string> StyleDefault = new()
    {
        ["border-collapse"] = "collapse",
        ["word-break"] = "keep-all",
    };

    public HtmlTable(MungSheet table, string @class = "table")
    {
        Sheet = table;
        Class = @class;
    }

    private string? GetPropsString()
    {
        static string GetRow(KeyValuePair<string, string> prop)
        {
            return $" {StringFlow.HtmlEncode(prop.Key)}=\"{StringFlow.HtmlEncode(prop.Value)}\"";
        }

        var sb = new StringBuilder();
        var props = Props;
        var keys = props?.Keys.ToArray() ?? [];

        if (props is not null && props.Any())
        {
            foreach (var pair in props)
            {
                sb.Append(GetRow(pair));
            }
        }

        foreach (var pair in PropsDefault.Where(x => !keys.Contains(x.Key)))
        {
            sb.Append(GetRow(pair));
        }

        return sb.ToString();
    }

    private string? GetStyleString()
    {
        static string GetRow(KeyValuePair<string, string> pair)
        {
            return $"{StringFlow.HtmlEncode(pair.Key)}:{StringFlow.HtmlEncode(pair.Value)};";
        }

        var sb = new StringBuilder();
        var style = Style;
        var keys = style?.Keys.ToArray() ?? [];

        if (style is not null && style.Any())
        {
            foreach (var pair in style)
            {
                sb.Append(GetRow(pair));
            }
        }

        foreach (var pair in StyleDefault.Where(x => !keys.Contains(x.Key)))
        {
            sb.Append(GetRow(pair));
        }

        return sb.ToString();
    }

    private static string GetPropsString(params string[] props)
    {
        return props.Where(prop => !prop.IsNullOrWhiteSpace()).Select(prop => $" {prop}").Join("");
    }
    private static string RowSpanProp(ICell cell)
    {
        var span = cell.RowSpan;
        return span > 1 ? $@"rowspan=""{span}""" : "";
    }
    private static string ColSpanProp(ICell cell)
    {
        var span = cell.ColSpan;
        return span > 1 ? $@"colspan=""{span}""" : "";
    }

    private static string StyleProp(ICell cell)
    {
        if (cell.Ignored) return "";

        var dict = new Dictionary<string, string>();
        var style = cell.Style;

        if (style is null) return string.Empty;

        if (style.BackgroundColor is not null) dict.Add("background-color", $"#{style.BackgroundColor.Red:x2}{style.BackgroundColor.Green:x2}{style.BackgroundColor.Blue:x2}{(style.BackgroundColor.Alpha < byte.MaxValue ? $"{style.BackgroundColor.Alpha:x2}" : "")}");
        if (style.Color is not null) dict.Add("color", $"#{style.Color.Red:x2}{style.Color.Green:x2}{style.Color.Blue:x2}{(style.Color.Alpha < byte.MaxValue ? $"{style.Color.Alpha}:x2" : "")}");
        if (style.FontFamily is not null) dict.Add("font-family", $"{style.FontFamily}");
        if (style.FontSize.HasValue) dict.Add("font-size", $"{style.FontSize}px");
        if (style.Bold.HasValue && style.Bold.Value) dict.Add("font-weight", $"bold");
        if (style.BorderTop.HasValue && style.BorderTop.Value) dict.Add("border-top", $"1px solid #000000");
        if (style.BorderBottom.HasValue && style.BorderBottom.Value) dict.Add("border-bottom", $"1px solid #000000");
        if (style.BorderLeft.HasValue && style.BorderLeft.Value) dict.Add("border-left", $"1px solid #000000");
        if (style.BorderRight.HasValue && style.BorderRight.Value) dict.Add("border-right", $"1px solid #000000");
        if (style.HoriAlign.HasValue && style.HoriAlign.Value != HoriAlign.Preserve) dict.Add("text-align", $"{style.HoriAlign?.ToString().ToLower()}");
        if (style.VertAlign.HasValue && style.VertAlign.Value != VertAlign.Preserve) dict.Add("vertical-align", $"{style.VertAlign?.ToString().ToLower()}");
        if (style.Padding.HasValue) dict.Add("padding", $"{style.Padding.Value}");

        return $@"style=""{dict.Select(pair => $"{pair.Key}:{pair.Value}").Join(";")}""";
    }

    public string ToHtml()
    {
        static string? GetHtml(string? str)
        {
            if (str is null) return null;
            return str?.NormalizeNewLine()
                .Replace(" ", "\u00A0").Pipe(StringFlow.HtmlEncode)
                .Replace(Environment.NewLine, "<br/>");
        }

        var propsString = GetPropsString();
        var styleString = GetStyleString();

        var sb = new StringBuilder();
        sb.Append("<table");
        if (!Class.IsNullOrWhiteSpace()) sb.Append($@" class=""{Class}""");
        if (!propsString.IsNullOrWhiteSpace()) sb.Append(propsString);
        if (!styleString.IsNullOrWhiteSpace()) sb.Append($@" style=""{styleString}""");
        sb.AppendLine(">");

        sb.AppendLine(@"<tbody>");

        foreach (var row in Sheet.Rows)
        {
            sb.AppendLine(@"<tr>");
            foreach (var cell in row.Cells.Where(x => !x.Ignored))
            {
                //TODO: Auto calculate width
                var lines = cell.Comment?.GetPureLines() ?? [];
                int cellWidth = 10;
                if (lines.Any())
                {
                    cellWidth += lines.Max(x => (cell.Style?.FontSize ?? 12) * x.GetLengthA());
                }

                sb.AppendLine($@"<td{GetPropsString(RowSpanProp(cell), ColSpanProp(cell), StyleProp(cell))}>");
                if (!(cell.Comment?.IsNullOrWhiteSpace() ?? true))
                {
                    sb.AppendLine($@"
<span class=""excel-sharp"">
    <span class=""excel-text"">{GetHtml(cell.Text)}</span>
    <div></div>
    <div class=""excel-comment"" style=""width:{cellWidth}px"">
        {GetHtml(cell.Comment)}
    </div>
</span>
");
                }
                else sb.AppendLine(GetHtml(cell.Text));
                sb.AppendLine("</td>");
            }
            sb.AppendLine(@"</tr>");
        }

        sb.AppendLine(@"</tbody>");
        sb.AppendLine(@"</table>");

        return sb.ToString();
    }

}
