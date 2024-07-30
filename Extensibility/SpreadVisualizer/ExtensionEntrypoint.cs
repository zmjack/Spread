using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;

namespace SpreadVisualizer;

/// <summary>
/// Extension entrypoint for the VisualStudio.Extensibility extension.
/// </summary>
[VisualStudioContribution]
internal class ExtensionEntrypoint : Extension
{
    /// <inheritdoc/>
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
                id: "Spread.Extensibility.051b627e-650e-4a94-8298-352f33600817",
                version: this.ExtensionAssemblyVersion,
                publisherName: "zmjack",
                displayName: "Spread.Extensibility",
                description: "Spread Extensibility Pack"),
    };

    /// <inheritdoc />
    protected override void InitializeServices(IServiceCollection serviceCollection)
    {
        base.InitializeServices(serviceCollection);

        // You can configure dependency injection here by adding services to the serviceCollection.
    }
}
