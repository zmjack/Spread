using NStandard.Text.Json;
using System.Collections;
using System.ComponentModel;
using System.Text.Json;
using System.Xml;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Spread;

[JsonAs<SpreadBook, ISpreadBookSerializable>]
public class SpreadBook : ISpreadBook, IEnumerable<ISpreadSheet>, ISpreadBookSerializable, ISerializable
{
    private readonly Dictionary<string, SpreadSheet> _sheets = [];
    public SpreadSheet[] Sheets => [.. _sheets.Values];

    SpreadSheet[] ISpreadBookSerializable.Sheets
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

    public SpreadSheet this[string name] => _sheets[name];

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

    public void Add(SpreadSheet sheet)
    {
        _sheets.Add(sheet.Name, sheet);
    }

    public void Add(IEnumerable<SpreadSheet> sheets)
    {
        foreach (var sheet in sheets)
        {
            _sheets.Add(sheet.Name, sheet);
        }
    }

    public IEnumerator<ISpreadSheet> GetEnumerator()
    {
        return _sheets.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _sheets.GetEnumerator();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("info", info);
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
