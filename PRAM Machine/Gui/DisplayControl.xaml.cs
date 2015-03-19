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
using PRAM_Machine.Models;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;
using PRAM_Machine.Samples;
using System.Collections;

namespace PRAM_Machine.Gui {
    /// <summary>
    /// Interaction logic for DisplayControl.xaml
    /// </summary>
    public partial class DisplayControl : UserControl {
        private List<ProcessorView> processorsList;
        private Dictionary<string, List<MemoryCellView>> memoryRows;
        private IPRAMMachine machine;
        public StatisticsDisplay statisticsDisplay;
        public MainWindow mainWindow;

        public DisplayControl() {
            InitializeComponent();
            this.machine = null;
            processorsList = new List<ProcessorView>();
            memoryRows = new Dictionary<string, List<MemoryCellView>>();
            this.Loaded += new RoutedEventHandler(DisplayControl_Loaded);
        }

        void DisplayControl_Loaded(object sender, RoutedEventArgs e) {
            this.updateView();
        }


        private void drawEllipseAtPoint(Point point)
        {
            var ellipse = new Ellipse()
            {
                Fill = new SolidColorBrush() { Color = Color.FromArgb(255, 255, 255, 0) },
                StrokeThickness = 2,
                Stroke = Brushes.White,
                Width = 10,
                Height = 10,
            };
            Canvas.SetTop(ellipse, point.Y);
            Canvas.SetLeft(ellipse, point.X);
            arrowPaintingArea.Children.Add(ellipse);
        }


