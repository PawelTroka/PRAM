using System;
using System.Windows;
using System.Windows.Threading;
using PRAM_Machine.Machine;

namespace PRAM_Machine.Gui
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer runTimer;

        public MainWindow()
        {
            InitializeComponent();
            displayControl.StatisticsDisplay = statisticsDisplay;
            statisticsDisplay.displayControl = displayControl;
            displayControl.MainWindow = this;
            statisticsDisplay.mainWindow = this;
            displayControl.IsVisibleChanged += displayControl_IsVisibleChanged;
            statisticsDisplay.IsVisibleChanged += statisticsDisplay_IsVisibleChanged;
            runTimer = new DispatcherTimer();
            runTimer.Interval = TimeSpan.FromMilliseconds(500);
        }

        public MainWindow(IPRAMMachine PRAMMachine) : this()
        {
            displayControl.Machine = PRAMMachine;
        }

        private void statisticsDisplay_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                if (displayControl.Machine != null)
                {
                    statisticsDisplay.drawScalingLines();
                }
            }
        }

        private void displayControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                if (displayControl.Machine != null)
                {
                    displayControl.UpdateView();
                }
            }
        }

        private void statisticsNextButton_Click(object sender, RoutedEventArgs e)
        {
            displayControl.Machine.Step();
            displayControl.UpdateStatistics();
            displayControl.UpdateStatisticsDisplay();
            if (displayControl.Machine.IsStopped)
            {
                disableButtons();
            }
        }

        private void statisticsRunButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string) statisticsRunButton.Content == "Run")
            {
                statisticsRunButton.Content = "Stop";
                runTimer.Tick += runTimer_Tick;
                runTimer.Start();
            }
            else
            {
                statisticsRunButton.Content = "Run";
                runTimer.Tick -= runTimer_Tick;
                runTimer.Stop();
            }
        }

        private void runTimer_Tick(object sender, EventArgs e)
        {
            if (statisticsNextButton.IsEnabled)
            {
                statisticsNextButton_Click(null, null);
            }
            else
            {
                disableButtons();
            }
        }

        private void statisticsTickButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (statisticsNextButton.IsEnabled)
                {
                    statisticsNextButton_Click(sender, e);
                }
                else
                {
                    disableButtons();
                }
            }
        }

        public void disableButtons()
        {
            displayControl.nextButton.IsEnabled = false;
            displayControl.nextTickButton.IsEnabled = false;
            displayControl.runButton.IsEnabled = false;
            statisticsTickButton.IsEnabled = false;
            statisticsRunButton.IsEnabled = false;
            statisticsNextButton.IsEnabled = false;
        }
    }
}