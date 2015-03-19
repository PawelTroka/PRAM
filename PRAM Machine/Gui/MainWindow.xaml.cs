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
using System.ComponentModel;
using System.Windows.Threading;
using PRAM_Machine.Machine;

namespace PRAM_Machine.Gui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public DispatcherTimer runTimer;

        public MainWindow() {
            InitializeComponent();
            this.displayControl.statisticsDisplay = this.statisticsDisplay;
            this.statisticsDisplay.displayControl = this.displayControl;
            this.displayControl.mainWindow = this;
            this.statisticsDisplay.mainWindow = this;
            this.displayControl.IsVisibleChanged += new DependencyPropertyChangedEventHandler(displayControl_IsVisibleChanged);
            this.statisticsDisplay.IsVisibleChanged += new DependencyPropertyChangedEventHandler(statisticsDisplay_IsVisibleChanged);
            runTimer = new DispatcherTimer();
            runTimer.Interval = TimeSpan.FromMilliseconds(500);
        }

        public MainWindow(IPRAMMachine PRAMMachine) : this() {
            this.displayControl.Machine = PRAMMachine;
        }

        void statisticsDisplay_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue == true) {
                if (this.displayControl.Machine != null) {
                    this.statisticsDisplay.drawScalingLines();
                }
            }
        }

        void displayControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if ((bool)e.NewValue == true) {
                if (this.displayControl.Machine != null) {
                    this.displayControl.updateView();
                }
            } 
        }

        private void statisticsNextButton_Click(object sender, RoutedEventArgs e) {
            this.displayControl.Machine.Step();
            this.displayControl.updateStatistics();
            this.displayControl.updateStatisticsDisplay();
            if (this.displayControl.Machine.IsStopped) {
                this.disableButtons();
            }
        }

        private void statisticsRunButton_Click(object sender, RoutedEventArgs e) {
            if ((string)statisticsRunButton.Content == "Run") {
                this.statisticsRunButton.Content = "Stop";
                this.runTimer.Tick += new EventHandler(runTimer_Tick);
                this.runTimer.Start();
            } else {
                this.statisticsRunButton.Content = "Run";
                this.runTimer.Tick -= new EventHandler(runTimer_Tick);
                this.runTimer.Stop();
            }
        }

        void runTimer_Tick(object sender, EventArgs e) {
            if (this.statisticsNextButton.IsEnabled) {
                this.statisticsNextButton_Click(null, null);
            } else {
                this.disableButtons();
            }
        }

        private void statisticsTickButton_Click(object sender, RoutedEventArgs e) {
            for (int i = 0; i < 3; i++) {
                if (this.statisticsNextButton.IsEnabled) {
                    this.statisticsNextButton_Click(sender, e);
                } else {
                    this.disableButtons();
                }
            }
        }

        public void disableButtons() {
            this.displayControl.nextButton.IsEnabled = false;
            this.displayControl.nextTickButton.IsEnabled = false;
            this.displayControl.runButton.IsEnabled = false;
            this.statisticsTickButton.IsEnabled = false;
            this.statisticsRunButton.IsEnabled = false;
            this.statisticsNextButton.IsEnabled = false;
        }
    }
}
