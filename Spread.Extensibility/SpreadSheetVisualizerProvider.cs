using Microsoft.VisualStudio.Extensibility.DebuggerVisualizers;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.RpcContracts.RemoteUI;

namespace Spread.Extensibility;

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
        get => new("SpreadSheet Visualizer", typeof(Simple));
    }

    /// <inheritdoc/>
    public override async Task<IRemoteUserControl> CreateVisualizerAsync(VisualizerTarget visualizerTarget, CancellationToken cancellationToken)
    {
        var targetObjectValue = await visualizerTarget.ObjectSource.RequestDataAsync<Simple>(jsonSerializer: null, cancellationToken);

        return new SpreadSheetVisualizer(targetObjectValue);
    }
}
