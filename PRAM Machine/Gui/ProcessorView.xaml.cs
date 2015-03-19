using System;
using System.Windows;
using System.Windows.Controls;

namespace PRAM_Machine.Gui
{
    /// <summary>
    ///     Interaction logic for ProcessorView.xaml
    /// </summary>
    public partial class ProcessorView : UserControl
    {
        public ProcessorView()
        {
            InitializeComponent();
            SizeChanged += ProcessorView_SizeChanged;
        }

        private void ProcessorView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = Math.Min(ActualWidth, ActualHeight);
            Width = Math.Min(ActualWidth, ActualHeight);
        }
    }
}