        public void populateProcessorsGrid(List<ProcessorModel> processors) {
            for (int i = 0; i < processors.Count; i++) {
                if (processorsGrid.ColumnDefinitions.Count <= i) {
                    processorsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                ProcessorView processorView = new ProcessorView();
                processorView.processorNumber.Text = processors[i].Number.ToString();
                processorView.SetValue(Grid.ColumnProperty, processors[i].Number);
                if (processors[i].IsStopped) {
                    Brush redBrush = new SolidColorBrush(Colors.LightCoral);
                    processorView.processorFrame.Fill = redBrush;
                }
                processorsGrid.Children.Add(processorView);
                processorsList.Add(processorView);
            }
        }

        public void updateProcessorsGrid(List<ProcessorModel> processors) {
            for (int i = 0; i < processors.Count; i++) {
                if (processors[i].IsStopped) {
                    Brush redBrush = new SolidColorBrush(Colors.LightCoral);
                    processorsList[i].processorFrame.Fill = redBrush;
                }
            }
        }

        public void populateMemoryCellsGrid(PRAMModel dataRows) {
            int rowCount = -1;
            foreach (KeyValuePair<string, List<dynamic>> kvp in dataRows) {
                memoryGrid.RowDefinitions.Add(new RowDefinition());
                rowCount++;
                if (memoryGrid.ColumnDefinitions.Count < 1) {
                    memoryGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                Viewbox viewBox = new Viewbox();
                TextBlock rowName = new TextBlock();
                rowName.Margin = new Thickness(7);
                rowName.Text = kvp.Key;
                viewBox.SetValue(Grid.ColumnProperty, 0);
                viewBox.SetValue(Grid.RowProperty, rowCount);
                viewBox.VerticalAlignment = VerticalAlignment.Center;
                viewBox.HorizontalAlignment = HorizontalAlignment.Center;
                viewBox.Child = rowName;
                memoryGrid.Children.Add(viewBox);
                memoryRows[kvp.Key] = new List<MemoryCellView>();
                while (memoryGrid.ColumnDefinitions.Count < kvp.Value.Count + 1) {
                    memoryGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                for (int i = 0; i < kvp.Value.Count; i++) {
                    MemoryCellView memoryCellView = new MemoryCellView();
                    memoryCellView.Data = kvp.Value[i];
                    memoryCellView.SetValue(Grid.ColumnProperty, i + 1);
                    memoryCellView.SetValue(Grid.RowProperty, rowCount);
                    memoryGrid.Children.Add(memoryCellView);
                    memoryRows[kvp.Key].Add(memoryCellView);
                }
            }
        }

        public void updateMemoryCellsGrid(PRAMModel dataRows) {
            foreach (KeyValuePair<string, List<dynamic>> kvp in dataRows) {
                for (int i = 0; i < kvp.Value.Count; i++) {
                    memoryRows[kvp.Key][i].Data = dataRows[kvp.Key][i];
                }
            }
        }

        public void drawArrow(Point start, Point end) {
            double tipLength = 3.0;
            double arrowThickness = 1.5;
            Color arrowColor = Colors.DarkGray;
            Line body = new Line();
            body.X1 = start.X;
            body.Y1 = start.Y;
            body.X2 = end.X;
            body.Y2 = end.Y;
            body.StrokeThickness = arrowThickness;
            body.Stroke = new SolidColorBrush(arrowColor);

            double lineLength = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            Point arrowVersor = new Point(tipLength * (end.X - start.X) / lineLength,
                                          tipLength * (end.Y - start.Y) / lineLength);

            Point tipLeft = new Point(end.X + arrowVersor.Y - 3 * arrowVersor.X,
                                      end.Y - arrowVersor.X - 3 * arrowVersor.Y);
            Point tipRight = new Point(end.X - arrowVersor.Y - 3 * arrowVersor.X,
                                       end.Y + arrowVersor.X - 3 * arrowVersor.Y);

            Line tipLineLeft = new Line();
            tipLineLeft.X1 = tipLeft.X;
            tipLineLeft.Y1 = tipLeft.Y;
            tipLineLeft.X2 = end.X;
            tipLineLeft.Y2 = end.Y;
            tipLineLeft.Stroke = new SolidColorBrush(arrowColor);
            tipLineLeft.StrokeThickness = arrowThickness;

            Line tipLineRight = new Line();
            tipLineRight.X1 = tipRight.X;
            tipLineRight.Y1 = tipRight.Y;
            tipLineRight.X2 = end.X;
            tipLineRight.Y2 = end.Y;
            tipLineRight.Stroke = new SolidColorBrush(arrowColor);
            tipLineRight.StrokeThickness = arrowThickness;

            arrowPaintingArea.Children.Add(body);
            arrowPaintingArea.Children.Add(tipLineLeft);
            arrowPaintingArea.Children.Add(tipLineRight);
        }

        public void nextButton_Click(object sender, RoutedEventArgs e) {
            machine.Step();
            updateView();
            updateStatistics();
            updateStatisticsDisplay();
            if (machine.IsStopped) {
                this.mainWindow.disableButtons();
            }
        }

        public void updateStatistics() {
            this.machineStateTextBlock.Text = machine.Model.State.ToString();
            this.tickCountTextBlock.Text = machine.Model.TickCount.ToString();
        }

        public void updateView() {
            if (this.Machine != null) {
                if (this.IsLoaded) {
                    arrowPaintingArea.Children.Clear();
                    updateMemoryCellsGrid(machine.Model.PRAM);
                    updateProcessorsGrid(machine.Model.Processors);
                    for (int i = 0; i < machine.Model.Processors.Count; i++) {
                        if (machine.State == PRAMState.Reading) {
                            var address = machine.Model.Processors[i].DataToRead;
                            if (!address.Empty) {
                                var p = processorsList[i];
                                var m = memoryRows[address.MemoryName][address.Address];
                                drawArrow(getMemoryCellLocation(m), getProcessorLocation(p));
                            }
                        }
                        if (machine.State == PRAMState.Writing) {
                            var address = machine.Model.Processors[i].DataToWrite;
                            if (!address.Empty) {
                                var p = processorsList[i];
                                var m = memoryRows[address.MemoryName][address.Address];
                                drawArrow(getProcessorLocation(p), getMemoryCellLocation(m));
                            }
                        }
                    }
                }
            }
        }

        public void updateStatisticsDisplay() {
            this.statisticsDisplay.readCounts.Add(machine.Model.ActualReadCount);
            this.statisticsDisplay.writeCounts.Add(machine.Model.ActualWriteCount);
            this.mainWindow.stateTextBLock.Text = machine.State.ToString();
            this.mainWindow.clockTicksTextBLock.Text = machine.TickCount.ToString();
            this.mainWindow.readCountTextBlock.Text = this.statisticsDisplay.readCounts.Sum().ToString();
            this.mainWindow.writeCountTextBlock.Text = this.statisticsDisplay.writeCounts.Sum().ToString();
            this.statisticsDisplay.drawLines();
        }

        private Point getProcessorLocation(ProcessorView processor)
        {
            Point processorLocation = processor.TranslatePoint(new Point(0, 0), arrowPaintingArea);

            return new Point(processorLocation.X + processor.ActualWidth / 2, processorLocation.Y + processor.ActualHeight / 2);
        }

        private Point getMemoryCellLocation(MemoryCellView memoryCell)
        {

            var memoryCellLocation = memoryCell.TranslatePoint(new Point(0, 0), arrowPaintingArea);

            return new Point(memoryCellLocation.X + memoryCell.ActualWidth / 2, memoryCellLocation.Y + memoryCell.ActualHeight / 2);
        }

        private void nextTickButton_Click(object sender, RoutedEventArgs e) {
            for (int i = 0; i < 3; i++) {
                if (this.nextButton.IsEnabled) {
                    this.nextButton_Click(null, null);
                } else {
                    this.mainWindow.disableButtons();
                }
            }
        }

        private void runButton_Click(object sender, RoutedEventArgs e) {
            if ((string)runButton.Content == "Run") {
                this.runButton.Content = "Stop";
                this.mainWindow.runTimer.Tick += new EventHandler(runTimer_Tick);
                this.mainWindow.runTimer.Start();
            } else {
                this.runButton.Content = "Run";
                this.mainWindow.runTimer.Tick -= new EventHandler(runTimer_Tick);
                this.mainWindow.runTimer.Stop();
            }
        }

        void runTimer_Tick(object sender, EventArgs e) {
            if (this.nextButton.IsEnabled) {
                this.nextButton_Click(null, null);
            } else {
                this.mainWindow.disableButtons();
            }
        }

        public IPRAMMachine Machine {
            get { return this.machine; }
            set { 
                this.machine = value;
                populateMemoryCellsGrid(machine.Model.PRAM);
                populateProcessorsGrid(machine.Model.Processors);
            }
        }
    }
}
