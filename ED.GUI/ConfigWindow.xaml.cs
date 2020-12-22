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
using System.Windows.Shapes;

using AduSkin.Controls.Metro;
using ED.GUI.UC;

namespace ED.GUI
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : MetroWindow
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void UserControl1_BtnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("父弹窗");
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show(string.Format("{0},{1} | {2},{3} | {4},{5} | {6}", gtFile.LeftContext, gtFile.RightContext, gtTarget.LeftContext, gtTarget.RightContext, gtSample.LeftContext, gtSample.RightContext,cbJoinDb.IsChecked));
        }
    }
}
