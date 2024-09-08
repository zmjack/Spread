using Microsoft.VisualStudio.DebuggerVisualizers;
using Spread;
using System.Text.Json;

namespace SpreadVisualizerSource;

public class VertSource : VisualizerObjectSource
{
    /// <inheritdoc/>
    public override void GetData(object target, Stream outgoingData)
    {
        using var writer = new Utf8JsonWriter(outgoingData);
        var html = new HtmlTable(new SpreadSheet(nameof(Vert))
        {
            target as Vert,
        }).ToHtml();

        JsonSerializer.Serialize(writer, new HtmlWrapper
        {
            Html = html,
        });
    }
}
