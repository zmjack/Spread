using NStandard.Text.Json;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Xml;

namespace Spread;

public interface ISpreadBook
{
    ISpreadSheet[] Sheets { get; }
}

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

public class SpreadBook : ISpreadBook, IEnumerable<SpreadSheet>
{
    public readonly List<SpreadSheet> Sheets = [];
    ISpreadSheet[] ISpreadBook.Sheets => Sheets.ToArray();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static ISpreadBook Parse([StringSyntax(StringSyntaxAttribute.Json)] string json)
    {
        return JsonSerializer.Deserialize<ISpreadBook>(json)!;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Devloping...")]
    public static ISpreadBook Parse(XmlDocument doc)
    {
        return JsonSerializer.Deserialize<ISpreadBook>(JsonXmlSerializer.SerializeXmlNode(doc))!;
    }

    public void Add(SpreadSheet sheet)
    {
        Sheets.Add(sheet);
    }

    public IEnumerator<SpreadSheet> GetEnumerator()
    {
        return Sheets.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Sheets.GetEnumerator();
    }
}
