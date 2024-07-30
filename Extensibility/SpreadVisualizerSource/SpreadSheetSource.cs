using Microsoft.VisualStudio.DebuggerVisualizers;
using Spread;
using System.Text.Json;

namespace SpreadVisualizerSource;

public class SpreadSheetSource : VisualizerObjectSource
{
    /// <inheritdoc/>
    public override void GetData(object target, Stream outgoingData)
    {
        using var writer = new Utf8JsonWriter(outgoingData);
        var html = new HtmlTable(target as SpreadSheet).ToHtml();
        JsonSerializer.Serialize(writer, new { Html = html });
    }
}
