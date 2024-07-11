namespace Spread.Extensibility;

using Microsoft.VisualStudio.Extensibility.UI;

internal class SpreadSheetVisualizer : RemoteUserControl
{
    public SpreadSheetVisualizer(object value)
        : base(dataContext: null)
    {
    }
}