using System;
using System.Windows;
using System.Windows.Controls;

namespace PRAM_Machine.Gui
{
    /// <summary>
    ///     Interaction logic for MemoryCellView.xaml
    /// </summary>
    public partial class MemoryCellView : UserControl
    {
        private dynamic data;

        public MemoryCellView()
        {
            InitializeComponent();
            SizeChanged += MemoryCellView_SizeChanged;
        }

        public dynamic Data
        {
            get { return data; }
            set
            {
                data = value;
                if (data != null)
                {
                    memoryCellData.Text = data.ToString();
                }
                else
                {
                    memoryCellData.Text = "";
                }
            }
        }

        private void MemoryCellView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Height = Math.Min(ActualWidth, ActualHeight);
            Width = Math.Min(ActualWidth, ActualHeight);
        }
    }
}