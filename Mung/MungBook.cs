using NStandard.Text.Json;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Xml;

namespace Mung;

public interface IMungBook
{
    IMungSheet[] Sheets { get; }
}

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ISpreadsheetExtensions
{
    public static string ToJson(this IMungBook @this, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(@this, options);
    }

    public static XmlDocument ToXml(this IMungBook @this)
    {
        return JsonXmlSerializer.DeserializeXmlNode(@this.ToJson());
    }
}

public class MungBook : IMungBook, IEnumerable<MungSheet>
{
    public readonly List<MungSheet> Sheets = [];
    IMungSheet[] IMungBook.Sheets => Sheets.ToArray();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static MungBook Parse([StringSyntax(StringSyntaxAttribute.Json)] string json)
    {
        return JsonSerializer.Deserialize<MungBook>(json)!;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static MungBook Parse(XmlDocument doc)
    {
        return JsonSerializer.Deserialize<MungBook>(JsonXmlSerializer.SerializeXmlNode(doc))!;
    }

    public void Add(MungSheet sheet)
    {
        Sheets.Add(sheet);
    }

    public IEnumerator<MungSheet> GetEnumerator()
    {
        return Sheets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Sheets.GetEnumerator();
    }
}
