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

namespace PRAM_Machine.Gui {
    /// <summary>
    /// Interaction logic for ProcessorView.xaml
    /// </summary>
    public partial class ProcessorView : UserControl {
        public ProcessorView() {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(ProcessorView_SizeChanged);
        }

        void ProcessorView_SizeChanged(object sender, SizeChangedEventArgs e) {
            this.Height = Math.Min(this.ActualWidth, this.ActualHeight);
            this.Width = Math.Min(this.ActualWidth, this.ActualHeight);
        }
    }
}
