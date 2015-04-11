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
    /// Interaction logic for MemoryCellView.xaml
    /// </summary>
    public partial class MemoryCellView : UserControl {
        private dynamic data;

        public dynamic Data {
            get { return data; }
            set { 
                data = value;
                if (data != null) {
                    this.memoryCellData.Text = data.ToString();
                } else {
                    this.memoryCellData.Text = "";
                }
            }
        }

        public MemoryCellView() {
            InitializeComponent();
//           this.SizeChanged += new SizeChangedEventHandler(MemoryCellView_SizeChanged); 
        }

        void MemoryCellView_SizeChanged(object sender, SizeChangedEventArgs e) {
            this.Height = Math.Min(this.ActualWidth, this.ActualHeight);
            this.Width = Math.Min(this.ActualWidth, this.ActualHeight);
        }

    }
}
