using Microsoft.VisualStudio.Extensibility.DebuggerVisualizers;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.RpcContracts.RemoteUI;
using Spread;
using SpreadVisualizerSource;
using System.Text.Json;

namespace SpreadVisualizer;

[VisualStudioContribution]
internal class SpreadSheetVisualizerProvider : DebuggerVisualizerProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SpreadSheetVisualizerProvider"/> class.
    /// </summary>
    /// <param name="extension">Extension instance.</param>
    /// <param name="extensibility">Extensibility object.</param>
    public SpreadSheetVisualizerProvider(ExtensionEntrypoint extension, VisualStudioExtensibility extensibility)
        : base(extension, extensibility)
    {
    }

    /// <inheritdoc/>
    public override DebuggerVisualizerProviderConfiguration DebuggerVisualizerProviderConfiguration
    {
        get => new("SpreadSheet Visualizer", typeof(SpreadSheet))
        {
            Style = VisualizerStyle.ToolWindow,
            VisualizerObjectSourceType = new(typeof(SpreadSheetSource))
        };
    }

    /// <inheritdoc/>
    public override async Task<IRemoteUserControl> CreateVisualizerAsync(VisualizerTarget visualizerTarget, CancellationToken cancellationToken)
    {
        var data = await visualizerTarget.ObjectSource.RequestDataAsync(cancellationToken);
        var json = data.ToString();
        var target = JsonSerializer.Deserialize<HtmlWrapper>(json);
        return new HtmlUserControl(target.Html);
    }
}
