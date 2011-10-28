using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRAM_Machine.Gui
{
    /// <summary>
    /// Interaction logic for StatisticsDisplay.xaml
    /// </summary>
    public partial class StatisticsDisplay : UserControl
    {
        public List<int> readCounts;
        public List<int> writeCounts;
        public int maxValue;
        public DisplayControl displayControl;
        public MainWindow mainWindow;

        public StatisticsDisplay()
        {
            InitializeComponent();
            this.maxValue = 10;
            this.readCounts = new List<int>();
            this.writeCounts = new List<int>();
            for (int i = 0; i < 10; i++) {
                TextBlock tb = new TextBlock();
                tb.Text = (i * this.maxValue / 10).ToString();
                tb.SetValue(Grid.RowProperty, 9 - i);
                tb.SetValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                scalingGrid.Children.Add(tb);
            }
            drawScalingLines();
        }

        public void drawScalingLines() {
            double height = plotCanvas.Height;
            for (int i = 0; i < 9; i++) {
                drawScalingLine((i + 1) * height / 10);
            }
        }

        private void drawScalingLine(double y) {
            Line scalingLine = new Line();
            scalingLine.Stroke = new SolidColorBrush(Colors.DarkGray);
            scalingLine.StrokeThickness = 1.0;
            scalingLine.X1 = 0;
            scalingLine.Y1 = y;
            scalingLine.X2 = plotCanvas.Width;
            scalingLine.Y2 = y;
            plotCanvas.Children.Add(scalingLine);
        }

        public void drawLines() {
            plotCanvas.Children.Clear();
            drawScalingLines();
            double width = plotCanvas.Width;
            double height = plotCanvas.Height;
            maxValue = Math.Max(maxValue, readCounts.Max());
            maxValue = Math.Max(maxValue, writeCounts.Max());
            scalingGrid.Children.Clear();
            for (int i = 0; i < 10; i++) {
                TextBlock tb = new TextBlock();
                tb.Text = (i * maxValue / 10).ToString();
                tb.SetValue(Grid.RowProperty, 9 - i);
                tb.SetValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                scalingGrid.Children.Add(tb);
            } 

            double w = width / readCounts.Count;
            double h = height / maxValue;
            for (int i = 0; i < readCounts.Count - 1; i++) {
                Line l = new Line();
                l.X1 = i * w;
                l.Y1 = height - readCounts[i] * h;
                l.X2 = (i + 1) * w;
                l.Y2 = height - readCounts[i + 1] * h;
                l.StrokeThickness = 1.5;
                l.Stroke = new SolidColorBrush(Colors.DarkBlue);
                plotCanvas.Children.Add(l);
            }

            for (int i = 0; i < writeCounts.Count - 1; i++) {
                Line l = new Line();
                l.X1 = i * w;
                l.Y1 = height - writeCounts[i] * h;
                l.X2 = (i + 1) * w;
                l.Y2 = height - writeCounts[i + 1] * h;
                l.StrokeThickness = 1;
                l.Stroke = new SolidColorBrush(Colors.Red);
                plotCanvas.Children.Add(l);
            }
        }

    }
}
