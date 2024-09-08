using NStandard.Text.Json;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Xml;

namespace Spread;

public class SpreadBook : ISpreadBook, IEnumerable
{
    private readonly Dictionary<string, ISpreadSheet> _sheets = [];

    public ISpreadSheet[] Sheets
    {
        get => [.. _sheets.Values];
        set
        {
            foreach (var sheet in value)
            {
                Add(sheet);
            }
        }
    }

    public ISpreadSheet this[string name] => _sheets[name];

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static SpreadBook Parse([StringSyntax(StringSyntaxAttribute.Json)] string json)
    {
        return JsonSerializer.Deserialize<SpreadBook>(json)!;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static SpreadBook Parse(XmlDocument doc)
    {
        return JsonSerializer.Deserialize<SpreadBook>(JsonXmlSerializer.SerializeXmlNode(doc))!;
    }

    public void Add(ISpreadSheet sheet)
    {
        _sheets.Add(sheet.Name, sheet);
    }

    public void Add(IEnumerable<ISpreadSheet> sheets)
    {
        foreach (var sheet in sheets)
        {
            _sheets.Add(sheet.Name, sheet);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _sheets.Values.GetEnumerator();
    }
}

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ISpreadBookExtensions
{
    public static string ToJson(this SpreadBook @this, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(@this, options);
    }

    public static XmlDocument ToXml(this SpreadBook @this)
    {
        return JsonXmlSerializer.DeserializeXmlNode(@this.ToJson());
    }
}
