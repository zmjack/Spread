using Microsoft.VisualStudio.Extensibility.UI;
using System.Runtime.Serialization;
namespace SpreadVisualizer;

public class HtmlUserControl : RemoteUserControl
{
    [DataContract]
    internal class Data
    {
    }

    private static readonly string _tmpFile = Path.Combine(Path.GetTempPath(), "SpreadVisualizer.html");

    private static void WriteTmpFile(string html)
    {
        File.WriteAllText(_tmpFile,
$"""
<!DOCTYPE html>
<html>
<head>
  <meta charset="UTF-8">
  <title>SpreadVisualizer</title>
</head>
<body>
{html}
</body>
</html>
""");
    }

    public HtmlUserControl(string value) : base(dataContext: new Data
    {
    })
    {
        WriteTmpFile(value);
    }

    public override Task ControlLoadedAsync(CancellationToken cancellationToken)
    {
        return base.ControlLoadedAsync(cancellationToken);
    }

    public override Task<string> GetXamlAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            return
$"""
<DataTemplate xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
              xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
              xmlns:vs="http://schemas.microsoft.com/visualstudio/extensibility/2022/xaml"
>
    <DockPanel Background="White">
        <WebBrowser Source="file:///{_tmpFile}" />
    </DockPanel>
</DataTemplate>
""";
        }, cancellationToken);
    }
}
