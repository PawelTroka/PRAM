using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PRAM_Machine.ComplexDataTypes;
using PRAM_Machine.Machine;
using PRAM_Machine.Memory;
using PRAM_Machine.Models;

namespace PRAM_Machine.Gui
{
    /// <summary>
    ///     Interaction logic for DisplayControl.xaml
    /// </summary>
    public partial class DisplayControl : UserControl
    {
        private readonly Dictionary<string, List<MemoryCellView>> _memoryRows;
        private readonly List<ProcessorView> _processorsList;
        public MainWindow MainWindow;
        public StatisticsDisplay StatisticsDisplay;
        private IPRAMMachine _machine;

        public DisplayControl()
        {
            InitializeComponent();
            _machine = null;
            _processorsList = new List<ProcessorView>();
            _memoryRows = new Dictionary<string, List<MemoryCellView>>();
            Loaded += DisplayControl_Loaded;
        }

        public IPRAMMachine Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
                PopulateMemoryCellsGrid(_machine.Model.PRAM);
                PopulateProcessorsGrid(_machine.Model.Processors);
            }
        }

        private void DisplayControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateView();
        }

        public void PopulateProcessorsGrid(List<ProcessorModel> processors)
        {
            for (int i = 0; i < processors.Count; i++)
            {
                if (processorsGrid.ColumnDefinitions.Count <= i)
                {
                    processorsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                var processorView = new ProcessorView {processorNumber = {Text = processors[i].Number.ToString()}};
                processorView.SetValue(Grid.ColumnProperty, processors[i].Number);
                if (processors[i].IsStopped)
                {
                    Brush redBrush = new SolidColorBrush(Colors.LightCoral);
                    processorView.processorFrame.Fill = redBrush;
                }
                processorsGrid.Children.Add(processorView);
                _processorsList.Add(processorView);
            }
        }

        public void UpdateProcessorsGrid(List<ProcessorModel> processors)
        {
            for (int i = 0; i < processors.Count; i++)
            {
                if (processors[i].IsStopped)
                {
                    Brush redBrush = new SolidColorBrush(Colors.LightCoral);
                    _processorsList[i].processorFrame.Fill = redBrush;
                }
            }
        }

        public static bool IsInstanceOfGenericType(Type genericType, object instance)
        {
            Type type = instance.GetType();
            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }

        public void PopulateMemoryCellsGrid(PRAMModel dataRows)
        {
            int rowCount = -1;

            foreach (var kvp in dataRows)
            {
                if (memoryGrid.ColumnDefinitions.Count < 1)
                {
                    memoryGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                if (IsInstanceOfGenericType(typeof (Matrix<>), kvp.Value.First()))
                {
                    //MessageBox.Show("chuj " + kvp.Value.First().GetType().ToString());
                    //Type underlyingType = typeof (int);
                    // underlyingType = kvp.Value.First().GetType().GetTypeInfo().GenericTypeArguments[0];

                    foreach (dynamic o in kvp.Value)
                    {
                        while (memoryGrid.ColumnDefinitions.Count < o.ColumnsCount + 1)
                        {
                            memoryGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        }
                        rowCount++;
                        _memoryRows[kvp.Key] = new List<MemoryCellView>();


                        for (int i = 0; i < o.RowsCount; i++)
                        {
                            memoryGrid.RowDefinitions.Add(new RowDefinition());

                            var rowName = new TextBlock {Margin = new Thickness(7), Text = (i == 0) ? kvp.Key : ""};
                            var viewBox = new Viewbox
                            {
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Child = rowName
                            };
                            viewBox.SetValue(Grid.ColumnProperty, 0);
                            viewBox.SetValue(Grid.RowProperty, rowCount + i);

                            memoryGrid.Children.Add(viewBox);

                            for (int j = 0; j < o.ColumnsCount; j++)
                            {
                                var memoryCellView = new MemoryCellView {Data = o[i, j]};
                                memoryCellView.SetValue(Grid.ColumnProperty, j + 1);
                                memoryCellView.SetValue(Grid.RowProperty, rowCount + i);
                                memoryGrid.Children.Add(memoryCellView);
                                _memoryRows[kvp.Key].Add(memoryCellView);
                            }
                        }
                        rowCount += o.RowsCount;
                    }
                }
                else
                {
                    memoryGrid.RowDefinitions.Add(new RowDefinition());
                    rowCount++;


                    var rowName = new TextBlock {Margin = new Thickness(7), Text = kvp.Key};
                    var viewBox = new Viewbox
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Child = rowName
                    };
                    viewBox.SetValue(Grid.ColumnProperty, 0);
                    viewBox.SetValue(Grid.RowProperty, rowCount);

                    memoryGrid.Children.Add(viewBox);
                    _memoryRows[kvp.Key] = new List<MemoryCellView>();
                    while (memoryGrid.ColumnDefinitions.Count < kvp.Value.Count + 1)
                    {
                        memoryGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    for (int i = 0; i < kvp.Value.Count; i++)
                    {
                        var memoryCellView = new MemoryCellView {Data = kvp.Value[i]};
                        // MessageBox.Show(memoryCellView.Data.ToString());
                        memoryCellView.SetValue(Grid.ColumnProperty, i + 1);
                        memoryCellView.SetValue(Grid.RowProperty, rowCount);
                        memoryGrid.Children.Add(memoryCellView);
                        _memoryRows[kvp.Key].Add(memoryCellView);
                    }
                }
            }
        }

        public void UpdateMemoryCellsGrid(PRAMModel dataRows)
        {
            foreach (var kvp in dataRows)
            {
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    if (IsInstanceOfGenericType(typeof (Matrix<>), dataRows[kvp.Key][i]))
                    {
                        dynamic o = dataRows[kvp.Key][i];
                        for (int j = 0; j < o.RowsCount; j++)
                            for (int k = 0; k < o.ColumnsCount; k++)
                                _memoryRows[kvp.Key][j*o.ColumnsCount + k].Data = o[j, k];
                    }
                    else
                        _memoryRows[kvp.Key][i].Data = dataRows[kvp.Key][i];
                    //MessageBox.Show(dataRows[kvp.Key][i].ToString());
                }
            }
        }

        public void DrawArrow(Point start, Point end)
        {
            double tipLength = 3.0;
            double arrowThickness = 1.5;
            Color arrowColor = Colors.DarkGray;
            var body = new Line();
            body.X1 = start.X;
            body.Y1 = start.Y;
            body.X2 = end.X;
            body.Y2 = end.Y;
            body.StrokeThickness = arrowThickness;
            body.Stroke = new SolidColorBrush(arrowColor);

            double lineLength = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            var arrowVersor = new Point(tipLength*(end.X - start.X)/lineLength,
                tipLength*(end.Y - start.Y)/lineLength);

            var tipLeft = new Point(end.X + arrowVersor.Y - 3*arrowVersor.X,
                end.Y - arrowVersor.X - 3*arrowVersor.Y);
            var tipRight = new Point(end.X - arrowVersor.Y - 3*arrowVersor.X,
                end.Y + arrowVersor.X - 3*arrowVersor.Y);

            var tipLineLeft = new Line();
            tipLineLeft.X1 = tipLeft.X;
            tipLineLeft.Y1 = tipLeft.Y;
            tipLineLeft.X2 = end.X;
            tipLineLeft.Y2 = end.Y;
            tipLineLeft.Stroke = new SolidColorBrush(arrowColor);
            tipLineLeft.StrokeThickness = arrowThickness;

            var tipLineRight = new Line();
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

        public void nextButton_Click(object sender, RoutedEventArgs e)
        {
            _machine.Step();
            UpdateView();
            UpdateStatistics();
            UpdateStatisticsDisplay();
            if (_machine.IsStopped)
            {
                MainWindow.disableButtons();
            }
        }

        public void UpdateStatistics()
        {
            machineStateTextBlock.Text = _machine.Model.State.ToString();
            tickCountTextBlock.Text = _machine.Model.TickCount.ToString();
        }

        public void UpdateView()
        {
            if (Machine != null)
            {
                if (IsLoaded)
                {
                    arrowPaintingArea.Children.Clear();
                    UpdateMemoryCellsGrid(_machine.Model.PRAM);
                    UpdateProcessorsGrid(_machine.Model.Processors);
                    for (int i = 0; i < _machine.Model.Processors.Count; i++)
                    {
                        if (_machine.State == PRAMState.Reading)
                        {
                            MemoryAddress address = _machine.Model.Processors[i].DataToRead;
                            if (!address.Empty)
                            {
                                ProcessorView p = _processorsList[i];
                                MemoryCellView m = _memoryRows[address.MemoryName][address.Address];
                                DrawArrow(GetMemoryCellLocation(m), GetProcessorLocation(p));
                            }
                        }
                        if (_machine.State == PRAMState.Writing)
                        {
                            MemoryAddress address = _machine.Model.Processors[i].DataToWrite;
                            if (!address.Empty)
                            {
                                ProcessorView p = _processorsList[i];
                                MemoryCellView m = _memoryRows[address.MemoryName][address.Address];
                                DrawArrow(GetProcessorLocation(p), GetMemoryCellLocation(m));
                            }
                        }
                    }
                }
            }
        }

        public void UpdateStatisticsDisplay()
        {
            StatisticsDisplay.readCounts.Add(_machine.Model.ActualReadCount);
            StatisticsDisplay.writeCounts.Add(_machine.Model.ActualWriteCount);
            MainWindow.stateTextBLock.Text = _machine.State.ToString();
            MainWindow.clockTicksTextBLock.Text = _machine.TickCount.ToString();
            MainWindow.readCountTextBlock.Text = StatisticsDisplay.readCounts.Sum().ToString();
            MainWindow.writeCountTextBlock.Text = StatisticsDisplay.writeCounts.Sum().ToString();
            StatisticsDisplay.drawLines();
        }

        private Point GetProcessorLocation(ProcessorView processor)
        {
            Point canvasLocation = paintingArea.PointToScreen(new Point(0, 0));
            Point processorLocation = processor.PointToScreen(new Point(0, 0));
            return new Point(processorLocation.X - canvasLocation.X + processor.ActualWidth/2,
                processorLocation.Y - canvasLocation.Y + processor.ActualHeight);
        }

        private Point GetMemoryCellLocation(MemoryCellView memoryCell)
        {
            Point canvasLocation = paintingArea.PointToScreen(new Point(0, 0));
            Point memoryCellLocation = memoryCell.PointToScreen(new Point(0, 0));
            return new Point(memoryCellLocation.X - canvasLocation.X + memoryCell.ActualWidth/2,
                memoryCellLocation.Y - canvasLocation.Y);
        }

        private void nextTickButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (nextButton.IsEnabled)
                {
                    nextButton_Click(null, null);
                }
                else
                {
                    MainWindow.disableButtons();
                }
            }
        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string) runButton.Content == "Run")
            {
                runButton.Content = "Stop";
                MainWindow.runTimer.Tick += runTimer_Tick;
                MainWindow.runTimer.Start();
            }
            else
            {
                runButton.Content = "Run";
                MainWindow.runTimer.Tick -= runTimer_Tick;
                MainWindow.runTimer.Stop();
            }
        }

        private void runTimer_Tick(object sender, EventArgs e)
        {
            if (nextButton.IsEnabled)
            {
                nextButton_Click(null, null);
            }
            else
            {
                MainWindow.disableButtons();
            }
        }
    }
}