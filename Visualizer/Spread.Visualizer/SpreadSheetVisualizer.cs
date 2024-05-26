using Microsoft.VisualStudio.DebuggerVisualizers;
using Spread.Visualizer;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

[assembly: DebuggerVisualizer
(
    typeof(SpreadSheetVisualizer),
    typeof(VisualizerObjectSource),
    TargetTypeName = "Spread.SpreadSheet, Spread, Version=0.0.3.0, Culture=neutral, PublicKeyToken=null",
    Description = "SpreadSheet Visualizer"
)]
namespace Spread.Visualizer
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class SpreadSheetVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            IVisualizerObjectProvider3 provider = objectProvider as IVisualizerObjectProvider3;
            ISpreadSheet sheet = null;

            string content = default;

            try
            {
                using (var stream = provider.GetData())
                {
                    var formatter = new BinaryFormatter();
                    sheet = formatter.Deserialize(stream) as ISpreadSheet;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }

            var form = new System.Windows.Forms.Form
            {
                Text = "SpreadSheet Visualizer",
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
            };

            if (!(sheet is null))
            {
                var table = new HtmlTable(sheet);
                content = table.ToHtml();
            }

            var html = $@"<!DOCTYPE html>
<html>
<head>
</head>
<body>
{content}
</body>
</html>";

            void OnLoad(object sender, EventArgs e)
            {
                form.Invoke((Action)(() =>
                {
                    var browser = new WebBrowser
                    {
                        Dock = DockStyle.Fill,
                        Location = new Point(0, 0),
                        MinimumSize = new Size(20, 20),
                        Size = new Size(800, 450),
                        DocumentText = html,
                        Parent = form,
                    };
                }));
            }
            form.Load += OnLoad;

            windowService.ShowDialog(form);

            form.Load -= OnLoad;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(SpreadSheetVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
