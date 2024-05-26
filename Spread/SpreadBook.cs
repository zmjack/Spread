using NStandard.Text.Json;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Xml;

namespace Spread;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ISpreadBookExtensions
{
    public static string ToJson(this ISpreadBook @this, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(@this, options);
    }

    public static XmlDocument ToXml(this ISpreadBook @this)
    {
        return JsonXmlSerializer.DeserializeXmlNode(@this.ToJson());
    }
}

[JsonImpl<SpreadBook, ISpreadBook>]
public class SpreadBook : ISpreadBook, IEnumerable<ISpreadSheet>
{
    private readonly Dictionary<string, ISpreadSheet> _sheets = [];
    public ISpreadSheet[] Sheets => [.. _sheets.Values];

    public ISpreadSheet this[string name] => _sheets[name];

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static ISpreadBook Parse([StringSyntax(StringSyntaxAttribute.Json)] string json)
    {
        return JsonSerializer.Deserialize<SpreadBook>(json)!;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static ISpreadBook Parse(XmlDocument doc)
    {
        return JsonSerializer.Deserialize<SpreadBook>(JsonXmlSerializer.SerializeXmlNode(doc))!;
    }

    public void Add(ISpreadSheet sheet)
    {
        _sheets.Add(sheet.Name, sheet);
    }

    public IEnumerator<ISpreadSheet> GetEnumerator()
    {
        return _sheets.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _sheets.GetEnumerator();
    }
}
